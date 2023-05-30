using prac17.Model;
using prac17.View;
using prac17.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace prac17.ViewModel
{
    internal class AdminViewModel : Binding
    {
        #region Fields
        Image PicOfGame { get; set; }
        private BitmapImage _visImgSource;
        public BitmapImage VisImgSource
        {
            get
            {
                return _visImgSource;
            }
            set
            {
                _visImgSource = value;
                OnPropertyChanged();
            }
        }
        public Word _pickedWord;
        private List<char> L_lettersInWord = new List<char>() { };
        private List<Button> _disabledButtons = new List<Button>() { };
        private bool _isMsgRecived = false;
        private ObservableCollection<string> _usernames = new ObservableCollection<string>() { };
        public ObservableCollection<string> Usernames
        {
            get
            {
                return _usernames;
            }
            set
            {
                _usernames = value;
                OnPropertyChanged();
            }
        }
        #endregion
        public AdminViewModel(Image PicOfGame, string Login)
        {
            Usernames.Add(Login);
            this.PicOfGame = PicOfGame; //картинку делаем глобальной
            PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\LightTheme\\Blank.png", UriKind.Relative)); //делаем картинке пустой соурс

            Random rnd = new Random(); // делаем рандом
            string newWord = Deserialize.Word[rnd.Next(Deserialize.Word.Count - 1)].ThisWord; //генерим слово из джсона
            Word thisWord = new Word(newWord, 10, null); // создаем словечко
            _pickedWord = thisWord; //делаем его глобальным


            TCPServer.OnNewClient += (clientSocket) =>
            {
                TCPServer.SendMessage(clientSocket, _pickedWord.ThisWord + "/connect");
            };
            TCPServer.OnNewMessage += (msg) =>
            {
                if (msg.Contains("/username")) //если есть /username то добавляется в коллекцию боковой панели юзеров
                    _usernames.Add(msg.Substring(0, msg.IndexOf('/')));
                else
                {
                    foreach (var b in CreaterServerGameView.lettersbuttons) //в листе кнопок с буквами ищем ту, чей контент равен
                    //тексту сообщения (нажатой букве другого юзера), после чего имитируем нажатие этой кнопки и включаем все кнопки
                    {
                        if (Convert.ToChar(b.Content) == msg[0])
                        {
                            _isMsgRecived = true; //надо для того чтобы не циклилась отправка сообщений
                            clickletter(b);
                            foreach (var but in CreaterServerGameView.lettersbuttons)
                            {
                                if (!_disabledButtons.Contains(but)) //включаем кнопки если они не в листе нажатых
                                    but.IsEnabled = true;
                            }
                            _isMsgRecived = false;
                            break;
                        }
                    }
                }
            };
            TCPServer.Start(8888);

            for (int i = 0; i < newWord.Length; i++) //добавляем буквы в массив букв слова
                L_lettersInWord.Add(newWord[i]);
        }

        public void clickletter(Button sender) // при клике на букву
        {
            if (_pickedWord.AmountOfAttempsRemain > 0) //если остались попытки
            {
                if (_pickedWord.ThisWord.Contains((Convert.ToString(sender.Content).ToLower())))//проверка есть ли она в слове (хранится в переменной ThisWord)
                {
                    for (int i = 0; i < L_lettersInWord.Count; i++) //Если есть то для всех букв пробегаемся
                    {
                        if (L_lettersInWord[i].Equals(Convert.ToChar(Convert.ToString(sender.Content).ToLower()))) //если нашли совпадение
                        {
                            showletter(i, Convert.ToString(sender.Content)); //нужно передать в метод индекс (тег) который у кнопки и у текстблока одинаковый и изменить текст текстблока
                            _disabledButtons.Add(sender);
                        }
                    }
                }
                else //если нету
                {
                    _pickedWord.AmountOfAttempsRemain--; //Убираем попытку
                    switch (_pickedWord.AmountOfAttempsRemain) //меняем картинку
                    {
                        case 9:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\LightTheme\\1var.png", UriKind.Relative));
                                break;
                            }
                        case 8:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\LightTheme\\2var.png", UriKind.Relative));
                                break;
                            }
                        case 7:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\LightTheme\\3var.png", UriKind.Relative));
                                break;
                            }
                        case 6:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\LightTheme\\4var.png", UriKind.Relative));
                                break;
                            }
                        case 5:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\LightTheme\\5var.png", UriKind.Relative));
                                break;
                            }
                        case 4:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\LightTheme\\6var.png", UriKind.Relative));
                                break;
                            }
                        case 3:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\LightTheme\\7var.png", UriKind.Relative));
                                break;
                            }
                        case 2:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\LightTheme\\8var.png", UriKind.Relative));
                                break;
                            }
                        case 1:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\LightTheme\\9var.png", UriKind.Relative));
                                break;
                            }
                        case 0:
                            {
                                PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\LightTheme\\10var.png", UriKind.Relative));
                                break;
                            }
                    }
                }
                sender.IsEnabled = false; // вырубаем кнопку
                sender.Visibility = Visibility.Hidden;
                if (!_isMsgRecived) //если пользователь сам нажал, а не нажатие было сымитировано
                {
                    TCPServer.SendMessageToAll(Convert.ToString(sender.Content));

                    foreach (var b in CreaterServerGameView.lettersbuttons) //вырубаем все кнопки
                        b.IsEnabled = false;
                }
                if (CreaterServerGameView.spaceforletters.All(x => !string.IsNullOrEmpty(x.Text))) //проверяем если нет пустых текстблоков то победа
                    WinGame();

            }
            else
                LoseGame();
        }
        private static void showletter(int indexofbutton, string text) // принимает индекс буквы в слова и текст который будет в текстблок
        {
            CreaterServerGameView.spaceforletters[indexofbutton].Text = text;
        }
        private void WinGame() //победа
        {
            MessageBox.Show("Ты победил!");
            TCPServer.SendMessageToAll("/end");
        }
        private void LoseGame() //поражение
        {
            MessageBox.Show("Ты проиграл(");
            TCPServer.SendMessageToAll("/end");
        }
    }
}
