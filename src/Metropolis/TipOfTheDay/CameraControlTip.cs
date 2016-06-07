namespace Metropolis.TipOfTheDay
{
    public class CameraControlTip : ITipOfTheDay
    {
        public string Tip =>
@"
        <StackPanel>
            <TextBlock>
            <Span FontSize='14' FontWeight='Bold'>How to Use Metropolis Ribbon</Span>
                </TextBlock>
                <Image Source = '..\Images\TipOfTheDay/main-ribbon.png' />
                <BulletDecorator>
                    <BulletDecorator.Bullet>
                        <Rectangle Width='5' Height='5' Fill='Gray' />
                    </BulletDecorator.Bullet>
                    <TextBlock TextWrapping = 'Wrap' Margin='20,0,0,0'>Click on the 'Import Data and Code Metrics'</TextBlock>
                </BulletDecorator>
                <BulletDecorator>
                    <BulletDecorator.Bullet>
                        <Rectangle Width='5' Height='5' Fill='Gray' />
                    </BulletDecorator.Bullet>
                    <TextBlock TextWrapping = 'Wrap' Margin='20,0,0,0'>Click 'Run Analysis'</TextBlock>
                </BulletDecorator>
                <BulletDecorator>
                    <BulletDecorator.Bullet>
                        <Rectangle Width='5' Height='5' Fill='Gray' />
                    </BulletDecorator.Bullet>
                    <TextBlock TextWrapping = 'Wrap' Margin='20,0,0,0'>Use camera controls to get the perfect screenshot</TextBlock>
                </BulletDecorator>
                <BulletDecorator>
                    <BulletDecorator.Bullet>
                        <Rectangle Width='5' Height='5' Fill='Gray' />
                    </BulletDecorator.Bullet>
                    <TextBlock TextWrapping = 'Wrap' Margin='20,0,0,0'>Use the code inspector to view individual code files</TextBlock>
                </BulletDecorator>
                <TextBlock>
            <Span FontSize = '14' FontWeight= 'Bold' > Camera Controls</Span>
                </TextBlock>
                <BulletDecorator>
                    <BulletDecorator.Bullet>
                        <Rectangle Width='5' Height='5' Fill='Gray' />
                    </BulletDecorator.Bullet>
                    <TextBlock TextWrapping = 'Wrap' Margin='20,0,0,0'>To pan up and down HOLD DOWN - CTRL</TextBlock>
                </BulletDecorator>
                <BulletDecorator>
                    <BulletDecorator.Bullet>
                        <Rectangle Width='5' Height='5' Fill='Gray' />
                    </BulletDecorator.Bullet>
                    <TextBlock TextWrapping = 'Wrap' Margin='20,0,0,0'>To pan side to side HOLD DOWN - SHIFT</TextBlock>
                </BulletDecorator>
                <BulletDecorator>
                    <BulletDecorator.Bullet>
                        <Rectangle Width='5' Height='5' Fill='Gray' />
                    </BulletDecorator.Bullet>
                    <TextBlock TextWrapping = 'Wrap' Margin='20,0,0,0'>To free form rotate the view HOLD DOWN - ALT</TextBlock>
                </BulletDecorator>
                <BulletDecorator>
                    <BulletDecorator.Bullet>
                        <Rectangle Width='5' Height='5' Fill='Gray' />
                    </BulletDecorator.Bullet>
                    <TextBlock TextWrapping = 'Wrap' Margin='20,0,0,0'>To rotate on X-axis HOLD DOWN - CTRT ALT</TextBlock>
                </BulletDecorator>
                <BulletDecorator>
                    <BulletDecorator.Bullet>
                        <Rectangle Width='5' Height='5' Fill='Gray' />
                    </BulletDecorator.Bullet>
                    <TextBlock TextWrapping = 'Wrap' Margin='20,0,0,0'>To rotate on Y-axis HOLD DOWN - SHIFT ALT</TextBlock>
                </BulletDecorator>
                <BulletDecorator>
                    <BulletDecorator.Bullet>
                        <Rectangle Width='5' Height='5' Fill='Gray' />
                    </BulletDecorator.Bullet>
                    <TextBlock TextWrapping = 'Wrap' Margin='20,0,0,0'>To zoom use the mouse wheel or gesture on your touch screen</TextBlock>
                </BulletDecorator>
                <BulletDecorator>
                    <BulletDecorator.Bullet>
                        <Rectangle Width='5' Height='5' Fill='Gray' />
                    </BulletDecorator.Bullet>
                    <TextBlock TextWrapping = 'Wrap' Margin='20,0,0,0'>Right click the building to display the details of the instance (e.g. class or file depending on the language</TextBlock>
                </BulletDecorator>
            </StackPanel>
";

        public string ForMoreInfoUrl => @"https://github.com/dahood/metropolis/wiki/Beginner-Guide";
    }
}
