using System;
using FluentAssertions;
using Metropolis.Api.Domain;
using Metropolis.Api.Readers.XmlReaders.CheckStyles;
using Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers;
using NUnit.Framework;

namespace Metropolis.Test.Api.Readers.CheckStyles.CheckStylesMemberReaders
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

        protected static T MemberParserFor<T>() where T : ICheckStylesMemberReader, new()
        {
            return new T();
        }

        protected static T ClassParserFor<T>() where T : ICheckStylesClassReader, new()
        {
            return new T();
        }

        protected void RunMemberTest<T>(string message, string expectedSource, Action<Member> action,
            CheckStylesItem item = null) where T : ICheckStylesMemberReader, new()
        {
            var checkStylesItem = item ?? new CheckStylesItem {Message = message};
            var parser = MemberParserFor<T>();
            parser.Read(Member, checkStylesItem);

            parser.Source.Should().Be(expectedSource);
            action(Member);
        }

        protected void RunClassTest<T>(string message, string expectedSource, Action<Instance> action,
            CheckStylesItem item = null) where T : ICheckStylesClassReader, new()
        {
            var checkStylesItem = item ?? new CheckStylesItem {Message = message};
            var parser = ClassParserFor<T>();
            parser.Read(ParentInstance, checkStylesItem);

            parser.Source.Should().Be(expectedSource);
            action(ParentInstance);
        }
    }
}