using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Metropolis.Camera
{
    internal class TranslateOperation : TransformOperation, IMouseOperation
    {
        private double z;
        public TranslateOperation(ISceneProvider provider) : base(provider)
        {
            z = provider.GetCamera().Position.Z;
            provider.MouseWheel += HandleMouseWheel;
        }

        public void Execute(MouseEventArgs mouseEventArgs)
        {
            var pointIn3D = CalculatePositionChange(mouseEventArgs.GetPosition(null));
            Provider.GetCamera().Position = new Point3D(InitialPosition.X + pointIn3D.X,
                InitialPosition.Y + pointIn3D.Y, z);
        }

        public void Reset()
        {
            Provider.GetCamera().Position = InitialPosition;
        }

        private void HandleMouseWheel(object sender, MouseWheelEventArgs e)
        {
            z += e.Delta/4d;
            Provider.GetCamera().Position = new Point3D(InitialPosition.X,InitialPosition.Y, z);
        }
    }
}