using System;
using System.IO;
using FluentAssertions;
using Metropolis.Common.Models;
using Metropolis.Common.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Metropolis.Test.Common.Utilities
{
    [TestClass]
    public class YamlFileDeserializerTest
    {
        [TestMethod]
        public void ShouldDeserialYamlFileIntoObject()
        {
            var testFile = Path.Combine(Environment.CurrentDirectory, "TestFiles/metro.yml");
            var fileDeserializer = new YamlFileDeserializer<MetricsCommandArguments>();
            var commandArgument = fileDeserializer.Deserialize(testFile);

            commandArgument.ProjectName.Should().Be("Test");
            commandArgument.RepositorySourceType.Should().Be(RepositorySourceType.CSharp);
            commandArgument.EcmaScriptDialect.Should().Be(EslintPasringOptions.DEFAULT);
            commandArgument.ProjectFolder.Should().Be(@"I\am\the\project\folder\location");
            commandArgument.SourceDirectory.Should().Be(@"I\am\the\source\directory");
            commandArgument.IgnoreFile.Should().Be(@"I\am\the\file.metropolisignore");
            commandArgument.MetricsOutputFolder.Should().Be(@"I\am\the\metrics\output\folder");
            commandArgument.BuildOutputFolder.Should().Be(@"I\am\the\build\output\folder");
            commandArgument.ProjectFile.Should().Be(@"I\am\the\file.sln");
        }
    }
}