namespace Metropolis.TipOfTheDay
{
    public class InversionOfControlTip : ITipOfTheDay
    {
        //http://martinfowler.com/articles/injection.html

        public string Tip =>
@"
           <StackPanel>
                <TextBlock TextWrapping='Wrap'>  
                    <Span FontSize='14' FontWeight='Bold'> Reducing Code Coupling with Inversion of Control</Span>
                </TextBlock>
            <TextBlock TextWrapping='Wrap'>   
               <Span FontSize='12'> In software engineering, inversion of control (IoC) is a design principle in which custom-written portions of a computer program receive the flow of control from a generic framework.A software architecture with this design inverts control as compared to traditional procedural programming: in traditional programming, the custom code that expresses the purpose of the program calls into reusable libraries to take care of generic tasks, but with inversion of control, it is the framework that calls into the custom, or task-specific, code.</Span>
            </TextBlock>
            <TextBlock TextWrapping='Wrap'>
                <Span FontSize='12'>Inversion of control is used to increase modularity of the program and make it extensible, and has applications in object-oriented programming and other programming paradigms.The term was popularized by Robert C. Martin and Martin Fowler.</Span>                                    
            </TextBlock>
        </StackPanel>
";

        public string ForMoreInfoUrl => @"https://en.wikipedia.org/wiki/Inversion_of_control";
    }
}