using System;
using Metropolis.Api.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.Java
{
    /// <summary>
    ///     Java Checkstyle parser automation
    ///     TODO: check if Java is installed or not
    ///     For more info checkout: http://checkstyle.sourceforge.net/cmdline.html#Usage_by_Classpath_update
    /// </summary>
    public class PuppyCrawlerCheckstyleCollectionStep : BaseCollectionStep
    {
        private const string CheckstyleCommand = @"java -cp {0} com.puppycrawl.tools.checkstyle.Main -c {1} -f xml -o {2} {3}";

        public PuppyCrawlerCheckstyleCollectionStep() : base(false)
        {
        }

        public override string MetricsType => "Java Checkstyle";
        public override string Extension => ".xml";
        public override ParseType ParseType => ParseType.PuppyCrawler;

        public override string PrepareCommand(MetricsCommandArguments args, MetricsResult result)
        {
            var cmd = CheckstyleCommand.FormatWith(
                AppDomain.CurrentDomain.BaseDirectory + "*.jar", // include all jars into the class path
                AppDomain.CurrentDomain.BaseDirectory + "metropolis_checkstyle_metrics.xml", // metropolis collection settings for checkstyle
                result.MetricsFile, // output xml file
                args.SourceDirectory // source directory to scan
                );

            return cmd;
        }
    }
}