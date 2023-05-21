using prac17.ViewModel;
using prac17.ViewModel.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace prac17.View
{
    /// <summary>
    /// Логика взаимодействия для CreaterServerGameView.xaml
    /// </summary>
    public partial class CreaterServerGameView : Window
    {
        public static List<TextBlock> spaceforletters = new List<TextBlock>();
        public CreaterServerGameView()
        {
            InitializeComponent();
            DataContext = new AdminViewModel();
            GenerateLettersButtons();
            GenerateSpace();
        }
        private void GenerateLettersButtons()
        {
            for (int i = 1; i < 33; i++)
            {
                Button button = new Button();
                button.Tag = i;
                button.Command = new BindableCommand(_ => (DataContext as AdminViewModel).clickletter(button));
                button.Content = (char)(1039 + i);
                PanelForLetters.Children.Add(button);
            }
        }
        private void GenerateSpace()
        {
            for (int i = 1; i < (DataContext as AdminViewModel).PickedWord.ThisWord.Count()+1; i++)
            {
                Border border = new Border();
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 1, 1, 1);
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