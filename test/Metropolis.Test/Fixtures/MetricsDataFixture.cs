namespace Metropolis.Test.Fixtures
{
    public static class MetricsDataFixture
    {
        public static string CheckStylesReactFixture =>
@"<?xml version='1.0' encoding='utf-8'?>
<checkstyle version='4.3'>
	<file name='C:\OpenSource\javascript\react\src\addons\link\ReactLink.js'>
		<error line='43' column='1' severity='error' message='This function has too many parameters (2). Maximum allowed is 0. (max-params)' source='eslint.rules.max-params' />
		<error line='43' column='1' severity='error' message='This function has too many statements (2). Maximum allowed is 0. (max-statements)' source='eslint.rules.max-statements' />
		<error line='43' column='1' severity='error' message='Function &apos;ReactLink&apos; has a complexity of 1. (complexity)' source='eslint.rules.complexity' />
		<error line='56' column='1' severity='error' message='This function has too many parameters (1). Maximum allowed is 0. (max-params)' source='eslint.rules.max-params' />
		<error line='56' column='1' severity='error' message='This function has too many statements (2). Maximum allowed is 0. (max-statements)' source='eslint.rules.max-statements' />
		<error line='56' column='1' severity='error' message='Function &apos;createLinkTypeChecker&apos; has a complexity of 2. (complexity)' source='eslint.rules.complexity' />
	</file>
	<file name='C:\OpenSource\javascript\react\src\addons\ReactComponentWithPureRenderMixin.js'>
		<error line='41' column='26' severity='error' message='This function has too many parameters (2). Maximum allowed is 0. (max-params)' source='eslint.rules.max-params' />
		<error line='41' column='26' severity='error' message='This function has too many statements (1). Maximum allowed is 0. (max-statements)' source='eslint.rules.max-statements' />
		<error line='41' column='26' severity='error' message='Function &apos;shouldComponentUpdate&apos; has a complexity of 1. (complexity)' source='eslint.rules.complexity' />
	</file>
</checkstyle>";

        public static string FxCopMetricsMetricsData =>
@"<?xml version='1.0' encoding='utf-8'?>
<CodeMetricsReport Version='14.0'>
  <Targets>
    <Target Name='C:\develop\metropolis\dist\Metropolis.Api.dll'>
      <Modules>
        <Module Name='Metropolis.Api.dll' AssemblyVersion='1.0.0.0' FileVersion='1.0.0.0'>
          <Metrics>
            <Metric Name='MaintainabilityIndex' Value='83' />
            <Metric Name='CyclomaticComplexity' Value='927' />
            <Metric Name='ClassCoupling' Value='214' />
            <Metric Name='DepthOfInheritance' Value='3' />
            <Metric Name='LinesOfCode' Value='1,669' />
          </Metrics>
          <Namespaces>
            <Namespace Name='Metropolis.Api.Utilities'>
              <Metrics>
                <Metric Name='MaintainabilityIndex' Value='83' />
                <Metric Name='CyclomaticComplexity' Value='30' />
                <Metric Name='ClassCoupling' Value='5' />
                <Metric Name='DepthOfInheritance' Value='1' />
                <Metric Name='LinesOfCode' Value='50' />
              </Metrics>
              <Types>
                <Type Name='Clock'>
                  <Metrics>
                    <Metric Name='MaintainabilityIndex' Value='91' />
                    <Metric Name='CyclomaticComplexity' Value='8' />
                    <Metric Name='ClassCoupling' Value='3' />
                    <Metric Name='DepthOfInheritance' Value='1' />
                    <Metric Name='LinesOfCode' Value='8' />
                  </Metrics>
                  <Members>
                    <Member Name='Freeze() : void'>
                      <Metrics>
                        <Metric Name='MaintainabilityIndex' Value='92' />
                        <Metric Name='CyclomaticComplexity' Value='1' />
                        <Metric Name='ClassCoupling' Value='2' />
                        <Metric Name='LinesOfCode' Value='1' />
                      </Metrics>
                    </Member>
                    <Member Name='Today.get() : DateTime'>
                      <Metrics>
                        <Metric Name='MaintainabilityIndex' Value='92' />
                        <Metric Name='CyclomaticComplexity' Value='1' />
                        <Metric Name='ClassCoupling' Value='2' />
                        <Metric Name='LinesOfCode' Value='1' />
                      </Metrics>
                    </Member>
                  </Members>
                </Type>               
              </Types>
            </Namespace>
            <Namespace Name='Metropolis.Api.Services'>
              <Metrics>
                <Metric Name='MaintainabilityIndex' Value='89' />
                <Metric Name='CyclomaticComplexity' Value='42' />
                <Metric Name='ClassCoupling' Value='31' />
                <Metric Name='DepthOfInheritance' Value='1' />
                <Metric Name='LinesOfCode' Value='63' />
              </Metrics>
              <Types>
                <Type Name='AnalysisServices'>
                  <Metrics>
                    <Metric Name='MaintainabilityIndex' Value='72' />
                    <Metric Name='CyclomaticComplexity' Value='4' />
                    <Metric Name='ClassCoupling' Value='15' />
                    <Metric Name='DepthOfInheritance' Value='1' />
                    <Metric Name='LinesOfCode' Value='12' />
                  </Metrics>
                  <Members>
                    <Member Name='AnalysisServices()'>
                      <Metrics>
                        <Metric Name='MaintainabilityIndex' Value='92' />
                        <Metric Name='CyclomaticComplexity' Value='1' />
                        <Metric Name='ClassCoupling' Value='2' />
                        <Metric Name='LinesOfCode' Value='1' />
                      </Metrics>
                    </Member>                  
                    <Member Name='Analyze(MetricsCommandArguments) : CodeBase'>
                      <Metrics>
                        <Metric Name='MaintainabilityIndex' Value='62' />
                        <Metric Name='CyclomaticComplexity' Value='2' />
                        <Metric Name='ClassCoupling' Value='12' />
                        <Metric Name='LinesOfCode' Value='9' />
                      </Metrics>
                    </Member>
                  </Members>
                </Type>                                                   
              </Types>
            </Namespace>
          </Namespaces>
        </Module>
      </Modules>
    </Target>
  </Targets>
</CodeMetricsReport>
";
    }
}
