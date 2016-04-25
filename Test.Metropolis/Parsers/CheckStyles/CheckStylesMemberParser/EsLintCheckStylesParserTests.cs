using FluentAssertions;
using Metropolis.Parsers.XmlParsers.CheckStyles;
using Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.EsLint;
using NUnit.Framework;

namespace Test.Metropolis.Parsers.CheckStyles.CheckStylesMemberParser
{
    [TestFixture]
    public class EsLintCheckStylesParserTests : CheckStylesBaseTest
    {
        private const string ComplexityMessage = "Function 'jqLiteAcceptsData' has a complexity of 3. (complexity)";
        private const string StatementsMessage = "This function has too many statements (17). Maximum allowed is 10. (max-statements)";
        private const string ParametersMessage = "This function has too many parameters (2). Maximum allowed is 0. (max-params)";
        private const string DefaultCaseMessage = "Expected a default case. (default-case)";
        private const string NofallthroughMessage = "(no-fallthrough)";
        
        [Test]
        public void ShouldParseFunctionNameAndComplexity()
        {
            ParserFor<EsLintComplexityParser>().Parse(Member, new CheckStylesItem {Message = ComplexityMessage});

            Member.Name.Should().Be("jqLiteAcceptsData");
            Member.CylomaticComplexity.Should().Be(3);
        }

        [Test]
        public void CanParseNumberOfStatements()
        {
            ParserFor<EsLintNumberOfStatmentsParser>().Parse(Member, new CheckStylesItem { Message = StatementsMessage });
            Member.LinesOfCode.Should().Be(17);
        }

        [Test]
        public void CanParseNumberOfParameters()
        {
            ParserFor<EsLintNumberOfParametersParser>().Parse(Member, new CheckStylesItem { Message = ParametersMessage });
            Member.NumberOfParameters.Should().Be(2);
        }

        [Test]
        public void CanParseDefaultCaseMissing()
        {
            var parser = ParserFor<EsLintDefaultCaseParser>();
            var checkStylesItem = new CheckStylesItem { Message = DefaultCaseMessage, Source = "eslint.rules.default-case" };

            parser.Source.Should().Be(checkStylesItem.Source);
            parser.Parse(Member, checkStylesItem);
            parser.Parse(Member, checkStylesItem);
            Member.MissingDefaultCase.Should().Be(2);
        }

        [Test]
        public void CanParseNoFallthrough()
        {
            var parser = ParserFor<EsLintCaseNoFallThroughParser>();
            var checkStylesItem = new CheckStylesItem { Message = NofallthroughMessage, Source = "eslint.rules.no-fallthrough" };

            parser.Source.Should().Be(checkStylesItem.Source);
            parser.Parse(Member, checkStylesItem);
            parser.Parse(Member, checkStylesItem);
            parser.Parse(Member, checkStylesItem);
            Member.NoFallthrough.Should().Be(3);
        }
    }
}