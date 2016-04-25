using FluentAssertions;
using Metropolis.Parsers.XmlParsers.CheckStyles;
using Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl;
using NUnit.Framework;

namespace Test.Metropolis.Parsers.CheckStyles.CheckStylesMemberParser
{
    [TestFixture]
    public class PuppyCrawlCheckStylesParserTests : CheckStylesBaseTest
    {
        private const string ComplexityMessage = "Class Fan-Out Complexity is 13 (max allowed is 0).";
        private const string LinesOfCodeMessage = "Method length is 66 lines (max allowed is 0).";
        private const string NumberOfParametersMessage = "More than 0 parameters (found 123).";
        private const string BooleanComplexityMessage = "Boolean expression complexity is 4 (max allowed is 3).";
        private const string NestedTryDepthMessage = "Nested try depth is 1 (max allowed is 0).";
        private const string NestedIfDepthMessage = "Nested if-else depth is 5 (max allowed is 0).";
        private const string AnonymousInnerClassLengthMessage = "Anonymous inner class length is 19 lines (max allowed is 0).";
        private const string ClassFanOutComplexitymMessage = "Class Fan-Out Complexity is 11 (max allowed is 0).";
        private const string ClassDataAbstractionCouplingMessage =
            "Class Data Abstraction Coupling is 22 (max allowed is 0) classes [AuthScope, BasicNameValuePair, DefaultHttpClient, FailedLoginException, HttpGet, HttpPost, JSONObject, NTCredentials, NTLMSchemeFactory, PoolingClientConnectionManager, SSLSocketFactory, SSOException, Scheme, SchemeRegistry, SecureCMSException, SecureRandom, TrustManager, URI, UrlEncodedFormEntity, UserInfo, UsernamePasswordCredentials, X509TrustManager].";

        [Test]
        public void ShouldParseFunctionNameAndComplexity()
        {
            ParserFor<PuppyCrawlComplexityParser>().Parse(Member, new CheckStylesItem { Message = ComplexityMessage, Line = 1, Column = 2});
            Member.Name.Should().Be("1-2");
            Member.CylomaticComplexity.Should().Be(13);
        }

        [Test]
        public void ShouldParseLinesOfCode()
        {
            ParserFor<PuppyCrawlLinesOfCodeParser>().Parse(Member, new CheckStylesItem { Message = LinesOfCodeMessage, Line = 1, Column = 2});
            Member.LinesOfCode.Should().Be(66);
        }

        [Test]
        public void ShouldParseNumberOfParameters()
        {
            ParserFor<PuppyCrawlNumberOfParametersParser>().Parse(Member, new CheckStylesItem { Message = NumberOfParametersMessage, Line = 1, Column = 2});
            Member.NumberOfParameters.Should().Be(123);
        }

        [Test]
        public void ShouldParseMissingSwitchDefaultCheck()
        {
            var puppyCrawlDefaultCaseParser = ParserFor<PuppyCrawlDefaultCaseParser>();
            var checkStylesItem = new CheckStylesItem { Message = NumberOfParametersMessage, Line = 1, Column = 2};
            puppyCrawlDefaultCaseParser.Parse(Member, checkStylesItem);
            puppyCrawlDefaultCaseParser.Parse(Member, checkStylesItem);
            Member.MissingDefaultCase.Should().Be(2);
        }
        
        [Test]
        public void ShouldParseBooleanExpressionComplexity()
        {
            ParserFor<PuppyCrawlBooleanExpressionComplexityParser>().Parse(Member, new CheckStylesItem { Message = BooleanComplexityMessage, Line = 1, Column = 2 });
            Member.BooleanExpressionComplexity.Should().Be(4);
        }
        
        [Test]
        public void ShouldParseNestedTryDepth()
        {
            ParserFor<PupyyCrawlNestedTryDepthParser>().Parse(Member, new CheckStylesItem { Message = NestedTryDepthMessage, Line = 1, Column = 2 });
            Member.NestedTryDepth.Should().Be(1);
        }
        
        [Test]
        public void ShouldParseNestedIfDepth()
        {
            ParserFor<PupyyCrawlNestedIfDepthParser>().Parse(Member, new CheckStylesItem { Message = NestedIfDepthMessage, Line = 1, Column = 2 });
            Member.NestedIfDepth.Should().Be(5);
        }
        
        [Test]
        public void ShouldParseAnonymousInnerClassLenthestedIfDepth()
        {
            ParserFor<PuppyCrawlAnonymousInnerClassLenthParser>().Parse(Member, new CheckStylesItem { Message = AnonymousInnerClassLengthMessage, Line = 1, Column = 2 });
            Member.AnonymousInnerClassLenth.Should().Be(19);
        }
        
        [Test]
        public void ShouldParsePuppyCrawlClassFanOutComplexity()
        {
            ParserFor<PuppyCrawlClassFanOutComplexityParser>().Parse(Member, new CheckStylesItem { Message = ClassFanOutComplexitymMessage, Line = 1, Column = 2 });
            Member.ClassFanOutComplexity.Should().Be(11);
        }
        
        [Test]
        public void ShouldParseClassDataAbstractionCoupling()
        {
            ParserFor<PuppyCrawlClassDataAbstractionCouplingParser>().Parse(Member, new CheckStylesItem { Message = ClassDataAbstractionCouplingMessage, Line = 1, Column = 2 });
            Member.ClassDataAbstractionCoupling.Should().Be(22);
        }
    }
}
