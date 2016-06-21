using System;
using System.Linq;
using System.Windows.Media.Media3D;
using Metropolis.Api.Domain;
using Metropolis.Models;

namespace Metropolis.Layout
{
    public class GoldenRatioLayout : AbstractLayout
    {
        public override void ModelCity(Model3DGroup cityScape, CodeBase codeBase, AbstractHeatMap heatMap)
        {
            Reset(cityScape);
            SetCityLights(cityScape);

            var maxHeight = codeBase.Graph.MaxLinesOfCode;
            var minHeight = codeBase.Graph.MinLinesOfCode;

            var namespaces = codeBase.ByNamespace();
            var squaredNamespaces = (int) Math.Sqrt(namespaces.Keys.Count);

            var offset = namespaces.Keys.Count*-Spacer/2;
            var namespaceCounter = 0;
            var arrayofClasses = namespaces.Values.ToArray();
            var origin = new Point3D(offset, 0, offset);
            for (var columnCounter = 0; columnCounter < squaredNamespaces; columnCounter++)
            {
                double tallestX = 0;
                for (var rowCounter = 0; rowCounter < squaredNamespaces; rowCounter++)
                {
                    var listToRender = arrayofClasses[namespaceCounter];
                    RenderRectangularBlock(cityScape, listToRender, origin, minHeight, maxHeight, heatMap);
                    namespaceCounter++;
                    var zOffeset = Math.Sqrt(listToRender.Count());
                    if (zOffeset > tallestX) tallestX = zOffeset;
                    origin.Z += Math.Sqrt(listToRender.Count())*Spacer;
                }
                origin.X += tallestX*Spacer;
                origin.Z = offset;
            }
        }
    }
}