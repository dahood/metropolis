using System.Collections.Generic;
using System.IO;
using System.Linq;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;
using Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles
{
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
            //this is wrong

            // group for members should be cyclomatic complexity to establish all members
            // modify that group with all methodlength types to enable LOC & Method length range
            // group for all the member properties we need to collect

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
            //TODO: Gross
            var parts = key.Split('\\').ToList();
            var fileName = parts.Last();
            var className = fileName.Split('.')[0];
            parts.RemoveRange(parts.Count - 1, 1);
            var packageLocation = string.Join("\\", parts);
            parts.RemoveAt(0);
            var packageName = string.Join(".", parts);
            var codeBag = new CodeBag(packageName, CodeBagType.Package, packageLocation);

            
            return InstanceBuilder.Build(codeBag, className, new Location(Path.Combine(packageLocation, fileName)), members);
        }
    }
}