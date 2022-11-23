using LionSkyNot.Models.Algorithms;
using System.Collections.Generic;

using Xunit;

namespace LionSkyNot.Tests.Models
{
    public class LuhnAlgorithmTest
    {


        [Fact]
        public void Implementation_ShouldReturnFalse_TheNumberIsText()
        {
            //Arrange

            LuhnAlgorithm algorithm = new LuhnAlgorithm();
            string cardNumber = "Test";

            //Act & Assert

            Assert.False(algorithm.Implementation(cardNumber));
        }


        [Fact]
        public void Implementation_ShouldReturnFalse_TheNumberIsLessThenTwo()
        {
            //Arrange

            LuhnAlgorithm algorithm = new LuhnAlgorithm();
            string cardNumber = "1";

            //Act & Assert
            Assert.False(algorithm.Implementation(cardNumber));
        }

        [Fact]
        public void Implementation_ShouldReturnFalse_TheNumberIsNotReal()
        {
            //Arrange

            LuhnAlgorithm algorithm = new LuhnAlgorithm();
            string cardNumber = "4999999999999108";

            //Act & Assert
            Assert.False(algorithm.Implementation(cardNumber));
        }

        [Fact]
        public void Implementation_ShouldReturnTrue_TheNumberIsCorrect()
        {
            //Arrange

            LuhnAlgorithm algorithm = new LuhnAlgorithm();
            string cardNumber = "424 242 42 42 42 4242";

            //Act & Assert
            Assert.True(algorithm.Implementation(cardNumber));
        }
    }
}
