using System;
using FluentAssertions;
using Metropolis.Api.Core.Domain;
using Metropolis.Api.Core.Parsers.XmlParsers.CheckStyles;
using Metropolis.Api.Core.Parsers.XmlParsers.CheckStyles.Parsers;
using NUnit.Framework;

namespace Metropolis.Test.Api.Core.Parsers.CheckStyles.CheckStylesMemberParser
{
    public abstract class CheckStylesBaseTest
    {
        protected Member Member;
        protected Instance ParentInstance;

        [SetUp]
        public void SetUp()
        {
            ParentInstance = new Instance("ShoppingCart", "ShoppingCart");
            Member = new Member(string.Empty, 0, 0, 0);
        }

        protected static T MemberParserFor<T>() where T : ICheckStylesMemberParser, new()
        {
            return new T();
        }

        protected static T ClassParserFor<T>() where T : ICheckStylesClassParser, new()
        {
            return new T();
        }

        protected void RunMemberTest<T>(string message, string expectedSource, Action<Member> action,
            CheckStylesItem item = null) where T : ICheckStylesMemberParser, new()
        {
            var checkStylesItem = item ?? new CheckStylesItem {Message = message};
            var parser = MemberParserFor<T>();
            parser.Parse(Member, checkStylesItem);

            parser.Source.Should().Be(expectedSource);
            action(Member);
        }

        protected void RunClassTest<T>(string message, string expectedSource, Action<Instance> action,
            CheckStylesItem item = null) where T : ICheckStylesClassParser, new()
        {
            var checkStylesItem = item ?? new CheckStylesItem {Message = message};
            var parser = ClassParserFor<T>();
            parser.Parse(ParentInstance, checkStylesItem);

            parser.Source.Should().Be(expectedSource);
            action(ParentInstance);
        }
    }
}