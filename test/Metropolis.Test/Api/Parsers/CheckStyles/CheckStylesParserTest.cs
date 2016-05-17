using System.Linq;
using FluentAssertions;
using Metropolis.Api.Domain;
using Metropolis.Api.Parsers.XmlParsers.CheckStyles;
using Metropolis.Test.Fixtures;
using Metropolis.Test.Utilities;
using NUnit.Framework;

namespace Metropolis.Test.Api.Parsers.CheckStyles
{
    [TestFixture]
    public class EslintCheckStylesParserTest : BaseCheckstylesParserTest
    {
        protected override string FileName => $"eslint_checkstyles_fixture.xml";
        protected override string CheckStylesFixture => MetricsDataFixture.CheckStylesReactFixture;

        protected override CheckStylesParser CreateParser()
        {
            return CheckStylesParser.EslintParser;
        }

        [Test]
        public void CanParse()
        {
            var result = Parser.Parse(FileName);
            result.Should().NotBeNull();

            result.AllInstances.Count.Should().Be(2);

            //ReactLink.js expectations
            VerifyHasInstanceWithMembers(result, "ReactLink.js", 
                                                new Member("createLinkTypeChecker", 2, 2, 0) {NumberOfParameters = 1},
                                                new Member("ReactLink", 2, 1, 0) {NumberOfParameters = 2});

            //ReactComponentWithPureRenderMixin expectations
            VerifyHasInstanceWithMembers(result, "ReactComponentWithPureRenderMixin.js",
                                                new Member("shouldComponentUpdate", 1, 1, 0) { NumberOfParameters = 2 });
        }

        private void VerifyHasInstanceWithMembers(CodeBase result, string name, params Member[] expectedMembers)
        {
            var instance = result.AllInstances.FirstOrDefault(x => x.Name.EndsWith(name));
            instance.Should().NotBeNull();
            instance.Members.Count.Should().Be(expectedMembers.Length);

            foreach (var expectedMember in expectedMembers)
            {
                AssertHasEslintMember(instance, expectedMember);
            }
        }
        
        private static void AssertHasEslintMember(Instance actual, Member expectedMember)
        {
            actual.Should().NotBeNull();
            var member = actual.Members.FirstOrDefault(x => x.Name == expectedMember.Name);

            Validate.Begin()
                    .IsNotNull(member, "Member not found")
                    .Check()
                    .IsEqual(member.LinesOfCode, expectedMember.LinesOfCode, "LOC")
                    .IsEqual(member.CylomaticComplexity, expectedMember.CylomaticComplexity, "complexity")
                    .IsEqual(member.NumberOfParameters, expectedMember.NumberOfParameters, "number of parameters")
                    .Check();
        }
    }
}
