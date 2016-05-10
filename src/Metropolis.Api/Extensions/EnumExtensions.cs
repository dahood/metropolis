using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Metropolis.Api.Extensions
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumOrderAttribute : Attribute
    {
        public int Order { get; set; }
    }

    public static class EnumExtensions
    {
        public static int IntValue(this Enum value)
        {
            return Convert.ToInt32(value);
        }

        public static IEnumerable<T> ToEnumExact<T>(this IEnumerable<string> input) where T : struct, IConvertible
        {
            return input.Select(ToEnumExact<T>);
        }

        /// <summary>
        /// Attempts to parse input string into a single enum value. Tries to be flexible by ignoring case, whitespace, and falling back to the enum Description attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns>Enum of Type T or throws an ArgumentException if no Enum value is found.</returns>
        public static T ToEnumExact<T>(this string input) where T : struct, IConvertible
        {
            T enumValue;
            if (TryParseToEnumExact(input, out enumValue))
                return enumValue;
            throw new ArgumentException($"Unable to convert '{input}' to an Enum of type {typeof (T).Name}.");
        }

        public static T ToEnumExactOrDefault<T>(this string input, T defaultValue) where T : struct, IConvertible
        {
            T enumValue;
            return TryParseToEnumExact(input, out enumValue) ? enumValue : defaultValue;
        }

        public static T? ToNullableEnumExact<T>(this string input) where T : struct, IConvertible
        {
            return input.RemoveWhitespace().IsEmpty() ? default(T?) : input.ToEnumExact<T>();
        }

        /// <summary>
        /// Attempts to parse input string into a single enum value. Tries to be flexible by ignoring case, whitespace, and falling back to the enum Description attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryParseToEnumExact<T>(this string input, out T value) where T : struct, IConvertible
        {
            var cleanInput = input.RemoveWhitespace().ToLower();
            foreach (var enumValue in Enum.GetValues(typeof (T)).Cast<T>())
            {
                if (cleanInput != enumValue.ToString(CultureInfo.InvariantCulture).ToLower()) continue;
                value = enumValue;
                return true;
            }

            return TryParseFromDescription(input, out value);
        }

        public static T ToEnumByDescription<T>(this string input) where T : struct, IConvertible
        {
            T parsed;
            if (!TryParseToEnumExact(input, out parsed))
                throw new ArgumentException($"{input} is not a valid member of FileInclusion ");
            return parsed;
        }

        public static List<T> GetEnumList<T>() where T : struct, IConvertible
        {
            var values = (T[]) Enum.GetValues(typeof (T));
            return new List<T>(values);
        }

        public static IEnumerable<T> GetSortedList<T>() where T : struct, IConvertible
        {
            var t = typeof (T);
            return !t.IsEnum ? null : Enum.GetValues(t).Cast<Enum>().OrderBy(x => x.GetSortOrder()).Cast<T>();
        }

        public static T ToEnum<T>(this int input) where T : IConvertible
        {
            return (T) Enum.Parse(typeof (T), input.ToString());
        }

        public static T ToEnum<T>(this string input) where T : IConvertible
        {
            return (T) Enum.Parse(typeof (T), input);
        }

        public static T ToEnum<T>(this long input) where T : IConvertible
        {
            return (T) Enum.Parse(typeof (T), input.ToString());
        }

        public static IEnumerable<T> ToEnumList<T>(this IEnumerable<string> values) where T : struct, IConvertible
        {
            return values?.Select(ToEnumExact<T>).ToArray() ?? new T[0];
        }

        private static bool TryParseFromDescription<T>(string input, out T value) where T : struct, IConvertible
        {
            value = default(T);
            var fields = typeof (T).GetFields();
            foreach (var field in from field in fields
                                  let descriptionAttribute = field.GetCustomAttribute<DescriptionAttribute>()
                                  where descriptionAttribute != null && descriptionAttribute.Description == input
                                  select field)
            {
                value = (T) Enum.Parse(typeof (T), field.Name);
                return true;
            }
            return false;
        }

        public static string GetDescription<T>(this T value) where T : struct, IConvertible
        {
            if (!typeof (T).IsEnum)
            {
                throw new ArgumentException("{0} must be an enumerated type", typeof (T).Name);
            }
            var fi = value.GetType().GetField(value.ToString(CultureInfo.InvariantCulture));
            var attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof (DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString(CultureInfo.InvariantCulture);
        }

        public static int GetSortOrder(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (EnumOrderAttribute[]) fi.GetCustomAttributes(typeof (EnumOrderAttribute), false);
            return attributes.Length > 0 ? attributes[0].Order : value.IntValue();
        }

        public static string GetName<T>(this Enum value)
        {
            return Enum.GetName(typeof (T), value);
        }
    }
}
