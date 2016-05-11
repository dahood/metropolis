using System;
using System.Collections.Generic;
using System.Linq;

namespace Metropolis.Test.Utilities
{
    public static class ValidationExtensions
    {
        public static bool IsValid(this Validation validation)
        {
            return validation?.Errors == null || !validation.Errors.Any();
        }

        public static Validation IsEqual<T>(this Validation validation, T left, T right, string message)
        {
            return Equals(left, right) ? validation : validation.AddException(new ValidationException(message));
        }

        public static Validation IsFalse(this Validation validation, bool val, string message)
        {
            return val ? validation.AddException(new ValidationException(message)) : validation;
        }

        public static Validation IsTrue(this Validation validation, bool val, string message)
        {
            return val ? validation : validation.AddException(new ValidationException(message));
        }

        public static Validation IsNotNull<T>(this Validation validation, T theObject, string paramName) where T : class
        {
            return theObject == null ? validation.AddException(ValidationException.IsRequired(paramName)) : validation;
        }

        public static Validation TypeIsEqual<T>(this Validation validation, object theObject, string paramName)
        {
            return validation
                .IsNotNull(theObject, "theObject").Check()
                .IsEqual(typeof (T), theObject.GetType(), paramName);
        }

        public static Validation IsNotNull(this Validation validation, int theObject, string paramName)
        {
            return theObject == 0 ? validation.AddException(ValidationException.IsRequired(paramName)) : validation;
        }

        public static Validation IsNotNull(this Validation validation, decimal theObject, string paramName)
        {
            return theObject == 0 ? validation.AddException(ValidationException.IsRequired(paramName)) : validation;
        }

        public static Validation IsNotNull(this Validation validation, DateTime theObject, string paramName)
        {
            return Equals(theObject, DateTime.MinValue)
                ? validation.AddException(ValidationException.IsRequired(paramName))
                : validation;
        }

        public static Validation IsNotNull<T>(this Validation validation, T? theObject, string paramName)
            where T : struct
        {
            return theObject == null ? validation.AddException(ValidationException.IsRequired(paramName)) : validation;
        }

        public static Validation IsNull<T>(this Validation validation, T? theObject, string paramName) where T : struct
        {
            return theObject != null
                ? validation.AddException(ValidationException.FormattedError("{0} should be null", paramName))
                : validation;
        }

        public static Validation IsNull(this Validation validation, object theObject, string paramName)
        {
            return theObject != null
                ? validation.AddException(ValidationException.FormattedError("{0} should be null", paramName))
                : validation;
        }

        public static Validation IsNotEmpty(this Validation validation, string theString, string paramName)
        {
            return string.IsNullOrEmpty(theString)
                ? validation.AddException(ValidationException.IsRequired(paramName))
                : validation;
        }

        public static Validation IsNotEmpty<T>(this Validation validation, IEnumerable<T> theList, string paramName)
        {
            return theList == null || !theList.Any()
                ? validation.AddException(ValidationException.FormattedError("{0} is not expected to be empty",
                    paramName))
                : validation;
        }

        public static Validation IsEmpty(this Validation validation, string theString, string paramName)
        {
            return !string.IsNullOrEmpty(theString)
                ? validation.AddException(ValidationException.FormattedError("{0} should be Empty", paramName))
                : validation;
        }

        public static Validation IsEmpty<T>(this Validation validation, IEnumerable<T> theItems, string paramName)
        {
            return theItems != null && theItems.Any()
                ? validation.AddException(ValidationException.FormattedError("{0} should be Empty", paramName))
                : validation;
        }

        public static Validation IsNotEqual<T>(this Validation validation, T theObject, T comparison, string paramName)
        {
            return Equals(theObject, comparison)
                ? validation.AddException(ValidationException.IsRequired(paramName))
                : validation;
        }

        public static Validation GreaterThan(this Validation validation, DateTime? theObject, DateTime? comparison,
            string paramName)
        {
            if (!theObject.HasValue || !comparison.HasValue) return validation;
            return theObject.Value.CompareTo(comparison.Value) > 0
                ? validation
                : validation.AddException(new ValidationException(paramName));
        }

        public static Validation GreaterThan<T>(this Validation validation, T theObject, T comparison, string paramName)
            where T : IComparable<T>
        {
            return theObject.CompareTo(comparison) > 0
                ? validation
                : validation.AddException(new ValidationException(paramName));
        }

        public static Validation GreaterThanOrEqual(this Validation validation, DateTime? theObject,
            DateTime? comparison, string paramName)
        {
            if (!theObject.HasValue || !comparison.HasValue) return validation;
            return GreaterThanOrEqual(validation, theObject.Value, comparison.Value, paramName);
        }

        public static Validation Between(this Validation validation, int lowerLimit, int upperLimit, int comparison,
            string paramName)
        {
            return comparison < lowerLimit || comparison > upperLimit
                ? validation.AddException(
                    new ValidationException($"{paramName} is not within the range {lowerLimit} to {upperLimit}"))
                : validation;
        }

        public static Validation GreaterThanOrEqual<T>(this Validation validation, T theObject, T comparison,
            string paramName) where T : IComparable
        {
            return theObject.CompareTo(comparison) >= 0
                ? validation
                : validation.AddException(new ValidationException(paramName));
        }

        public static Validation ValidateDependantFields(this Validation validation, string theObject,
            string theDependantObject, string paramName,
            string dependantParamNames)
        {
            var hasValue = !string.IsNullOrEmpty(theObject);
            var hasDependant = !string.IsNullOrEmpty(theDependantObject);

            if (hasValue && !hasDependant)
                return validation.AddException(ValidationException.IsRequired(dependantParamNames));
            if (hasDependant && !hasValue)
                return validation.AddException(ValidationException.IsRequired(paramName));
            return validation;
        }

        public static Validation Check(this Validation validation)
        {
            if (validation == null) return null;
            if (!validation.Errors.Any()) return validation;
            if (validation.Errors.Count() == 1)
                throw new ValidationException("Validation Failure", validation.Errors.First());
            throw new ValidationException("Validation Failure", new MultiException(validation.Errors));
        }

        public static ValidationException Warnings(this Validation validation)
        {
            if (validation == null) return null;
            var enumerable = validation.Warnings;
            return enumerable.Count() == 1 ? enumerable.First() : new MultiException(enumerable);
        }

        public static Validation IsOfType<T>(this Validation validation, object theObject, string paramName)
        {
            if (theObject == null)
                return validation.AddException(ValidationException.IsRequired(paramName));
            if (!(theObject.GetType() == typeof (T)))
                return
                    validation.AddException(
                        new ValidationException($"{paramName} is not of type {typeof (T).Name}"));
            return validation;
        }

        public static Validation Contains<T>(this Validation validation, IEnumerable<T> items, T criteria,
            string message)
        {
            return items.Any(x => Equals(x, criteria))
                ? validation
                : validation.AddException(new ValidationException(message));
        }

        public static Validation Or(this Validation validation, string message, params bool[] criteria)
        {
            return criteria.Count(x => x) == 0 ? validation.Add(new ValidationException(message)) : validation;
        }

        public static Validation AddException(this Validation validation, ValidationException exception)
        {
            return (validation ?? new Validation()).Add(exception);
        }
    }
}