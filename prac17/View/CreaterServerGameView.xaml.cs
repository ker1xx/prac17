using prac17.ViewModel;
using prac17.ViewModel.Helpers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace prac17.View
{
    /// <summary>
    /// Логика взаимодействия для CreaterServerGameView.xaml
    /// </summary>
    /// 

    public partial class CreaterServerGameView : Window
    {
        public static List<TextBlock> spaceforletters = new List<TextBlock>();
        public static List<Button> lettersbuttons = new List<Button>();

        public CreaterServerGameView(string Login)
        {
            InitializeComponent();
            DataContext = new AdminViewModel(PicOfGame, Login);
            GenerateLettersButtons();
            GenerateSpace();
            if (Process.GetProcessesByName("RvRvpnGui").Any())
                setIP.Text += System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0];
            else
                setIP.Text += "127.0.0.1";
        }
        private void GenerateLettersButtons()
        {
            for (int i = 1; i < 33; i++)
            {
                Button button = new Button();
                button.Tag = i;
                button.Command = new BindableCommand(_ => (DataContext as AdminViewModel).clickletter(button));
                button.Content = (char)(1039 + i);
                lettersbuttons.Add(button);
                PanelForLetters.Children.Add(button);
            }
        }
        private void GenerateSpace()
        {
            for (int i = 1; i < (DataContext as AdminViewModel)._pickedWord.ThisWord.Count() + 1; i++)
            {
                Border border = new Border();
                border.BorderBrush = System.Windows.Media.Brushes.Black;
                border.BorderThickness = new Thickness(0, 0, 0, 1);
                TextBlock text = new TextBlock();
                text.Text = "";
                text.Tag = i;
                border.Child = text;
                PanelForSpaces.Children.Add(border);
                spaceforletters.Add(text);
            }
        }

    }
}