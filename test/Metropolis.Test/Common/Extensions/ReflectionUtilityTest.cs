using System;
using System.Linq.Expressions;
using FluentAssertions;
using Metropolis.Common.Extensions;
using Metropolis.ViewModels;
using NUnit.Framework;

namespace Metropolis.Test.Common.Extensions
{
    [TestFixture]
    public class ReflectionUtilityTest
    {
        [Test]
        public void GetProperty()
        {
            Expression<Func<ProjectDetailsViewModel, string>> expression = model => model.ProjectName;
            var property = expression.GetProperty();
            property.Should().NotBeNull();
            property.Name.Should().Be("ProjectName");
        }
    }
}
