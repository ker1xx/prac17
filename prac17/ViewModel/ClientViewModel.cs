using prac17.Model;
using prac17.View;
using prac17.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Net.Sockets;
using System.Threading;
using pratice_6_messenger;
using System.Windows.Threading;

namespace prac17.ViewModel
{
    internal class ClientViewModel : Binding
    {
        #region Fields
        public static bool Connected = false;
        private Image PicOfGame { get; set; }
        private TCPClient _client = new TCPClient();
        private BitmapImage visImgSource;
        public BitmapImage VisImgSource
        {
            get
            {
                return visImgSource;
            }
            set
            {
                visImgSource = value;
                OnPropertyChanged();
            }
        }
        public static Word PickedWord;
        private List<char> LettersInWord = new List<char>() { };
        #endregion
        #region binding
        public BindableCommand LetterClicked { get; set; }
        #endregion
        public ClientViewModel(Image PicOfGame, string Login, string IP)
        {
            Word thisWord = new Word("", 10, null); // создаем словечко
            PickedWord = thisWord; //делаем его глобальным

            _client.OnNewMessage += (msg) =>
            {
                if (msg.Contains("/conenct"))
                {
                    PickedWord.ThisWord = msg.Substring(0, msg.IndexOf('/'));

                    Dispatcher.CurrentDispatcher.Invoke((Action)(() =>
                    {
                        this.PicOfGame = PicOfGame;
                        PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\Blank.png", UriKind.Relative));
                        for (int i = 0; i < PickedWord.ThisWord.Length; i++) //добавляем буквы в массив букв слова
                            LettersInWord.Add(PickedWord.ThisWord[i]);
                        JoinToServerGameView.IsConnected = true;
                        
                    }));
                }
                else
                {
                    foreach (var b in JoinToServerGameView.lettersbuttons)
                    {
                        if (b.Content == msg)
                        {
                            clickletter(b);
                            foreach (var but in JoinToServerGameView.lettersbuttons)
                                but.IsEnabled = true;
                            break;
                        }
                    }
                }

            };
            _client.Connect("127.0.0.1", 8888);

            /*
            this.PicOfGame = PicOfGame;
            PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\Blank.png", UriKind.Relative));
            for (int i = 0; i < PickedWord.ThisWord.Length; i++) //добавляем буквы в массив букв слова
                LettersInWord.Add(PickedWord.ThisWord[i]);
            */

        }
        public void clickletter(Button sender) // при клике на букву
        {
            if (PickedWord.AmountOfAttempsRemain > 0)
            {
                if (PickedWord.ThisWord.Contains((Convert.ToString(sender.Content).ToLower())))//проверка есть ли она в слове (хранится в переменной ThisWord)
                {
                    for (int i = 0; i < LettersInWord.Count; i++) //Если есть то для всех букв пробегаемся
                    {
                        if (LettersInWord[i].Equals(Convert.ToChar(Convert.ToString(sender.Content).ToLower()))) //если нашли совпадение
                        {
                            showletter(i, Convert.ToString(sender.Content)); //нужно передать в метод индекс (тег) который у кнопки и у текстблока одинаковый и изменить текст текстблока
                        }
                    }
                }
                else //если нету
                {
                    PickedWord.AmountOfAttempsRemain--; //Убираем попытку
                    switch (PickedWord.AmountOfAttempsRemain) //меняем картинку
                    {
                        case 9:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\1var.png", UriKind.Relative));
                                break;
                            }
                        case 8:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\2var.png", UriKind.Relative));
                                break;
                            }
                        case 7:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\3var.png", UriKind.Relative));
                                break;
                            }
                        case 6:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\4var.png", UriKind.Relative));
                                break;
                            }
                        case 5:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\5var.png", UriKind.Relative));
                                break;
                            }
                        case 4:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\6var.png", UriKind.Relative));
                                break;
                            }
                        case 3:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\7var.png", UriKind.Relative));
                                break;
                            }
                        case 2:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\8var.png", UriKind.Relative));
                                break;
                            }
                        case 1:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\9var.png", UriKind.Relative));
                                break;
                            }
                        case 0:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\10var.png", UriKind.Relative));
                                break;
                            }
                    }
                }
                sender.IsEnabled = false; // вырубаем кнопку
                sender.Visibility = Visibility.Hidden;
                if (JoinToServerGameView.spaceforletters.All(x => !string.IsNullOrEmpty(x.Text)))
                    WinGame();
            }
            else
                LoseGame();
        }
        private static void showletter(int indexofbutton, string text) // принимает индекс буквы в слова и текст который будет в текстблок
        {
            JoinToServerGameView.spaceforletters[indexofbutton].Text = text;
        }
        private void WinGame()
        {
            MessageBox.Show("Ты победил!");
        }
        private void LoseGame()
        {
            MessageBox.Show("Ты проиграл(");
        }
    }
}
