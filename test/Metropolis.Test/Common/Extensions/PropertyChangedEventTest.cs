using System;
using FluentAssertions;
using Metropolis.Common.Models;
using Metropolis.ViewModels;
using NUnit.Framework;

namespace Metropolis.Test.Common.Extensions
{
    [TestFixture]
    public class PropertyChangedEventTest
    {
        ProjectDetailsViewModel viewModel;
        private bool eventCalled;

        [SetUp]
        public void SetUp()
        {
            viewModel = new ProjectDetailsViewModel();
            eventCalled = false;
        }

        [Test]
        public void NotifyOnName()
        {
            RunTest("ProjectName", d => d.ProjectName = "I changed");
        }

        [Test]
        public void NotifyOnRepositorySourceType()
        {
            RunTest("RepositorySourceType", d => d.RepositorySourceType = RepositorySourceType.Java);
        }

        [Test]
        public void NotifyOnSourceDirectory()
        {
            RunTest("SourceDirectory", d => d.SourceDirectory = @"h:\hdrive");
        }

        [Test]
        public void NotifyOnIgnoreFile()
        {
            RunTest("IgnoreFile", d => d.IgnoreFile = @"h:\ignore\me.ignore");
        }

        [Test]
        public void NotifyOnMetricsOutputDirectory()
        {
            RunTest("MetricsOutputDirectory", d => d.MetricsOutputDirectory = @"h:\output_dir");
        }

        private void RunTest(string expectedPropertyName, Action<ProjectDetailsViewModel> action)
        {
            viewModel.PropertyChanged += (s, e) =>
            {
                eventCalled = true;
                e.PropertyName.Should().Be(expectedPropertyName);
            };

            action(viewModel);
            eventCalled.Should().BeTrue();
        }
    }
}
