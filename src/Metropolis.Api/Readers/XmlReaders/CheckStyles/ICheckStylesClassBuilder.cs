using System.Collections.Generic;
using System.IO;
using System.Linq;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;
using Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles
{
    public interface ICheckStylesClassBuilder
    {
        Instance Build(string key, List<CheckStylesItem> cls);
    }

    public abstract class BaseCheckStylesClassBuilder : ICheckStylesClassBuilder
    {
        protected readonly IEnumerable<ICheckStylesClassReader> ClassParsers;
        protected readonly IEnumerable<ICheckStylesMemberReader> MemberParsers;

        protected BaseCheckStylesClassBuilder(IEnumerable<ICheckStylesClassReader> classParsers, IEnumerable<ICheckStylesMemberReader> memberParsers)
        {
            ClassParsers = classParsers;
            MemberParsers = memberParsers;
        }

        public Instance Build(string key, List<CheckStylesItem> items)
        {
            var grouped = (from item in items
                           group item by new {item.Line} into grp
                           select new { grp.Key, Metrics = grp }).ToList();

            var members = grouped.Select(each => {
                var member = new Member($"line: {each.Key}", 0, 0, 0);

                (from p in MemberParsers
                 join item in each.Metrics
                   on p.Source equals item.Source
                 select new { Parser = p, Item = item }
                ).ForEach(e => e.Parser.Read(member, e.Item));

                return member;
            }).ToList();

            var type = ParseClass(key, members);

             //add class level metrics
            grouped.ForEach(each => {

                (from p in ClassParsers
                 join item in each.Metrics
                   on p.Source equals item.Source
                 select new { Parser = p, Item = item }
                ).ForEach(e => e.Parser.Read(type, e.Item));
            });

            return type;
        }

        private static Instance ParseClass(string key, IEnumerable<Member> members)
        {
            var parts = key.Split('\\').ToList();
            var name = parts.Last();
            parts.RemoveRange(parts.Count - 1, 1);
            var ns = string.Join("\\", parts);
            return new Instance(ns, name, members) {PhysicalPath = Path.Combine(ns, name) };
        }
    }
}