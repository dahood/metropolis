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
    <Target CodeBag='C:\develop\metropolis\dist\Metropolis.Api.dll'>
      <Modules>
        <Module CodeBag='Metropolis.Api.dll' AssemblyVersion='1.0.0.0' FileVersion='1.0.0.0'>
          <Metrics>
            <Metric CodeBag='MaintainabilityIndex' Value='83' />
            <Metric CodeBag='CyclomaticComplexity' Value='927' />
            <Metric CodeBag='ClassCoupling' Value='214' />
            <Metric CodeBag='DepthOfInheritance' Value='3' />
            <Metric CodeBag='LinesOfCode' Value='1,669' />
          </Metrics>
          <Namespaces>
            <Namespace CodeBag='Metropolis.Api.Utilities'>
              <Metrics>
                <Metric CodeBag='MaintainabilityIndex' Value='83' />
                <Metric CodeBag='CyclomaticComplexity' Value='30' />
                <Metric CodeBag='ClassCoupling' Value='5' />
                <Metric CodeBag='DepthOfInheritance' Value='1' />
                <Metric CodeBag='LinesOfCode' Value='50' />
              </Metrics>
              <Types>
                <Type CodeBag='Clock'>
                  <Metrics>
                    <Metric CodeBag='MaintainabilityIndex' Value='91' />
                    <Metric CodeBag='CyclomaticComplexity' Value='8' />
                    <Metric CodeBag='ClassCoupling' Value='3' />
                    <Metric CodeBag='DepthOfInheritance' Value='1' />
                    <Metric CodeBag='LinesOfCode' Value='8' />
                  </Metrics>
                  <Members>
                    <Member CodeBag='Freeze() : void'>
                      <Metrics>
                        <Metric CodeBag='MaintainabilityIndex' Value='92' />
                        <Metric CodeBag='CyclomaticComplexity' Value='1' />
                        <Metric CodeBag='ClassCoupling' Value='2' />
                        <Metric CodeBag='LinesOfCode' Value='1' />
                      </Metrics>
                    </Member>
                    <Member CodeBag='Today.get() : DateTime'>
                      <Metrics>
                        <Metric CodeBag='MaintainabilityIndex' Value='92' />
                        <Metric CodeBag='CyclomaticComplexity' Value='1' />
                        <Metric CodeBag='ClassCoupling' Value='2' />
                        <Metric CodeBag='LinesOfCode' Value='1' />
                      </Metrics>
                    </Member>
                  </Members>
                </Type>               
              </Types>
            </Namespace>
            <Namespace CodeBag='Metropolis.Api.Services'>
              <Metrics>
                <Metric CodeBag='MaintainabilityIndex' Value='89' />
                <Metric CodeBag='CyclomaticComplexity' Value='42' />
                <Metric CodeBag='ClassCoupling' Value='31' />
                <Metric CodeBag='DepthOfInheritance' Value='1' />
                <Metric CodeBag='LinesOfCode' Value='63' />
              </Metrics>
              <Types>
                <Type CodeBag='AnalysisServices'>
                  <Metrics>
                    <Metric CodeBag='MaintainabilityIndex' Value='72' />
                    <Metric CodeBag='CyclomaticComplexity' Value='4' />
                    <Metric CodeBag='ClassCoupling' Value='15' />
                    <Metric CodeBag='DepthOfInheritance' Value='1' />
                    <Metric CodeBag='LinesOfCode' Value='12' />
                  </Metrics>
                  <Members>
                    <Member CodeBag='AnalysisServices()'>
                      <Metrics>
                        <Metric CodeBag='MaintainabilityIndex' Value='92' />
                        <Metric CodeBag='CyclomaticComplexity' Value='1' />
                        <Metric CodeBag='ClassCoupling' Value='2' />
                        <Metric CodeBag='LinesOfCode' Value='1' />
                      </Metrics>
                    </Member>                  
                    <Member CodeBag='Analyze(MetricsCommandArguments) : CodeBase'>
                      <Metrics>
                        <Metric CodeBag='MaintainabilityIndex' Value='62' />
                        <Metric CodeBag='CyclomaticComplexity' Value='2' />
                        <Metric CodeBag='ClassCoupling' Value='12' />
                        <Metric CodeBag='LinesOfCode' Value='9' />
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
