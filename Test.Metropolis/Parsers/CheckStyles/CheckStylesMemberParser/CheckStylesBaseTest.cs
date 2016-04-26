using System;
using FluentAssertions;
using Metropolis.Domain;
using Metropolis.Parsers.XmlParsers.CheckStyles;
using Metropolis.Parsers.XmlParsers.CheckStyles.Parsers;
using NUnit.Framework;

namespace Test.Metropolis.Parsers.CheckStyles.CheckStylesMemberParser
{
    public abstract class CheckStylesBaseTest
    {
        protected Class ParentClass;
        protected Member Member;

        [SetUp]
        public void SetUp()
        {
            ParentClass = new Class("ShoppingCart", "ShoppingCart");
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

        protected void RunMemberTest<T>(string message, string expectedSource, Action<Member> action, CheckStylesItem item = null) where T : ICheckStylesMemberParser, new()
        {
            var checkStylesItem = item??new CheckStylesItem { Message = message };
            var parser = MemberParserFor<T>();
            parser.Parse(Member, checkStylesItem);

            parser.Source.Should().Be(expectedSource);
            action(Member);
        }

        protected void RunClassTest<T>(string message, string expectedSource, Action<Class> action, CheckStylesItem item = null) where T : ICheckStylesClassParser, new()
        {
            var checkStylesItem = item??new CheckStylesItem { Message = message };
            var parser = ClassParserFor<T>();
            parser.Parse(ParentClass, checkStylesItem);

            parser.Source.Should().Be(expectedSource);
            action(ParentClass);
        }
    }
}