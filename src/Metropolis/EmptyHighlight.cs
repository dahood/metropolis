using System.Windows.Media.Media3D;

namespace Metropolis
{
    public class EmptyHighlight : IHighlightModel
    {
        public void Reset()
        {

        }

        public IHighlightModel Swap(GeometryModel3D model)
        {
            return model == null ? (IHighlightModel)this :  new HighlightModel(model);
        }
    }
}