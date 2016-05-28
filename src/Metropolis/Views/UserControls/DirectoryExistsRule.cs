using System;
using System.IO;
using System.Windows.Controls;

namespace Metropolis.Views.UserControls
{
    public class DirectoryExistsRule : ValidationRule
    {
        public static DirectoryExistsRule Instance = new DirectoryExistsRule();

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                if (!(value is string))
                    return new ValidationResult(false, "InvalidPath");

                 if (!Directory.Exists((string)value))
                    return new ValidationResult(false, "Path Not Found");                                        
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Invalid Path");
            }

            return new ValidationResult(true, null);
        }
    }
}
