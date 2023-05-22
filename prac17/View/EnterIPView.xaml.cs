using prac17.ViewModel;
using System.Windows;

namespace prac17.View
{
    /// <summary>
    /// Логика взаимодействия для EnterIPView.xaml
    /// </summary>
    public partial class EnterIPView : Window
    {
        public EnterIPView(string Login)
        {
            InitializeComponent();
            DataContext = new EnterIPViewModel(this, Login);
        }
    }
}
