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
    }
}
