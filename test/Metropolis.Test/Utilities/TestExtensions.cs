﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace Metropolis.Test.Utilities
{
    public static class TestExtensions
    {
        public static void ShouldContain<T>(this IEnumerable<T> items, Func<T, bool> criteria, string message = "item not found")
        {
            var found = items.FirstOrDefault(criteria);
            found.Should().NotBeNull(message);
        }

        public static string ShouldContainText(this string content, string target)
        {
            content.Should().NotBeNullOrEmpty();
            target.Should().NotBeNullOrEmpty();
            content.Contains(target).Should().BeTrue($"{content} should contain {target}");
            return content;
        }
    }
}
