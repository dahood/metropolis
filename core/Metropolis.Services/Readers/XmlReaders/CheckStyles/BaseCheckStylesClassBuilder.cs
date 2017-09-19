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

        protected abstract ICheckStylesMemberReader MethodLengthSourceType { get; }

        public Instance Build(string filename, List<CheckStylesItem> allCheckstyleItems)
        {
            var allMethods = new List<Member>();
            var allMethodLengths = allCheckstyleItems.Where(x => x.Source == MethodLengthSourceType.Source).ToList();

            foreach (var methodItem in allMethodLengths)
            {
                var member = new Member($"Method on Line: {methodItem.Line}", 0, 0, 0);
                MethodLengthSourceType.Read(member, methodItem);

                var otherMemberAttributes = allCheckstyleItems.Where(x => member.StartLine <= x.Line && x.Line <= member.EndLine
                                                                          && x.Source != MethodLengthSourceType.Source);

                (from p in MemberParsers
                    join item in otherMemberAttributes
                        on p.Source equals item.Source
                    select new {Parser = p, Item = item}).ForEach(e => e.Parser.Read(member, e.Item));

                allMethods.Add(member);
            }

            var type = ParseClass(filename, allMethods);
            var grouped = (from item in allCheckstyleItems
                group item by new {item.Line}
                into grp
                select new {grp.Key, Metrics = grp}).ToList();

            //add class level metrics
            grouped.ForEach(each =>
            {
                (from p in ClassParsers
                    join item in each.Metrics
                        on p.Source equals item.Source
                    select new {Parser = p, Item = item}
                    ).ForEach(e => e.Parser.Read(type, e.Item));
            });

            return type;
        }

        private static Instance ParseClass(string fullFileName, IEnumerable<Member> members)
        {
            var fileName = Path.GetFileName(fullFileName);
            var className = Path.GetExtension(fullFileName);
            var packageLocation = Path.GetDirectoryName(fullFileName);
            var packageName = packageLocation.Split(':').Length > 1 ? packageLocation.Split(':')[1] : packageLocation;
            packageName = packageName.Replace(Path.DirectorySeparatorChar, '.'); 
            var codeBag = new CodeBag(packageName, CodeBagType.Package, packageLocation);

            return InstanceBuilder.Build(codeBag, className, new Location(Path.Combine(packageLocation, fileName)), members);
        }
    }
}