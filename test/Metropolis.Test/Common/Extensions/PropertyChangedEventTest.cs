using System;
using System.ComponentModel;
using FluentAssertions;
using Metropolis.Common.Extensions;
using NUnit.Framework;

namespace Metropolis.Test.Common.Extensions
{
    [TestFixture]
    public class PropertyChangedEventTest
    {
        private TestViewModel viewModel;
        private bool eventCalled;

        [SetUp]
        public void SetUp()
        {
            viewModel = new TestViewModel();
            eventCalled = false;
        }

        [Test]
        public void NotifyOnName()
        {
            RunTest("CodeBag", d => d.Name = "I changed");
        }

        [Test]
        public void NotifyOnAge()
        {
            RunTest("Age", d => d.Age = 21);
        }
        
        private void RunTest(string expectedPropertyName, Action<TestViewModel> action)
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

    public class TestViewModel : INotifyPropertyChanged
    {
        private string name;
        private int age;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged.Notify(this, x => x.Name);
            }
        }
        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                PropertyChanged.Notify(this, x => x.Age);
            }
        }
    }
}
