using prac17.Model;
using prac17.View;
using prac17.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace prac17.ViewModel
{
    internal class ClientViewModel : Binding
    {
        #region Fields
        public static bool Connected = false;
        private Image PicOfGame { get; set; }
        private bool _isMsgRecived = false;
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
        private List<Button> DisabledButtons = new List<Button>() { };
        #endregion
        #region binding
        public BindableCommand LetterClicked { get; set; }
        #endregion
        public ClientViewModel(Image PicOfGame, string Login, string IP)
        {
            Word thisWord = new Word("", 10, null); // создаем словечко
            PickedWord = thisWord; //делаем его глобальным

            _client.OnConnected += () =>
            {
                _client.SendMessage(Login + "/username"); //при подключении срабатывает ивент, отправляющий имя пользователя
            };

            _client.OnNewMessage += (msg) => //при получении нового смс срабатывает ивент
            {
                if (msg.Contains("/connect")) //если есть коннект (сервер отправил сообщение при подключении к нему пользователя,
                //передающее в себе слово для игры
                {
                    PickedWord.ThisWord = msg.Substring(0, msg.IndexOf('/')); //пользователь берет это слово

                    Dispatcher.CurrentDispatcher.Invoke((Action)(() => //ставит в очередь основого потока событие для того, чтобы вьюшка 
                    //не рисовалась быстрее кода и появились кнопки
                    {
                        this.PicOfGame = PicOfGame;
                        PicOfGame.Source = new BitmapImage(new Uri("..\\ViewModel\\Helpers\\Additional\\gamepics\\LightTheme\\Blank.png", UriKind.Relative));
                        for (int i = 0; i < PickedWord.ThisWord.Length; i++) //добавляем буквы в массив букв слова
                            LettersInWord.Add(PickedWord.ThisWord[i]);
                        JoinToServerGameView.IsConnected = true;
                    }));
                }
                else if (msg.Contains("/end")) //если есть /end то вырубает таски
                {
                    _client.isWorking.Cancel();
                }
                else //если просто пришла буква то она ищется в листе с кнопками и имитируется нажатие
                {
                    foreach (var b in JoinToServerGameView.lettersbuttons)
                    {
                        if (Convert.ToChar(b.Content) == msg[0])
                        {
                            _isMsgRecived = true; //для имитации нажатия (чтобы еще раз не отправлялось сообщение и оно не циклилось)
                            clickletter(b);
                            foreach (var but in JoinToServerGameView.lettersbuttons)
                            {
                                if (!DisabledButtons.Contains(but))
                                    but.IsEnabled = true;
                            }
                            _isMsgRecived = false;
                            break;
                        }
                    }
                }

            };
            _client.Connect(IP, 8888);

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
                            DisabledButtons.Add(sender);
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
                if (!_isMsgRecived) //если не имитация то шлет сообщение и вырубает кнопки
                {
                    _client.SendMessage(Convert.ToString(sender.Content));
                    foreach (var b in JoinToServerGameView.lettersbuttons)
                        b.IsEnabled = false;
                }
                if (JoinToServerGameView.spaceforletters.All(x => !string.IsNullOrEmpty(x.Text))) //если не осталось пустых текстблоков то победа
                    WinGame();
            }
            else
                LoseGame();
        }
        private static void showletter(int indexofbutton, string text) // принимает индекс буквы в слова и текст который будет в текстблок
        {
            JoinToServerGameView.spaceforletters[indexofbutton].Text = text; //ставит в текстблок букву
        }
        private void WinGame()
        {
            MessageBox.Show("Ты победил!");
            _client.SendMessage("/end");
        }
        private void LoseGame()
        {
            MessageBox.Show("Ты проиграл(");
            _client.SendMessage("/end");
        }
    }
}
