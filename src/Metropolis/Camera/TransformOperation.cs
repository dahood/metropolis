using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Metropolis.Camera
{
    internal abstract class TransformOperation
    {
        protected readonly ISceneProvider Provider;
        protected Point3D InitialPosition;
        protected Point PreviousPoint;


        protected TransformOperation(ISceneProvider provider)
        {
            Provider = provider;
            InitialPosition = provider.GetCamera().Position;
        }

        protected Point CalculatePositionChange(Point currentPoint)
        {
            // map from 2D space to 3D space
            return new Point(CalculateXPositionChange(currentPoint), CalculateYPositionChange(currentPoint));
        }

        private double CalculateYPositionChange(Point currentPosition)
        {
            return Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)
                ? 0
                : 120*(currentPosition.Y - PreviousPoint.Y) / Provider.ViewPort.ActualHeight;
        }

        private double CalculateXPositionChange(Point currentPosition)
        {
            return Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)
                ? 0
                : 120*(currentPosition.X - PreviousPoint.X) / Provider.ViewPort.ActualWidth;
        }


        public void PreExecute(MouseEventArgs mouseEventArgs)
        {
            PreviousPoint = mouseEventArgs.GetPosition(null);
        }

        public void PostExecute(MouseEventArgs mouseEventArgs)
        {
            // nothing to do here
        }
    }
}