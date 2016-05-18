﻿namespace Metropolis.Api.Parsers.XmlReaders.CheckStyles.Parsers.PuppyCrawl
{
    public static class PuppyCrawlSources
    {
        public static string FanOutComplexity => "com.puppycrawl.tools.checkstyle.checks.metrics.ClassFanOutComplexityCheck";
        public static string MethodLength => "com.puppycrawl.tools.checkstyle.checks.sizes.MethodLengthCheck";
        public static string NumberOfParameters => "com.puppycrawl.tools.checkstyle.checks.sizes.ParameterNumberCheck";
        public static string MissingSwitchDefault => "com.puppycrawl.tools.checkstyle.checks.coding.MissingSwitchDefaultCheck";
        public static string BooleanExpressionComplexity => "com.puppycrawl.tools.checkstyle.checks.metrics.BooleanExpressionComplexityCheck";
        public static string NestedTryDepth => "com.puppycrawl.tools.checkstyle.checks.coding.NestedTryDepthCheck";
        public static string NestedIfDepth => "com.puppycrawl.tools.checkstyle.checks.coding.NestedIfDepth";
        public static string AnonymousInnerClassLength => "com.puppycrawl.tools.checkstyle.checks.sizes.AnonInnerLengthCheck";
        public static string ClassFanOutComplexity => "com.puppycrawl.tools.checkstyle.checks.metrics.ClassFanOutComplexityCheck";
        public static string ClassDataAbstractionCoupling => "com.puppycrawl.tools.checkstyle.checks.metrics.ClassDataAbstractionCouplingCheck";
    }
}