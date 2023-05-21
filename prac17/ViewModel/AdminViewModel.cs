using prac17.Model;
using prac17.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using prac17.View;
using System.Linq;

namespace prac17.ViewModel
{
    internal class AdminViewModel : Binding
    {
        #region peremennie
        private ObservableCollection<String> playersDispay;
        public ObservableCollection<String> PlayersDispay
        {
            get
            {
                return playersDispay;
            }
            set
            {
                playersDispay = value;
                OnPropertyChanged();
            }

        }
        private ObservableCollection<Button> allLetters;
        public ObservableCollection<Button> AllLetters
        {
            get
            {
                return allLetters;
            }
            set
            {
                allLetters = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<TextBlock> spaceforletters;
        private ObservableCollection<TextBlock> Spaceforletters
        {
            get
            {
                return spaceforletters;
            }
            set
            {
                spaceforletters = value;
                OnPropertyChanged();
            }
        }
        public Word PickedWord;
        private List<Char> LettersInWord = new List<char>() { };
        #endregion
        #region binding
        public BindableCommand LetterClicked { get; set; }
        #endregion
        public AdminViewModel()
        {
            Random rnd = new Random(); // делаем рандом
            string newWord = Deserialize.Word[rnd.Next(Deserialize.Word.Count - 1)].ThisWord; //генерим слово из джсона
            Word thisWord = new Word(newWord, 10, null); // создаем словечко
            PickedWord = thisWord; //делаем его глобальным
            for (int i = 0; i < newWord.Length; i++) //добавляем буквы в массив букв слова
                LettersInWord.Add(newWord[i]);

        }
        public void clickletter(Button sender) // при клике на букву
        {
            if (PickedWord.AmountOfAttempsRemain > 0)
            {
                if (PickedWord.ThisWord.Contains((Convert.ToString(sender.Content).ToLower())))//проверка есть ли она в слове (хранится в переменной ThisWord)
                {
                    for (int i = 0; i < LettersInWord.Count - 1; i++) //Если есть то для всех букв пробегаемся
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
                }
                sender.IsEnabled = false; // вырубаем кнопку
                sender.Visibility = Visibility.Hidden;
                if (CreaterServerGameView.spaceforletters.All(x => string.IsNullOrEmpty(x.Text)))
                    WinGame();

            }
            else
                LoseGame();
        }
        private static void showletter(int indexofbutton, string text) // принимает индекс буквы в слова и текст который будет в текстблок
        {
            CreaterServerGameView.spaceforletters[indexofbutton].Text = text;
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
