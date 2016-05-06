using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Metropolis
{
    public class HighlightModel : IHighlightModel
    {
        private static readonly DiffuseMaterial HighlightMaterial = new DiffuseMaterial(Brushes.MidnightBlue);
        private readonly GeometryModel3D model;
        private Material material;

        public HighlightModel(GeometryModel3D model)
        {
            this.model = model;
            Initialize();
        }

        private void Initialize()
        {
            material = model.Material;
            model.Material = HighlightMaterial;
        }

        public void Reset()
        {
            model.Material = material;
        }

        public IHighlightModel Swap(GeometryModel3D newModel)
        {
            Reset();
            return new HighlightModel(newModel);
        }
    }
}