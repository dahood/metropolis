using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Metropolis.Api.Extensions;
using Metropolis.Common.Models;
using Metropolis.ViewModels;
using NUnit.Framework;

namespace Metropolis.Test.Api.Extensions
{
    [TestFixture]
    public class PropertyChangedEventHandlerTest
    {
        private ProjectDetailsViewModel viewModel;
        private bool propertyChanged;
        private List<string> propertyNames;

        [SetUp]
        public void SetUp()
        {
            viewModel = new ProjectDetailsViewModel();

            propertyChanged = false;
            propertyNames = new List<string>();

            viewModel.PropertyChanged += (s, e) =>
                                        {
                                            propertyChanged = true;
                                            propertyNames.Add(e.PropertyName);
                                        };
        }

        [Test]
        public void NotifiesOnProjectName()
        {
            ShouldNotify(x => x.ProjectName = "change me", "ProjectName", "IsValid");
        }

        [Test]
        public void NotifiesOnIgnoreFile()
        {
            ShouldNotify(x => x.IgnoreFile = "change me", "IgnoreFile");
        }
        
        [Test]
        public void NotifiesOnMetricsRepositorySourceType()
        {
            ShouldNotify(x => x.RepositorySourceType = RepositorySourceType.ECMA, "RepositorySourceType");
        }

        [Test]
        public void NotifiesOnMetricsSourceDirectory()
        {
            ShouldNotify(x => x.SourceDirectory = "srcdir", "SourceDirectory", "IsValid");
        }
        
        private void ShouldNotify(Action<ProjectDetailsViewModel> action, params string[] expectedPropertyNames)
        {
            action(viewModel);
            propertyChanged.Should().BeTrue();

            propertyNames.Count.Should().Be(expectedPropertyNames.Length);
            expectedPropertyNames.ForEach(each => propertyNames.Any(x => x == each).Should().BeTrue($"Didn't find {each} in the list of notified property names"));
        }
    }
}
