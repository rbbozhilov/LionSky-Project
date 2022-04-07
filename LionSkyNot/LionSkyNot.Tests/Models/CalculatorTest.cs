using LionSkyNot.Models.Calculator;
using Xunit;

namespace LionSkyNot.Tests.Models
{
    public class CalculatorTest
    {

        private float prIncreaseMuscle = 2.5f;
        private float klIncreaseMuscle = 37.5f;
        private float prLoseWeight = 1.8f;
        private float klLoseWeight = 29;
        private float prWeightMaintenance = 2;
        private float klWeightMaintenance = 30;

        [Fact]
        public void CalculationForLossWeight_ShouldBeCorrect()
        {

            //Arrange
            var calculator = new Calculator();

            var choice = "Weight Loss";
            var weight = 55;
            var proteinResult = weight * prLoseWeight;
            var caloriesResult = klLoseWeight * weight;
            float vug = (caloriesResult * 0.3f) + (proteinResult * 4);
            var carbohydratesResult = (caloriesResult - vug) / 4;
            var fatResult = (caloriesResult * 0.3f) / 9;


            //Act

            var result = calculator.Calculation(choice, weight);


            //Assert

            Assert.Equal(proteinResult, result.Protein);
            Assert.Equal(caloriesResult, result.Calories);
            Assert.Equal(carbohydratesResult, result.Carbohydrates);
            Assert.Equal(fatResult, result.Fat);

        }


        [Fact]
        public void CalculationForWeightMaintenance_ShouldBeCorrect()
        {

            //Arrange
            var calculator = new Calculator();

            var choice = "Weight Maintenance";
            var weight = 55;
            var proteinResult = weight * prWeightMaintenance;
            var caloriesResult = klWeightMaintenance * weight;
            float vug = (caloriesResult * 0.3f) + (proteinResult * 4);
            var carbohydratesResult = (caloriesResult - vug) / 4;
            var fatResult = (caloriesResult * 0.3f) / 9;


            //Act

            var result = calculator.Calculation(choice, weight);


            //Assert

            Assert.Equal(proteinResult, result.Protein);
            Assert.Equal(caloriesResult, result.Calories);
            Assert.Equal(carbohydratesResult, result.Carbohydrates);
            Assert.Equal(fatResult, result.Fat);

        }


        [Fact]
        public void CalculationForMuscleMass_ShouldBeCorrect()
        {

            //Arrange
            var calculator = new Calculator();

            var choice = "Muslce Mass";
            var weight = 55;
            var proteinResult = weight * prIncreaseMuscle;
            var caloriesResult = klIncreaseMuscle * weight;
            float vug = (caloriesResult * 0.3f) + (proteinResult * 4);
            var carbohydratesResult = (caloriesResult - vug) / 4;
            var fatResult = (caloriesResult * 0.3f) / 9;


            //Act

            var result = calculator.Calculation(choice, weight);


            //Assert

            Assert.Equal(proteinResult, result.Protein);
            Assert.Equal(caloriesResult, result.Calories);
            Assert.Equal(carbohydratesResult, result.Carbohydrates);
            Assert.Equal(fatResult, result.Fat);

        }

    }
}
