using prac17.ViewModel.Helpers;
using prac17.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace prac17.View
{
    /// <summary>
    /// Логика взаимодействия для JoinToServerGameView.xaml
    /// </summary>
    public partial class JoinToServerGameView : Window
    {
        public static List<TextBlock> spaceforletters = new List<TextBlock>();
        public static List<Button> lettersbuttons = new List<Button>();
        public JoinToServerGameView(string IP, string Login)
        {
            InitializeComponent();
            DataContext = new ClientViewModel(PicOfGame, Login,IP);
            
            GenerateLettersButtons();
            GenerateSpace();
        }
        private void GenerateLettersButtons()
        {
            for (int i = 1; i < 33; i++)
            {
                Button button = new Button();
                button.Tag = i;
                button.Command = new BindableCommand(_ => (DataContext as ClientViewModel).clickletter(button));
                button.Content = (char)(1039 + i);
                lettersbuttons.Add(button);
                PanelForLetters.Children.Add(button);
            }
        }
        private void GenerateSpace()
        {
            for (int i = 1; i < ClientViewModel.PickedWord.ThisWord.Count() + 1; i++)
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
