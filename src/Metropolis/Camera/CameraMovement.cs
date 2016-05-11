using System.Windows.Media.Media3D;

namespace Metropolis.Camera
{
    public class CameraMovement
    {
        private readonly double fieldOfView;
        private readonly Point3D position;
        private readonly ISceneProvider provider;

        public CameraMovement(ISceneProvider provider)
        {
            this.provider = provider;
            var camera = provider.GetCamera();
            position = camera.Position;
            fieldOfView = camera.FieldOfView;
        }

        public Point3D ChangePosition(double x, double y, double z)
        {
            return new Point3D(position.X + x, position.Y + y, position.Z + z);
        }

        public double ChangeFieldOfView(double value)
        {
            return fieldOfView + value;
        }

        public void Update(double x, double y, double z, double fov)
        {
            var camera = provider.GetCamera();
            camera.Position = ChangePosition(x, y, z);
            camera.FieldOfView = ChangeFieldOfView(fov);
        }
    }
}