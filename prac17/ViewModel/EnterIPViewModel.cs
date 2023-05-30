using prac17.View;
using prac17.ViewModel.Helpers;
using System;

namespace prac17.ViewModel
{
    internal class EnterIPViewModel : Binding
    {
        #region peremenie
        EnterIPView window;
        private string iptext;
        private string Login;
        public string IPText
        {
            get
            {
                return iptext;
            }
            set
            {
                iptext = value;
                OnPropertyChanged();
            }
        }
        public BindableCommand Connect { get; set; }
        #endregion
        public EnterIPViewModel(EnterIPView a, string Login)
        {
            this.window = a;
            this.Login = Login;
            Connect = new BindableCommand(_ => showgameview());
        }
        private void showgameview()
        {
            window.Visibility = System.Windows.Visibility.Collapsed;
            JoinToServerGameView join = new JoinToServerGameView(IPText, Login);
            join.Show();
            join.Closed += new EventHandler(close);
        }
        private void close(object sender, EventArgs args)
        {
            Environment.Exit(0);
        }
    }
}
