using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Metropolis.Camera
{
    internal class RotationalOperation : TransformOperation, IMouseOperation
    {
        private Matrix3D viewMatrix;

        public RotationalOperation(ISceneProvider provider) : base(provider)
        {
            provider.Model.Transform = new Transform3DGroup();
            viewMatrix = new Matrix3D();
        }

        public void Execute(MouseEventArgs mouseEventArgs)
        {
            var currentPosition = mouseEventArgs.GetPosition(null);
            var pointIn3D = CalculatePositionChange(currentPosition);

            viewMatrix.Rotate(new Quaternion(new Vector3D(-1, 0, 0), pointIn3D.Y));
            viewMatrix.Rotate(new Quaternion(new Vector3D(0, 1, 0), pointIn3D.X));

            Rotate();
            PreviousPoint = currentPosition;
        }

        public void Reset()
        {
            viewMatrix = new Matrix3D();
            Rotate();
        }

        private void Rotate()
        {
            var transform = (Transform3DGroup)Provider.Model.Transform;
            transform.Children.Clear();
            transform.Children.Add(new MatrixTransform3D(viewMatrix));
        }
    }
}