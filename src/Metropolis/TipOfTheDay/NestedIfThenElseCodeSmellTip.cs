namespace Metropolis.TipOfTheDay
{
    public class NestedIfThenElseCodeSmellTip : ITipOfTheDay
    {
        public string Tip =>
            @"
            <StackPanel>
                <StackPanel>
                    <TextBlock TextWrapping='Wrap'> 
                        <Span FontSize='14'>Complex if...then...else statements are a CODE SMELL that can greatly increase your toxicity</Span>
                    </TextBlock>
                    <Image HorizontalAlignment='left' Height='225' Width='400' Source = '..\Images\TipOfTheDay\IfThenElse_CodeSmell.png' />
                </StackPanel>
                <StackPanel>
                    <TextBlock TextWrapping='Wrap'> 
                        <Span FontSize='11'>Consider using the</Span> <Span FontSize='11' FontWeight='Bold'>Chain-of-responsibility pattern</Span> <Span FontSize='11'>instead</Span>
                    </TextBlock>
                </StackPanel>
            </StackPanel>
";

        public string ForMoreInfoUrl => @"https://en.wikipedia.org/wiki/Chain-of-responsibility_pattern";
    }
}