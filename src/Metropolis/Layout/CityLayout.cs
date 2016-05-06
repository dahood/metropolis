using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;
using Metropolis.Api.Core.Domain;
using Metropolis.Api.Extensions;
using Metropolis.Domain;

namespace Metropolis.Layout
{
    public class CityLayout : AbstractLayout
    {
        public override void ModelCity(Model3DGroup cityScape, CodeBase codeBase)
        {
            Reset(cityScape);
            SetCityLights(cityScape);

            var maxHeight = codeBase.Graph.MaxLinesOfCode;
            var minHeight = codeBase.Graph.MinLinesOfCode;

            var namespaces = codeBase.ByNamespace();
            var plots = GeneratePlots(namespaces);
            var rows = plots.Batch((int)Math.Ceiling(Math.Sqrt(plots.Count())));
            if (rows == null) return;

            var previous = Plot.Empty;
            var maxZ = 0d;
            var totalZ = 0d;

            foreach (var row in rows)
            {
                foreach (var plot in row)
                {
                    plot.Adjust(previous, totalZ);
                    RenderSquareBlock(cityScape, plot.Classes, plot.Bounds.Location, minHeight, maxHeight);
                    previous = plot;
                    maxZ = Math.Max(maxZ, previous.SizeZ);
                }
                previous = Plot.Empty;
                totalZ -= (maxZ + Spacer);
                maxZ = 0;
            }
        }

        private static IEnumerable<Plot> GeneratePlots(Dictionary<string, IEnumerable<Class>> namespaces)
        {
            var result = new List<Plot>(namespaces.Count);

            result.AddRange(
                from pair in namespaces
                let d = Math.Ceiling(Math.Sqrt(pair.Value.Count()))
                let c = d/2 
                select new Plot(pair.Key, pair.Value, new Rect3D(new Point3D(c,0,c), new Size3D(d, 0, d))));

            result.Sort((left, right) =>
            {
                var x = left.Classes.Max(c => c.LinesOfCode);
                var y = right.Classes.Max(c => c.LinesOfCode);
                if (x == y) return left.Classes.Count() - right.Classes.Count();
                return x - y;
            });    

            return result;
        }
    }

    public class Plot
    {
        public static readonly Plot Empty = new Plot(string.Empty, null, Rect3D.Empty);

        public Plot(string name, IEnumerable<Class> classes, Rect3D bounds)
        {
            Name = name;
            Classes = classes;
            Bounds = bounds;
        }

        public string Name { get; }

        public IEnumerable<Class> Classes { get; }

        public Rect3D Bounds { get; private set; }
        public double SizeZ => Bounds.SizeZ * 2;

        public void Adjust(Plot previous, double zOffset)
        {
            var l = this.Bounds.Location;
            var newX = previous.Bounds.IsEmpty ? l.X : previous.Bounds.X + previous.Bounds.SizeX + Bounds.SizeX + 2;
            Bounds = new Rect3D(new Point3D(newX, 0, zOffset), Bounds.Size);
        }

        public override string ToString()
        {
            return $"Namespace: {Name}: Bounds:{Bounds}";
        }
    }
}
