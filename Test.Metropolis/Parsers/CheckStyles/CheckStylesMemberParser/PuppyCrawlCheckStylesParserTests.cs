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
            RunTest<PuppyCrawlComplexityParser>(ComplexityMessage, PuppyCrawlSources.FanOutComplexity, m =>
                                                                    {
                                                                        m.Name.Should().Be("1-2");
                                                                        m.CylomaticComplexity.Should().Be(13);
                                                                    }, 
                                                                    new CheckStylesItem { Message = ComplexityMessage, Line = 1, Column = 2 });
        }

        [Test]
        public void ShouldParseLinesOfCode()
        {
            RunTest<PuppyCrawlMethodLengthParser>(LinesOfCodeMessage, PuppyCrawlSources.MethodLength, m => m.LinesOfCode.Should().Be(66));
        }

        [Test]
        public void ShouldParseNumberOfParameters()
        {
            RunTest<PuppyCrawlNumberOfParametersParser>(NumberOfParametersMessage, PuppyCrawlSources.NumberOfParameters, 
                                                        m => m.NumberOfParameters.Should().Be(123));
        }

        [Test]
        public void ShouldParseMissingSwitchDefaultCheck()
        {
            RunTest<PuppyCrawlDefaultCaseParser>("kaka", PuppyCrawlSources.MissingSwitchDefault, m => m.MissingDefaultCase.Should().Be(1));
        }
        
        [Test]
        public void ShouldParseBooleanExpressionComplexity()
        {
            RunTest<PuppyCrawlBooleanExpressionComplexityParser>(BooleanComplexityMessage, PuppyCrawlSources.BooleanExpressionComplexity,
                                                                 m => m.BooleanExpressionComplexity.Should().Be(4));
        }
        
        [Test]
        public void ShouldParseNestedTryDepth()
        {
            RunTest<PupyyCrawlNestedTryDepthParser>(NestedTryDepthMessage, PuppyCrawlSources.NestedTryDepth, m => m.NestedTryDepth.Should().Be(1));
        }
        
        [Test]
        public void ShouldParseNestedIfDepth()
        {
            RunTest<PupyyCrawlNestedIfDepthParser>(NestedIfDepthMessage, PuppyCrawlSources.NestedIfDepth, m => m.NestedIfDepth.Should().Be(5));
        }
        
        //[Test]
        //public void ShouldParseAnonymousInnerClassLenthestedIfDepth()
        //{
        //    RunTest<PuppyCrawlAnonymousInnerClassLenthParser>(AnonymousInnerClassLengthMessage, PuppyCrawlSources.AnonymousInnerClassLength, 
        //                                                      m => m.AnonymousInnerClassLenth.Should().Be(19));
        //}
        
        //[Test]
        //public void ShouldParsePuppyCrawlClassFanOutComplexity()
        //{
        //    RunTest<PuppyCrawlClassFanOutComplexityParser>(ClassFanOutComplexitymMessage , PuppyCrawlSources.ClassFanOutComplexity,
        //                                                      m => m.ClassFanOutComplexity.Should().Be(11));
        //}
        
        //[Test]
        //public void ShouldParseClassDataAbstractionCoupling()
        //{
        //    RunTest<PuppyCrawlClassDataAbstractionCouplingParser>(ClassDataAbstractionCouplingMessage, PuppyCrawlSources.ClassDataAbstractionCoupling,
        //                                                          m => m.ClassDataAbstractionCoupling.Should().Be(22));
        //}
    }
}
