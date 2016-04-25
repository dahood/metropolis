﻿using System.Collections.Generic;
using System.Linq;
using Metropolis.Domain;
using Metropolis.Extensions;
using Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers;

namespace Metropolis.Parsers.XmlParsers.CheckStyles
{
    public interface ICheckStylesClassBuilder
    {
        Class Build(string key, List<CheckStylesItem> cls);
    }

    public abstract class BaseCheckStylesClassBuilder : ICheckStylesClassBuilder
    {
        protected readonly IEnumerable<ICheckStylesMemberParser> Parsers;

        protected BaseCheckStylesClassBuilder(IEnumerable<ICheckStylesMemberParser> memberParsers)
        {
            Parsers = memberParsers;
        }

        public Class Build(string key, List<CheckStylesItem> items)
        {
            var grouped = (from item in items
                           group item by new { Line = item.Line, Column = item.Column } into grp
                           select new { grp.Key, Metrics = grp }).ToList();

            var members = grouped.Select(each => {
                var member = new Member("unknown", 0, 0, 0);

                (from p in Parsers
                 join item in each.Metrics
                   on p.Source equals item.Source
                 select new { Parser = p, Item = item }
                ).ForEach(e => e.Parser.Parse(member, e.Item));

                return member;
            }).ToList();

            return ParseClass(key, members);
        }

        private static Class ParseClass(string key, IEnumerable<Member> members)
        {
            var parts = key.Split('\\').ToList();
            var name = parts.Last();
            parts.RemoveRange(parts.Count - 1, 1);
            return new Class(string.Join(".", parts), name, members);
        }
    }
}