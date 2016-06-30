using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Metropolis.Api.Domain;

namespace Metropolis
{
    public class InstanceInformationFacade
    {
        private readonly IDisplayInstanceInformation provider;
        private IHighlightModel highlight = new EmptyHighlight();
        public Instance Instance { get; private set; }

        public InstanceInformationFacade(IDisplayInstanceInformation provider)
        {
            this.provider = provider;
            Initialize();
        }

        public void ClearDisplay()
        {
            highlight.Reset();
        }

        public void DisplayClass(Instance src)
        {
            ClearDisplay();
            var model = provider.Layout.LookupModel(src);
            Display((GeometryModel3D) model);
        }

        public string GetPhysicalFilePath()
        {
            return Instance != null ?  Instance.PhysicalPath.Path : string.Empty;
        }

        private void Initialize()
        {
            provider.MouseRightButtonDown += Display;
        }

        private void Display(object sender, MouseButtonEventArgs e)
        {
            var result = VisualTreeHelper.HitTest(provider.ViewPort, e.GetPosition(provider.ViewPort)) as RayMeshGeometry3DHitTestResult;

            if (result == null)
            {
                ClearDisplay();
            }
            else
            {
                Display((GeometryModel3D) result.ModelHit);
            }
        }

        private void Display(GeometryModel3D model)
        {
            highlight = highlight.Swap(model);
            var type = provider.Layout.LookupClass(model);
            Instance = type;
            provider.ShowCodeInspector();
        }
    }
}