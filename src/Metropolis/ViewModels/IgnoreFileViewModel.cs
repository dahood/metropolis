using System.ComponentModel;
using Metropolis.Common.Extensions;

namespace Metropolis.ViewModels
{
    public class IgnoreFileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isIgnored;
        
        public bool IsIgnored
        {
            get { return isIgnored; }
            set
            {
                isIgnored = value;
                PropertyChanged.Notify(this, x => x.IsIgnored);
            }
        }

        public string FileName { get; set; }
    }
}
