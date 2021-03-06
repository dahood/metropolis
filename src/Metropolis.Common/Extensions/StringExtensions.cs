﻿using System;
using System.Text.RegularExpressions;

namespace Metropolis.Common.Extensions
{
    public static class StringExtensions
    {
        public static string TrimTo(this string str, char c)
        {
            var indexOf = str.IndexOf(c);
            return indexOf == -1 ? string.Empty : str.Substring(indexOf + 1);
        }
        public static string TrimOff(this string str, char c)
        {
            var indexOf = str.IndexOf(c);
            return indexOf == -1 ? string.Empty : str.Substring(0, indexOf);
        }
        public static int AsInt(this string value)
        {
            return int.Parse(value);
        }
        
        public static string TrimPath(this string value, string pattern)
        {
            if (string.IsNullOrEmpty(pattern)) return value;

            var start = value.IndexOf(pattern, StringComparison.CurrentCultureIgnoreCase);
            return value.Remove(0, start - 1);
        }
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotEmpty(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static string RemoveWhitespace(this string value)
        {
            return Regex.Replace(value, @"\s+", "");
        }
    }
}
