﻿using System;
using System.Globalization;
using System.Windows.Controls;

namespace Metropolis.AutoComplete
{
    public class RequiredValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var result = new ValidationResult(true, null);
            if (value == null || ReferenceEquals(value, DBNull.Value))
            {
                result = new ValidationResult(false, "Required!");
            }
            return result;
        }
    }
}
