using System.Windows.Media.Media3D;
using Metropolis.Api.Domain;
using Metropolis.Models;

namespace Metropolis.Layout
{
    public class SquaredLayout : AbstractLayout
    {
        public override void ModelCity(Model3DGroup cityScape, CodeBase codeBase, AbstractHeatMap heatMap)
        {
            Reset(cityScape);
            SetCityLights(cityScape);

            if (codeBase.InstanceCount() == 0) return;
            RenderSquareBlock(cityScape, codeBase.AllInstances, new Point3D(0, 0, 0),
                codeBase.Graph.MinLinesOfCode, codeBase.Graph.MaxLinesOfCode, heatMap);
        }
    }
}