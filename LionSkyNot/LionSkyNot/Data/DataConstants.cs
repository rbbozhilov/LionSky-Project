namespace LionSkyNot.Data
{
    public class DataConstants
    {

        public class Gym
        {
            public const int FullNameMaxLength = 255;
            public const int FullNameMinLength = 2;
        }

        public class Categorie
        {
            public const int CategorieNameMaxLength = 50;
        }

        public class Class
        {
            public const int NameMaxLength = 255;
            public const int NameMinLength = 2;
            public const int MaxPractitionerCounts = 200;
            public const int MinPractitionerCount = 1;
        }

        public class Contact
        {
            public const string DisplayName = "Name";
            public const string DisplayEmail = "Email";
            public const string DisplayMessage = "Message";

            public const int StringLengthSubject = 100;
            public const int StringLengthMessage = 5000;

            public const string EmailNameErrorMessage = "The email is not valid";
            public const string SubjectStringLengthErrorMessage = "Too long subject";
            public const string MessageStringLengthErrorMessage = "Too long message";
        }

        public class Trainer
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 2;
            public const int DescriptionMaxLength = 255;
            public const int DescriptionMinLength = 5;
            public const int YearOfExperienceMax = 60;
            public const int YearOfExperienceMin = 0;
            public const int WeightMax = 150;
            public const int WeightMin = 1;
            public const int HeightMax = 250;
            public const int HeightMin = 1;
        }

        public class Exercise
        {
            public const int NameMaxLength = 200;
            public const int NameMinLength = 2;
            public const int DescriptionMaxLength = 2000;
            public const int DescriptionMinLength = 10;

            public const string DisplayTypeName = "Type";
        }

        public class TypeExercise
        {
            public const int NameMaxLength = 50;
        }

        public class Recipe
        {
            public const int NameMaxLength = 255;
            public const int NameMinLength = 2;
            public const int DescriptionMaxLength = 2000;
            public const int DescriptionMinLength = 10;
        }

        public class Brand
        {
            public const int NameMaxLength = 255;
        }

        public class Product
        {
            public const int NameMaxLength = 255;
            public const int NameMinLength = 2;
            public const int DescriptionMaxLength = 2000;
            public const int DescriptionMinLength = 10;
            public const int MinPrice = 0;
            public const int MaxPrice = 1000;
            public const int MaxInStock = 100;
            public const int MinInStock = 1;
            public const int MaxPromotionPercentage = 100;
            public const int MinPromotionPercentage = 0;

            public const string TypeDecimal = "decimal(18,2)";
        }

        public class Payment
        {

            public const int NameOnCardMaxLength = 30;
            public const int NameOnCardMinLength = 3;

            public const string CardNumberRegularExpression = @"^4[0-9]{12}(?:[0-9]{3})?$";
            public const string CvvCodeRegularExpression = @"^[0-9]{3,4}$";
            public const string MonthYearCardRegularExpression = @"^(0[1-9]|1[0-2])\/?([0-9]{4}|[0-9]{2})$";

            public const string CardNumberErrorMessage = "Invalid card number";
            public const string MonthYearCardErrorMessage = "Invalid date on card";
            public const string CvvCodeErrorMessage = "Invalid cvv code";
            public const string NameOnCardMaxLengthErrorMessage = "The name cannot be more then 30 characters";
            public const string NameOnCardMinLengthErrorMessage = "The name must be at least 3 characters";
        }


        public class Type
        {
            public const int NameMaxLength = 255;
        }

        public class Calculator
        {
            public const int MinAge = 1;
            public const int MaxAge = 100;
            public const int MinWeight = 1;
            public const int MaxWeight = 150;
            public const int MinHeight = 1;
            public const int MaxHeight = 250;

            public const string WeightErrorMessage = "Enter again, weight should be between {1} and {2}";
            public const string HeightErrorMessage = "Enter again, height should be between {1} and {2}";
            public const string AgeErrorMessage = "Enter again , age is not correct";

        }
    }
}
