using System;
using FluentAssertions;
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
        private string propertyName;

        [SetUp]
        public void SetUp()
        {
            viewModel = new ProjectDetailsViewModel();

            propertyChanged = false;
            propertyName = string.Empty;

            viewModel.PropertyChanged += (s, e) =>
                                        {
                                            propertyChanged = true;
                                            propertyName = e.PropertyName;
                                        };
        }

        [Test]
        public void NotifiesOnProjectName()
        {
            ShouldNotify("ProjectName", x => x.ProjectName = "change me");
        }

        [Test]
        public void NotifiesOnIgnoreFile()
        {
            ShouldNotify("IgnoreFile", x => x.IgnoreFile = "change me");
        }
        
        [Test]
        public void NotifiesOnMetricsRepositorySourceType()
        {
            ShouldNotify("RepositorySourceType", x => x.RepositorySourceType = RepositorySourceType.ECMA);
        }

        [Test]
        public void NotifiesOnMetricsSourceDirectory()
        {
            ShouldNotify("SourceDirectory", x => x.SourceDirectory = "srcdir");
        }
        
        private void ShouldNotify(string expectedPropertyName, Action<ProjectDetailsViewModel> action)
        {
            action(viewModel);
            propertyChanged.Should().BeTrue();
            propertyName.Should().Be(expectedPropertyName);
        }
    }
}
