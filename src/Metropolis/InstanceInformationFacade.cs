using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Metropolis.Api.Domain;
using Metropolis.Common.Models;

namespace Metropolis
{
    public class InstanceInformationFacade
    {
        private readonly IDisplayInstanceInformation provider;
        private IHighlightModel highlight = new EmptyHighlight();
        private Instance highlightedInstance = null;

        public InstanceInformationFacade(IDisplayInstanceInformation provider)
        {
            this.provider = provider;
            Initialize();
        }

        public void ClearDisplay()
        {
            provider.SetClassInformation(string.Empty);
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
            if (highlightedInstance != null)
                return highlightedInstance.PhysicalPath;
            return string.Empty;
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
            highlightedInstance = type;
            var txt = DisplayTextBasedOnRepositoryType(type);

            provider.SetClassInformation(txt);
        }

        private string DisplayTextBasedOnRepositoryType(Instance type)
        {
            if (provider.SourceType == RepositorySourceType.CSharp)
            {
                return string.Format(
                    "Class: {1}{0}Lines Of Code: {2}{0}Number Of Methods: {3}{0}Cyclomatic Complexity: {4}{0}Class Coupling: {5}{0}Depth of Inheritance: {6}{0}Toxicity: {7}{0}Namespace: {8}{0}",
                    Environment.NewLine, type.Name, type.LinesOfCode, type.NumberOfMethods, type.CyclomaticComplexity, type.ClassCoupling,
                    type.DepthOfInheritance, type.Toxicity, type.CodeBag.Name);
            }
            if (provider.SourceType == RepositorySourceType.Java)
            {
                return string.Format(
                    "Class: {1}{0}Lines Of Code: {2}{0}Number Of Methods: {3}{0}Cyclomatic Complexity: {4}{0}ClassFanOut: {5}{0}ClassDataAbstractionCoupling: {6}{0}Anon Inner Classes: {7}{0}Toxicity: {8}{0}Package: {9}{0}",
                    Environment.NewLine, type.Name, type.LinesOfCode, type.NumberOfMethods, type.CyclomaticComplexity, type.ClassFanOutComplexity,
                    type.ClassDataAbstractionCoupling,
                    type.AnonymousInnerClassLength, type.Toxicity, type.CodeBag.Name);
            }
            if (provider.SourceType == RepositorySourceType.ECMA)
            {
                return string.Format(
                    "File: {1}{0}Lines Of Code: {2}{0}Number Of Methods: {3}{0}Cyclomatic Complexity: {4}{0}Toxicity: {5}{0}Directory: {6}{0}",
                    Environment.NewLine, type.Name, type.LinesOfCode, type.NumberOfMethods, type.CyclomaticComplexity, type.Toxicity, type.CodeBag.Name);
            }
            return string.Empty;
        }
    }
}