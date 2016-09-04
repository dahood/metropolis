namespace Metropolis.TipOfTheDay
{
    public class CSharpTipOfTheDay : ITipOfTheDay
    {
        public string Tip =>
@"
            <StackPanel>
                <StackPanel>
                    <TextBlock TextWrapping='Wrap'> 
                        <Span FontSize='14' FontWeight='Bold'>How Collect .Net Metrics</Span>
                    </TextBlock>
                    <BulletDecorator Margin='20,2,2,2'>
                        <BulletDecorator.Bullet>
                            <Rectangle Width='5' Height='5' Fill='Gray' />
                        </BulletDecorator.Bullet>
                        <TextBlock TextWrapping = 'Wrap' Margin='20,0,0,0'>Choose the solution or project file you want to run metrics against and run the Build.</TextBlock>
                    </BulletDecorator>
                </StackPanel>
                <StackPanel>
                    <BulletDecorator Margin='20,2,2,2'>
                        <BulletDecorator.Bullet>
                            <Rectangle Width='5' Height='5' Fill='Gray' />
                        </BulletDecorator.Bullet>
                        <TextBlock TextWrapping = 'Wrap' Margin='20,0,0,0'>Next choose the binary files you DO NOT want to run metrics on. These files will be added to your .metropolisIgnore file.</TextBlock>
                    </BulletDecorator>
                    <Image HorizontalAlignment='left' Height='190' Width='203' Margin='20,10,2,2' Source = '..\Images\TipOfTheDay/CSharpIgnore.png'/>
                    <BulletDecorator Margin='20,2,2,2'>
                        <BulletDecorator.Bullet>
                            <Rectangle Width = '5' Height='5' Fill='Gray' />
                        </BulletDecorator.Bullet>
                        <TextBlock TextWrapping = 'Wrap' Margin='20,0,0,0'>Click the RunAnalysis Button</TextBlock>
                    </BulletDecorator>
                </StackPanel>
            </StackPanel>
";
        public string ForMoreInfoUrl => @"https://dahood.io/metropolis-user-guide/";
    }
}