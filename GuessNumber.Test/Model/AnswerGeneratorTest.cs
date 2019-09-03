using System.Collections.Generic;
using GuessNumber.Model;
using Moq;
using Xunit;

namespace GuessNumber.Test.Model
{
    public class AnswerGeneratorTest
    {
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
    }
}