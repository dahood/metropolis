﻿using FluentAssertions;
using Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.EsLint;
using NUnit.Framework;

namespace Metropolis.Test.Api.Readers.CheckStyles.CheckStylesMemberReaders
{
    [TestFixture]
    public class EsLintCheckStylesReaderTests : CheckStylesBaseTest
    {
        private const string ComplexityMessage = "Function 'jqLiteAcceptsData' has a complexity of 3. (complexity)";

        private const string StatementsMessage =
            "This function has too many statements (17). Maximum allowed is 10. (max-statements)";

        private const string ParametersMessage =
            "This function has too many parameters (2). Maximum allowed is 0. (max-params)";

        private const string DefaultCaseMessage = "Expected a default case. (default-case)";
        private const string NofallthroughMessage = "(no-fallthrough)";

        [Test]
        public void CanParseDefaultCaseMissing()
        {
            RunMemberTest<EsLintDefaultCaseReader>(DefaultCaseMessage, EslintSources.DefaultCase,
                m => m.MissingDefaultCase.Should().Be(1));
        }

        [Test]
        public void CanParseNoFallthrough()
        {
            RunMemberTest<EsLintCaseNoFallThroughReader>(NofallthroughMessage, EslintSources.CaseNoFallThrough,
                m => m.NoFallthrough.Should().Be(1));
        }

        [Test]
        public void CanParseNumberOfParameters()
        {
            RunMemberTest<EsLintNumberOfParametersReader>(ParametersMessage, EslintSources.NumberOfParameters,
                m => m.NumberOfParameters.Should().Be(2));
        }

        [Test]
        public void CanParseNumberOfStatements()
        {
            RunMemberTest<EsLintNumberOfStatmentsReader>(StatementsMessage, EslintSources.MemberNumberOfStatements,
                m => m.LinesOfCode.Should().Be(17));
        }

        [Test]
        public void ShouldParseFunctionNameAndComplexity()
        {
            RunMemberTest<EsLintComplexityReader>(ComplexityMessage, EslintSources.Complexity, m =>
            {
                m.Name.Should().Be("jqLiteAcceptsData");
                m.CylomaticComplexity.Should().Be(3);
            });
        }
    }
}