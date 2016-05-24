using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Metropolis.Models
{
    /// <summary>
    ///     z = LOC, y/x = Toxicity , Color = NOM
    /// </summary>
    public class BuildingFactory
    {
        public static GeometryModel3D CreateCube(Brush brush)
        {
            var cube = new MeshGeometry3D
            {
                Positions = AssemblePoints(),
                TriangleIndices = CreateTrianglesForACube()
            };

            //TODO: Normals

            return new GeometryModel3D
            {
                Geometry = cube,
                Material = new DiffuseMaterial(brush)
            };
        }

        private static Point3DCollection AssemblePoints()
        {
            const double size = 0.5;

            return new Point3DCollection
            {
                new Point3D(size, size, size),
                new Point3D(-size, size, size),
                new Point3D(-size, -size, size),
                new Point3D(size, -size, size),
                new Point3D(size, size, -size),
                new Point3D(-size, size, -size),
                new Point3D(-size, -size, -size),
                new Point3D(size, -size, -size)
            };
        }


        private static Int32Collection CreateTrianglesForACube()
        {
            return new Int32Collection(new[]
            {
                //front
                0, 1, 2,
                0, 2, 3,
                //back
                4, 7, 6,
                4, 6, 5,
                //Right
                4, 0, 3,
                4, 3, 7,
                //Left
                1, 5, 6,
                1, 6, 2,
                //Top
                1, 0, 4,
                1, 4, 5,
                //Bottom
                2, 6, 7,
                2, 7, 3
            });
        }
    }
}