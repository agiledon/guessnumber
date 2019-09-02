using GuessNumber.Exceptions;
using GuessNumber.Model;
using Xunit;

namespace GuessNumber.Test.Model
{
    public class AnswerTest
    {
        [Fact]
        public void Should_throw_exception_if_given_number_is_greater_than_0()
        {
            // 这里需要确认是在构造函数中抛出异常，还是提供validate()方法

            Assert.Throws<OutOfRangeException>(() => Answer.Of(0, 1, 2, 10));
        }
    }
}