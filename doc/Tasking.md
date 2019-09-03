## Step 0：分解任务

分解任务：
* 随机生成答案
* 判断每次猜测的结果
* 检查输入是否合法
* 记录并显示历史猜测数据
* 判断游戏结果。判断猜测次数，如果满6次但是未猜对则判负；如果在6次内猜测的4个数字值与位置都正确，则判胜 

## Step 1: 选择第一个任务

随机生成答案和检查输入是否合法，存在一个共同任务：检查答案的合法性

因此，先增加一个任务：检查答案的合法性。由此开始编写单元测试。

在创建测试时，先确定项目中包的结构。由于当前任务牵涉到了一个模型对象`Answer`，因此可以考虑定义一个包`Model`。同时，驱动出`Answer`类。

测试场景：
* 数字只能在0~9之间
* 数字不能重复

在编写测试时，站在调用者角度，就要思考该如何创建`Answer`对象，才能让调用者觉得非常舒服。在设计时，需要一直遵循Kent Beck的“简单设计”原则。

例如，需求要求答案是4个数字，那到底该定义为四个固定的参数，还是使用变参？

注意，在设计模型对象时，尽量不要考虑UI的输入。例如这的接口就应该设计为int类型。

抛出异常时，也需要考虑异常类的位置。

TDD的过程中，要记得随时重构。

## Step 2：选择第二个任务

在已经提供了Answer类之后，可以考虑选择“随机生成答案”这一任务。该任务需要具备生成一个满足条件的随机数。由于生成随机数是一个不可预知的功能，我们考虑用Moq。

采用测试驱动开发时，我们并没有先去实现代码，而是编写测试用例：

```cs
        [Fact]
        public void Should_generate_actual_answer()
        {
            // given
            var mockRandom = new Mock<IRandomIntNumber>();
            mockRandom.SetupSequence(r => r.Next())
                .Returns(1)
                .Returns(2)
                .Returns(13)
                .Returns(3)
                .Returns(2)
                .Returns(4);

            var generator = new AnswerGenerator(mockRandom.Object);
            
            // when
            var answer = generator.Generate();

            // then
            Assert.Equal(new List<int> {1, 2, 3, 4}, answer.Numbers);
        }
```


注意，在使用Moq来模拟一个方法被多次调用时，要使用`SetupSequence()`方法。

由于使用了Mock，使得我可以控制IRandomIntNumber的返回值。上面的测试用例覆盖了生成越界数字、重复数字的情况。使得`AnswerGenerator`生成的答案一定是合法的。

在实现代码时，我发现需要将`IList<int>`作为参数创建一个`Answer`会变得更加简单。故而，我修改了`Answer`类的实现，增加了一个新的构造函数。这时，我需要增加新的单元测试。由于这个新的构造函数重用了之前的`Of()`方法，我只需要对新逻辑覆盖测试即可。增加的测试为：

```cs
        [Fact]
        public void Should_throw_exeption_if_input_list_not_equal_to_4()
        {
            Assert.Throws<InvalidCountException>(() => Answer.Of(new List<int> {1, 2, 3, 4, 5}));
        }
```

在有测试帮助下，重构也变得更加有信心。

例如之前的实现代码为：

```cs
        public Answer Generate()
        {
            IList<int> numbers = new List<int>(4);
            var number = _random.Next();
            numbers.Add(number);

            for (int i = 0; i < 3; i++)
            {
                int nextNumber;
                do
                {
                    nextNumber = _random.Next();
                } while (numbers.Contains(nextNumber) || NotInRange(nextNumber));
                numbers.Add(nextNumber);
            }
            return Answer.Of(numbers);
        }
```

后来我提取了`for`循环内的代码，定义为一个生成正确的无重复数字的方法。提取了方法后，发现第4、5行的代码没有必要单独处理。根据提取出来的方法判断，其实生成多个数字的逻辑是完全一样的：

```cs
       public Answer Generate()
        {
            IList<int> numbers = new List<int>(AnswerSize);

            for (var i = 0; i < AnswerSize; i++)
            {
                numbers.Add(GenerateUniqueCorrectNumber(numbers));
            }

            return Answer.Of(numbers);
        }

        private int GenerateUniqueCorrectNumber(IList<int> numbers)
        {
            int nextNumber;
            do
            {
                nextNumber = _random.Next();
            } while (numbers.Contains(nextNumber) || NotInRange(nextNumber));

            return nextNumber;
        }
```

这个重构做了部分手动工作，将原来的第4、5行的代码去掉了，然后将遍历的次数改为4。由于有测试保证，重构之后再运行一次测试，测试通过，就说明重构没有引入问题。


当我们为`Answer`引入了新的工厂方法之后，我们发现为`Answer`的有效性验证定义了太多细粒度的异常。其实这些异常的差别仅在于异常信息的不同。故而都可以定义为`InvalidAnswerException`。

在测试方法中，需要验证异常时，就不仅要验证异常的类型，还要验证异常的消息。xUnit提供了`Record.Exception(lambda)`方法，来记录异常。然后对返回的异常进行断言。这种方式其实更符合Given-When-Then或AAA Pattern。

```cs
            var exception = Record.Exception(() => Answer.Of(-1, 1, 2, 9));

            Assert.IsType<InvalidAnswerException>(exception);
            Assert.Equal("The number must be between 0 to 9.", exception.Message);
```
