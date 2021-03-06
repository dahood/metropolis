﻿using System.Text.RegularExpressions;
using Metropolis.Api.Domain;
using Metropolis.Common.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.EsLint
{
    public class EsLintComplexityReader : CheckStyleBaseReader, ICheckStylesMemberReader
    {
        private readonly Regex nameRegex = new Regex("'",RegexOptions.IgnorePatternWhitespace|RegexOptions.Compiled);

        public override string Source => EslintSources.Complexity;

        public EsLintComplexityReader() : base(IntRegex)
        {
        }

        public void Read(Member member, CheckStylesItem item)
        {
            member.Name = nameRegex.IsMatch(item.Message) ? nameRegex.Split(item.Message)[1] : string.Format("Method on line: {0}", item.Line);
            member.CylomaticComplexity = Parser.Match(item.Message).Value.AsInt();
        }
    }
}