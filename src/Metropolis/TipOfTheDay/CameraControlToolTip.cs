namespace Metropolis.TipOfTheDay
{
    public class CameraControlToolTip : ITipOfTheDay
    {
        public string Tip =>
            @"
        <StackPanel>
                <TextBlock>
                    <Span FontSize = '18' FontWeight= 'Bold' >Camera Controls</Span>
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
            </StackPanel>
";
        public string ForMoreInfoUrl => @"https://dahood.io/metropolis-user-guide/";
    }
}