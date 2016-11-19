using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Typing_Speed_Trainer.StatisicsData
{
    public class NotifiableDataBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T storage, T value, [CallerMemberName] string property = null)
        {
            if (Equals(storage, value)) return;

            storage = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}