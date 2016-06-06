using System.ComponentModel;
using Metropolis.Common.Extensions;
using Metropolis.TipOfTheDay;

namespace Metropolis.ViewModels
{
    public class TipOfTheDayViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool showTips;
        private ITipOfTheDay tipOfTheDay;

        public bool ShowTips
        {
            get { return showTips; }
            set
            {
                showTips = value;
                PropertyChanged.Notify(this, x => x.ShowTips);
            }
        }

        public ITipOfTheDay TipOfTheDay
        {
            get { return tipOfTheDay; }
            set
            {
                tipOfTheDay = value;
                PropertyChanged.Notify(this, x => x.TipOfTheDay);
            }
        }
    }
}
