using prac17.Model;
using prac17.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using prac17.View;
using System.Linq;
using System.Security.RightsManagement;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Net.Sockets;
using System.Text;
using pratice_6_messenger;

namespace prac17.ViewModel
{
    internal class AdminViewModel : Binding
    {
        #region peremennie
        Image PicOfGame { get; set; }
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
        public Word PickedWord;
        private List<char> LettersInWord = new List<char>() { };
        #endregion
        #region binding
        #endregion
        public AdminViewModel(Image PicOfGame, string Login)
        {   //генерим сокет для того чтобы сервер держать

            this.PicOfGame = PicOfGame; //картинку делаем глобальной
            PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\Blank.png", UriKind.Relative)); //делаем картинке пустой соурс

            Random rnd = new Random(); // делаем рандом
            string newWord = Deserialize.Word[rnd.Next(Deserialize.Word.Count - 1)].ThisWord; //генерим слово из джсона
            Word thisWord = new Word(newWord, 10, null); // создаем словечко
            PickedWord = thisWord; //делаем его глобальным

            TCPServer.OnNewClient += (clientSocket) =>
            {
                TCPServer.SendMessageToAll(PickedWord.ThisWord+"/connect");
            };
            TCPServer.Start(8888);

            for (int i = 0; i < newWord.Length; i++) //добавляем буквы в массив букв слова
                LettersInWord.Add(newWord[i]);
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
                TCPServer.SendMessageToAll((string)sender.Content);
                foreach (var b in CreaterServerGameView.lettersbuttons)
                    b.IsEnabled = false;
                if (JoinToServerGameView.spaceforletters.All(x => !string.IsNullOrEmpty(x.Text))) //проверяем если нет пустых текстблоков то победа
                    WinGame();

            }
            else
                LoseGame();
        }
        private static void showletter(int indexofbutton, string text) // принимает индекс буквы в слова и текст который будет в текстблок
        {
            JoinToServerGameView.spaceforletters[indexofbutton].Text = text;
        }
        private void WinGame() //победа
        {
            MessageBox.Show("Ты победил!");
        }
        private void LoseGame() //поражение
        {
            MessageBox.Show("Ты проиграл(");
        }
    }
}
