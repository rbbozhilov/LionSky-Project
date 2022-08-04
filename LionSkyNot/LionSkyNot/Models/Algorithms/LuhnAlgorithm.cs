using System.Text.RegularExpressions;

namespace LionSkyNot.Models.Algorithms
{
    public class LuhnAlgorithm
    {

        public bool Implementation(string cardNumber)
        {
            bool isvalid = false;
            cardNumber = Regex.Replace(cardNumber, @"\s+", "");

            if (!double.TryParse(cardNumber, out double result))
            {
                return false;
            }

            if (cardNumber.Length <= 1)
            {
                return false;
            }

            var finalCreditCardNumbers = new List<int>();
            int counter = 0;
            int indexer = cardNumber.Length - 1;

            for (int i = cardNumber.Length; i > 0; i--)
            {
                counter++;

                if (counter % 2 == 0)
                {
                    string doubledNumber = ((int)Char.GetNumericValue(cardNumber[indexer]) * 2).ToString();

                    if (doubledNumber.Length > 1)
                    {
                        int sumOfTwoDigits = 0;
                        sumOfTwoDigits = (int)Char.GetNumericValue(doubledNumber[0]) + (int)Char.GetNumericValue(doubledNumber[1]);
                        finalCreditCardNumbers.Add(sumOfTwoDigits);
                    }
                    else
                    {
                        finalCreditCardNumbers.Add(int.Parse(doubledNumber));
                    }
                }
                else
                {
                    finalCreditCardNumbers.Add((int)Char.GetNumericValue(cardNumber[indexer]));
                }

                indexer--;
            }

            if (finalCreditCardNumbers.Sum() % 10 == 0)
            {
                isvalid = true;
            }

            return isvalid;
        }

    }
}
