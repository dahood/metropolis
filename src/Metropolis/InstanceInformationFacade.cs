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
        private Instance highlightedInstance;

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
                    "Class: {1}{0}Lines Of Code: {2}{0}Duplicate Percentage: {3}%Number Of Methods: {4}{0}Cyclomatic Complexity: {5}{0}Class Coupling: {6}{0}Depth of Inheritance: {7}{0}Toxicity: {8}{0}Namespace: {9}{0}",
                    Environment.NewLine, type.Name, type.LinesOfCode, type.DuplicatePercentage, type.NumberOfMethods, type.CyclomaticComplexity, type.ClassCoupling,
                    type.DepthOfInheritance, type.Toxicity, type.CodeBag.Name);
            }
            if (provider.SourceType == RepositorySourceType.Java)
            {
                return string.Format(
                    "Class: {1}{0}Lines Of Code: {2}{0}Duplicate Percentage: {3}%{0}Number Of Methods: {4}{0}Cyclomatic Complexity: {5}{0}ClassFanOut: {6}{0}ClassDataAbstractionCoupling: {7}{0}Anon Inner Classes: {8}{0}Toxicity: {9}{0}Package: {10}{0}",
                    Environment.NewLine, type.Name, type.LinesOfCode, type.DuplicatePercentage, type.NumberOfMethods, type.CyclomaticComplexity, type.ClassFanOutComplexity,
                    type.ClassDataAbstractionCoupling,
                    type.AnonymousInnerClassLength, type.Toxicity, type.CodeBag.Name);
            }
            if (provider.SourceType == RepositorySourceType.ECMA)
            {
                return string.Format(
                    "File: {1}{0}Lines Of Code: {2}{0}Duplicate Percentage: {3}%{0}Number Of Methods: {4}{0}Cyclomatic Complexity: {5}{0}Toxicity: {6}{0}Directory: {7}{0}",
                    Environment.NewLine, type.Name, type.LinesOfCode, type.DuplicatePercentage, type.NumberOfMethods, type.CyclomaticComplexity, type.Toxicity,
                    type.CodeBag.Name);
            }
            return string.Empty;
        }
    }
}