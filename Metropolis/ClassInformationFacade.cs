using Metropolis.Domain;
using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Metropolis
{
    public class ClassInformationFacade
    {
        private readonly IDisplayClassInformation provider;
        private IHighlightModel highlight = new EmptyHighlight();

        public ClassInformationFacade(IDisplayClassInformation provider)
        {
            this.provider = provider;
            Initialize();
        }

        public void ClearDisplay()
        {
            provider.SetClassInformation(string.Empty);
            highlight.Reset();
        }

        public void DisplayClass(Class src)
        {
            ClearDisplay();
            var model = provider.Layout.LookupModel(src);
            Display((GeometryModel3D)model);
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
            var txt =
                string.Format(
                    "Name: {1}{0}Lines Of Code {2}{0}Number Of Methods: {3}{0}Cyclomatic Complexity: {4}{0}Class Coupling: {5}{0}Depth of Inheritance: {6}{0}Toxicity: {7}{0}Namespace: {8}{0}",
                    Environment.NewLine, type.Name, type.LinesOfCode, type.NumberOfMethods, type.CyclomaticComplexity, type.ClassCoupling,
                    type.DepthOfInheritance, type.Toxicity, type.NameSpace);
            provider.SetClassInformation(txt);
        }
    }
}