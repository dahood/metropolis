using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels.CodeInspector
{
    public class SummaryViewModel : MvxViewModel
    {
        private string name;
        private string codeBag;

        private int linesOfCode;
        private int numberOfMethods;
        private int cyclomaticComplexity;
        private int classCoupling;
        private int depthOfInheritance;

        private double toxicity;
        private double percentDuplicate;
        
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public string CodeBag
        {
            get { return codeBag; }
            set { SetProperty(ref codeBag, value); }
        }

        public int LinesOfCode
        {
            get { return linesOfCode; }
            set { SetProperty(ref linesOfCode, value); }
        }

        public int NumberOfMethods
        {
            get { return numberOfMethods; }
            set { SetProperty(ref numberOfMethods, value); }
        }

        public int ClassCoupling
        {
            get { return classCoupling; }
            set { SetProperty(ref classCoupling, value); }
        }

        public int CyclomaticComplexity
        {
            get { return cyclomaticComplexity; }
            set { SetProperty(ref cyclomaticComplexity, value); }
        }

        public int DepthOfInheritance
        {
            get { return depthOfInheritance; }
            set { SetProperty(ref depthOfInheritance, value); }
        }

        public double PercentDuplicate
        {
            get { return percentDuplicate; }
            set { SetProperty(ref percentDuplicate, value); }
        }

        public double Toxicity
        {
            get { return toxicity; }
            set { SetProperty(ref toxicity, value); }
        }

    }
}