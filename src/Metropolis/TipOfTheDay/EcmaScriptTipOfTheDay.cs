namespace Metropolis.TipOfTheDay
{
    public class EcmaScriptTipOfTheDay : ITipOfTheDay
    {
        public string Tip =>
       @"
        <StackPanel>
                <TextBlock>
                    <Span FontSize = '18' FontWeight= 'Bold' >ECMA Script Eslint Settings</Span>
                </TextBlock>
                <TextBlock>ESLint requires several options to be turned on like if you support ECMA6, browser globals, node globals, etc. 
                    You can do this by filling out the Language Dialect option with one of these settings below:
                </TextBlock>
                <BulletDecorator>
                    <BulletDecorator.Bullet>
                        <Rectangle Width='5' Height='5' Fill='Gray' />
                    </BulletDecorator.Bullet>
                    <TextBlock TextWrapping = 'Wrap' Margin='20,0,0,0'>DEFAULT is browser-only globals and ECMA 5 </TextBlock>
                </BulletDecorator>
                <BulletDecorator>
                    <BulletDecorator.Bullet>
                        <Rectangle Width='5' Height='5' Fill='Gray' />
                    </BulletDecorator.Bullet>
                    <TextBlock TextWrapping = 'Wrap' Margin='20,0,0,0'>ECMA6, uses browser, node, and all ecma6 features including using source type of modules</TextBlock>
                </BulletDecorator>
                <TextBlock> More ESlint settings may be needed and will be added as they need to be programmed</TextBlock>
            </StackPanel>
";
        public string ForMoreInfoUrl => @"http://eslint.org/docs/user-guide/configuring";
    }
}