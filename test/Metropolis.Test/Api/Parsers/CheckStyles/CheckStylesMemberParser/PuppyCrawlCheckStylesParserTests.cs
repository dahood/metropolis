using FluentAssertions;
using Metropolis.Api.Readers.XmlReaders.CheckStyles;
using Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.PuppyCrawl;
using Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Class;
using Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Member;
using NUnit.Framework;

namespace Metropolis.Test.Api.Parsers.CheckStyles.CheckStylesMemberParser
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

        private const string AnonymousInnerClassLengthMessage =
            "Anonymous inner class length is 19 lines (max allowed is 0).";

        private const string ClassFanOutComplexitymMessage = "Class Fan-Out Complexity is 11 (max allowed is 0).";

        private const string ClassDataAbstractionCouplingMessage =
            "Class Data Abstraction Coupling is 22 (max allowed is 0) classes [AuthScope, BasicNameValuePair, DefaultHttpClient, FailedLoginException, HttpGet, HttpPost, JSONObject, NTCredentials, NTLMSchemeFactory, PoolingClientConnectionManager, SSLSocketFactory, SSOException, Scheme, SchemeRegistry, SecureCMSException, SecureRandom, TrustManager, URI, UrlEncodedFormEntity, UserInfo, UsernamePasswordCredentials, X509TrustManager].";

        [Test]
        public void ShouldParseAnonymousInnerClassLenthestedIfDepth()
        {
            RunClassTest<PuppyCrawlAnonymousInnerClassLenthReader>(AnonymousInnerClassLengthMessage,
                PuppyCrawlSources.AnonymousInnerClassLength,
                m => m.AnonymousInnerClassLength.Should().Be(19));
        }

        [Test]
        public void ShouldParseBooleanExpressionComplexity()
        {
            RunMemberTest<PuppyCrawlBooleanExpressionComplexityReader>(BooleanComplexityMessage,
                PuppyCrawlSources.BooleanExpressionComplexity,
                m => m.BooleanExpressionComplexity.Should().Be(4));
        }

        [Test]
        public void ShouldParseClassDataAbstractionCoupling()
        {
            RunClassTest<PuppyCrawlClassDataAbstractionCouplingReader>(ClassDataAbstractionCouplingMessage,
                PuppyCrawlSources.ClassDataAbstractionCoupling,
                m => m.ClassDataAbstractionCoupling.Should().Be(22));
        }

        [Test]
        public void ShouldParseFunctionNameAndComplexity()
        {
            RunMemberTest<PuppyCrawlComplexityReader>(ComplexityMessage, PuppyCrawlSources.FanOutComplexity, m =>
            {
                m.Name.Should().Be("1-2");
                m.CylomaticComplexity.Should().Be(13);
            },
                new CheckStylesItem {Message = ComplexityMessage, Line = 1, Column = 2});
        }

        [Test]
        public void ShouldParseLinesOfCode()
        {
            RunMemberTest<PuppyCrawlMethodLengthReader>(LinesOfCodeMessage, PuppyCrawlSources.MethodLength,
                m => m.LinesOfCode.Should().Be(66));
        }

        [Test]
        public void ShouldParseMissingSwitchDefaultCheck()
        {
            RunMemberTest<PuppyCrawlDefaultCaseReader>("kaka", PuppyCrawlSources.MissingSwitchDefault,
                m => m.MissingDefaultCase.Should().Be(1));
        }

        [Test]
        public void ShouldParseNestedIfDepth()
        {
            RunMemberTest<PupyyCrawlNestedIfDepthReader>(NestedIfDepthMessage, PuppyCrawlSources.NestedIfDepth,
                m => m.NestedIfDepth.Should().Be(5));
        }

        [Test]
        public void ShouldParseNestedTryDepth()
        {
            RunMemberTest<PupyyCrawlNestedTryDepthReader>(NestedTryDepthMessage, PuppyCrawlSources.NestedTryDepth,
                m => m.NestedTryDepth.Should().Be(1));
        }

        [Test]
        public void ShouldParseNumberOfParameters()
        {
            RunMemberTest<PuppyCrawlNumberOfParametersReader>(NumberOfParametersMessage,
                PuppyCrawlSources.NumberOfParameters,
                m => m.NumberOfParameters.Should().Be(123));
        }

        [Test]
        public void ShouldParsePuppyCrawlClassFanOutComplexity()
        {
            RunClassTest<PuppyCrawlClassFanOutComplexityReader>(ClassFanOutComplexitymMessage,
                PuppyCrawlSources.ClassFanOutComplexity,
                m => m.ClassFanOutComplexity.Should().Be(11));
        }
    }
}