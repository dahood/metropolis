namespace Metropolis.TipOfTheDay
{
    public class NestedIfThenElseCodeSmellTip : ITipOfTheDay
    {
        public string Tip =>
            @"
    <StackPanel>
        <TextBlock>
            <Span FontSize='14'>Complex if...then...else statements are a CODE SMELL that can greatly increase your toxicity</Span>
        </TextBlock>
        <Image HorizontalAlignment='left' Height='250' Width='400' Source = '..\Images\TipOfTheDay\IfThenElse_CodeSmell.png' />
    </StackPanel>
";        
    }
}