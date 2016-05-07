using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Metropolis.Domain.Camera
{
    public class RotationalMovement
    {
        private readonly ISceneProvider provider;
        private Point initialPosition;
        private Matrix3D viewMatrix;

        public RotationalMovement(ISceneProvider provider)
        {
            this.provider = provider;
            provider.Model.Transform = new Transform3DGroup();
            ListenForEvents();
        }

        private bool IsActive { get; set; }

        private void ListenForEvents()
        {
            provider.MouseMove += HandleMouseMove;
            provider.MouseLeftButtonDown += HandleMouseButtonDown;
            provider.MouseLeftButtonUp += InactiveMouseMovements;
        }

        private void InactiveMouseMovements(object sender, MouseEventArgs e)
        {
            IsActive = false;
        }

        private void HandleMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsActive = true;
            initialPosition = e.GetPosition(null);
        }

        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (!IsActive) return;
            
            var currentPosition = e.GetPosition(null);

            var aY = CalculateYPositionChange(currentPosition, provider.ViewPort.ActualWidth);
            var aX = CalculateXPositionChange(currentPosition, provider.ViewPort.ActualHeight);

            viewMatrix.Rotate(new Quaternion(new Vector3D(1, 0, 0), aX));
            viewMatrix.Rotate(new Quaternion(new Vector3D(0, 1, 0), aY));

            Rotate();

            initialPosition = currentPosition;
        }

        private double CalculateXPositionChange(Point currentPosition, double height)
        {
            return Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl) 
                ? 0 : -180*(currentPosition.Y - initialPosition.Y)/height;
        }

        private double CalculateYPositionChange(Point currentPosition, double width)
        {
            return Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift) 
                ? 0 : 180*(currentPosition.X - initialPosition.X)/width;
        }

        private void Rotate()
        {
            var transform = (Transform3DGroup) provider.Model.Transform;
            transform.Children.Clear();
            transform.Children.Add(new MatrixTransform3D(viewMatrix));
        }

        public override string ToString()
        {
            return $"Active {IsActive}, initialPosition: {initialPosition}";
        }

        public void Reset()
        {
            viewMatrix = new Matrix3D();
            Rotate();
        }
    }
}