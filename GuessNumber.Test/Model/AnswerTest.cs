using System;
using System.Collections.Generic;
using GuessNumber.Exceptions;
using GuessNumber.Model;
using Xunit;

namespace GuessNumber.Test.Model
{
    public class AnswerTest
    {
        [Fact]
        public void Should_throw_exception_if_given_number_is_less_than_0()
        {
            // 这里需要确认是在构造函数中抛出异常，还是提供validate()方法

            var exception = Record.Exception(() => Answer.Of(-1, 1, 2, 9));

            AssertAnswerException(exception, "The number must be between 0 to 9.");
        }

        [Fact]
        public void Should_throw_exception_if_given_number_is_greater_than_9()
        {
            var exception = Record.Exception(() => Answer.Of(0, 1, 2, 10));

            AssertAnswerException(exception, "The number must be between 0 to 9.");
        }

        [Fact]
        public void Should_throw_exception_if_any_given_number_is_same()
        {
            Assert.Throws<DuplicatedAnswerException>(() => Answer.Of(1, 7, 2, 7));
        }

        [Fact]
        public void Should_get_answer_if_all_number_is_correct()
        {
            var answer = Answer.Of(0, 1, 2, 3);

            Assert.Equal(4, answer.Numbers.Count);
            Assert.Equal(new List<int>{0, 1, 2, 3}, answer.Numbers);
        }

        [Fact]
        public void Should_throw_exeption_if_input_list_not_equal_to_4()
        {
            Assert.Throws<InvalidCountException>(() => Answer.Of(new List<int> {1, 2, 3, 4, 5}));
        }

        private static void AssertAnswerException(Exception exception, string message)
        {
            Assert.IsType<InvalidAnswerException>(exception);
            Assert.Equal(message, exception.Message);
        }
    }
}