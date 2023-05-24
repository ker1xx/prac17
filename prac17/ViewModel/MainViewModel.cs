using prac17.Model;
using prac17.View;
using prac17.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace prac17.ViewModel
{
    internal class MainViewModel : Binding
    {
        MainWindow window;
        #region Properties

        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Commands
        public BindableCommand CreateServerCommand { get; set; }
        public BindableCommand JoinServerCommand { get; set; }
        #endregion
        public MainViewModel(MainWindow window)
        {
            this.window = window;
            CreateServerCommand = new BindableCommand(_ => CreateServer());
            JoinServerCommand = new BindableCommand(_ => JoinServer());
        }
        public void CreateServer()
        {
            window.Visibility = System.Windows.Visibility.Collapsed;
            CreaterServerGameView creater = new CreaterServerGameView(Login);
            creater.Show();
            creater.Closed += new EventHandler(close);
        }
        public void JoinServer()
        {
            window.Visibility = System.Windows.Visibility.Collapsed;
            EnterIPView creater = new EnterIPView(Login);
            creater.Show();
            creater.Closed += new EventHandler( close);
        }
        private void close(object sender, EventArgs args)
        {
            Environment.Exit(0);
        }

    }
}
