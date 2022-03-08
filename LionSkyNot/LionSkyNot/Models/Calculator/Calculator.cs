using LionSkyNot.Views.ViewModels.Calculator;

namespace LionSkyNot.Models.Calculator
{
    public class Calculator
    {

        private float prIncreaseMuscle = 2.5f;
        private float klIncreaseMuscle = 37.5f;
        private float prLoseWeight = 1.8f;
        private float klLoseWeight = 29;
        private float prWeightMaintenance = 2;
        private float klWeightMaintenance = 30;
        private float proteinResult;
        private float fatResult;
        private float caloriesResult;
        private float carbohydratesResult;



        public CalculatorViewModel Calculation(string choice, float weight)
        {

            var model = new CalculatorViewModel();

            if (choice == "Weight Loss")
            {
                CalculationLogic(prLoseWeight, klLoseWeight,weight);
            }

            else if (choice == "Weight Maintenance")
            {
                CalculationLogic(prWeightMaintenance, klWeightMaintenance,weight);
            }

            else
            {
                CalculationLogic(prIncreaseMuscle, klIncreaseMuscle,weight);
            }

            return new CalculatorViewModel()
            {
                Calories = caloriesResult,
                Protein = proteinResult,
                Carbohydrates = carbohydratesResult,
                Fat = fatResult
            };

        }

        private void CalculationLogic(float prConst, float klConst , float weight)
        {
            this.proteinResult = weight * prConst;
            this.caloriesResult = klConst * weight;
            float vug = (this.caloriesResult * 0.3f) + (this.proteinResult * 4);
            this.carbohydratesResult = (this.caloriesResult - vug) / 4;
            this.fatResult = (this.caloriesResult * 0.3f) / 9;
        }

    }
}
