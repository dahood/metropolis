using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Metropolis.Api.Domain;
using Metropolis.Models;

namespace Metropolis.Layout
{
    public abstract class AbstractLayout
    {
        protected const double Spacer = 2;
        private const double GoldenRatio = 1.618033988;

        private static readonly AmbientLight DefaultAmbientLight = new AmbientLight(Colors.DarkGray);
        private static readonly DirectionalLight DefaultDirectionalLight = new DirectionalLight(Colors.White, new Vector3D(1, 1, -1));
        private readonly Dictionary<Model3D, Instance> modelClassXRef = new Dictionary<Model3D, Instance>();

        public abstract void ModelCity(Model3DGroup model, CodeBase workspaceCodebase);

        protected void Reset(Model3DGroup cityScape)
        {
            modelClassXRef.Clear();
            cityScape.Children.Clear();
        }

        public void SetCityLights(Model3DGroup cityScape)
        {
            cityScape.Children.Add(DefaultAmbientLight);
            cityScape.Children.Add(DefaultDirectionalLight);
        }

        public Instance LookupClass(Model3D modelHit)
        {
            return modelClassXRef[modelHit];
        }

        internal Model3D LookupModel(Instance src)
        {
            foreach (var item in modelClassXRef)
            {
                if (item.Value.Equals(src))
                    return item.Key;
            }
            return null;
        }

        protected Rect3D RenderSquareBlock(Model3DGroup cityScape, IEnumerable<Instance> classes, Point3D center, int minHeight, int maxHeight)
        {
            var list = classes.ToList();
            var blocksFromCenter = Math.Sqrt(list.Count)/2d;
            return RenderRectangle(cityScape, list, center, minHeight, maxHeight, blocksFromCenter, blocksFromCenter);
        }

        protected Rect3D RenderRectangularBlock(Model3DGroup cityScape, IEnumerable<Instance> classes, Point3D center, int minHeight, int maxHeight)
        {
            var list = classes.ToList();
            var rectangleDimensions = CalculateWidthAndLength(list.Count);
            return RenderRectangle(cityScape, list, center, minHeight, maxHeight, rectangleDimensions[0], rectangleDimensions[1]);
        }

        protected static double[] CalculateWidthAndLength(int numberOfElements)
        {
            var width = Math.Sqrt(GoldenRatio*numberOfElements);
            var length = Math.Sqrt(1/GoldenRatio*numberOfElements);

            var rects = new List<double[]>
            {
                new[] {width + 1, length},
                new[] {width, length + 1},
                new[] {width - 1, length + 1},
                new[] {width + 1, length + 1}
            };

            return rects.Where(x => x[0]*x[1] >= numberOfElements)
                .Where(x => x[0]/x[1] >= 1 && x[0]/x[1] < 2)
                .OrderBy(x => x[0]*x[1])
                .First();
        }

        protected Rect3D RenderRectangle(Model3DGroup cityScape, List<Instance> classes, Point3D center, int minHeight, int maxHeight, double width,
            double length)
        {
            var counter = 0;


            var zOffset = center.Z - width;
            for (var column = 0 - width; column < width; column++)
            {
                var currentX = center.X + Spacer*column;
                for (var row = 0 - length; row < length; row++)
                {
                    if (counter >= classes.Count) break;
                    var currentZ = zOffset + Spacer*row;
                    cityScape.Children.Add(CreateCube(currentX, currentZ, minHeight, maxHeight, classes[counter++]));
                }
            }
            return new Rect3D(center, new Size3D(width, 0, length));
        }

        private Model3D CreateCube(double x, double z, int minHeight, int maxHeight, Instance c)
        {
            var color = BrushFactory.GetBrushForToxicity(c.Toxicity);
            var scaledHeight = LinearScale.Apply(c.LinesOfCode, minHeight, maxHeight);

            var result = CreateCube(x, z, scaledHeight, color);
            modelClassXRef.Add(result, c);
            return result;
        }

        private static Model3D CreateCube(double x, double z, int scaleOfheight, Brush brush)
        {
            var perfectCube = BuildingFactory.CreateCube(brush);

            var transformGroup = new Transform3DGroup();
            var verticalTransformOffset = scaleOfheight/2d;
            transformGroup.Children.Add(new ScaleTransform3D(1, scaleOfheight, 1));
            transformGroup.Children.Add(new TranslateTransform3D(x, verticalTransformOffset, z));

            perfectCube.Transform = transformGroup;
            return perfectCube;
        }
    }
}