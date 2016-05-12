using FluentAssertions;
using Metropolis.Api.Core.Parsers.XmlParsers.CheckStyles.Parsers.EsLint;
using NUnit.Framework;

namespace Metropolis.Test.Api.Core.Parsers.CheckStyles.CheckStylesMemberParser
{
    [TestFixture]
    public class EsLintCheckStylesParserTests : CheckStylesBaseTest
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
            RunMemberTest<EsLintDefaultCaseParser>(DefaultCaseMessage, EslintSources.DefaultCase,
                m => m.MissingDefaultCase.Should().Be(1));
        }

        [Test]
        public void CanParseNoFallthrough()
        {
            RunMemberTest<EsLintCaseNoFallThroughParser>(NofallthroughMessage, EslintSources.CaseNoFallThrough,
                m => m.NoFallthrough.Should().Be(1));
        }

        [Test]
        public void CanParseNumberOfParameters()
        {
            RunMemberTest<EsLintNumberOfParametersParser>(ParametersMessage, EslintSources.NumberOfParameters,
                m => m.NumberOfParameters.Should().Be(2));
        }

        [Test]
        public void CanParseNumberOfStatements()
        {
            RunMemberTest<EsLintNumberOfStatmentsParser>(StatementsMessage, EslintSources.MemberNumberOfStatements,
                m => m.LinesOfCode.Should().Be(17));
        }

        [Test]
        public void ShouldParseFunctionNameAndComplexity()
        {
            RunMemberTest<EsLintComplexityParser>(ComplexityMessage, EslintSources.Complexity, m =>
            {
                m.Name.Should().Be("jqLiteAcceptsData");
                m.CylomaticComplexity.Should().Be(3);
            });
        }
    }
}