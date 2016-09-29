using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels
{
    public class CodeSummaryViewModel : MvxViewModel
    {
        private string absoluteToxicity;
        private string averageToxicity;
        private string codeDensity;
        private string codeDuplicatePercentage;
        private int linesOfCode;
        private string numberOfTypes;

        private string projectName;
        /*
         * RunDateTextBox.Text = CodeBase.RunDate.ToString("yyyy-MM-dd HH:mm");
            LocTextBlock.Text = CodeBase.LinesOfCode().ToString("N0", CultureInfo.InvariantCulture);
            TypesTextBlock.Text = CodeBase.InstanceCount().ToString("N0", CultureInfo.InvariantCulture);
            ToxicityTextBlock.Text = CodeBase.AverageToxicity().ToString("N2", CultureInfo.InvariantCulture);
            AbsoluteToxicityTextBlock.Text = CodeBase.AbsoluteToxicity().ToString("N0", CultureInfo.InvariantCulture);
            CodeDensityTextBlock.Text = CodeBase.Density().ToString("N2", CultureInfo.InvariantCulture);
            DuplicateTextBlock.Text = CodeBase.Duplicates().ToString("P", CultureInfo.InvariantCulture);
        */


    }
}