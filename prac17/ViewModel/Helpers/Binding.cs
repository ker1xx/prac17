using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace prac17.ViewModel.Helpers
{
    internal class Binding : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
