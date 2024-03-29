﻿using LionSkyNot.Models.Algorithms;
using System.ComponentModel.DataAnnotations;

namespace LionSkyNot.Infrastructure.AttributeValidations
{
    public class CardNumberAttribute : ValidationAttribute
    {

        public override bool IsValid(object? value)
        {
            bool isValid = false;

            if (value is string cardNumber)
            {
                isValid = new LuhnAlgorithm().Implementation(cardNumber);
            }

            return isValid;
        }

        public override string FormatErrorMessage(string name)
        {
            return "Credit card number not valid";
        }
    }
}
