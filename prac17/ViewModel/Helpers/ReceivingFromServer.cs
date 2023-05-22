using prac17.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace prac17.ViewModel.Helpers
{
    internal class ReceivingFromServer
    {
        object someclass;
        bool IsAdmin;
        public ReceivingFromServer(object someclass, bool IsAdmin)
        {
            this.someclass = someclass;
            this.IsAdmin = IsAdmin;
        }
        public void ReciveLetterFromServer(string message) //получение сообщения от сервера
        {
            if (IsAdmin == true)
            {
                foreach (Button a in CreaterServerGameView.lettersbuttons) /*пробегаемся по листу с кнопками и 
                                                                        * ищем где контект кнопки == смске от сервера и потом делаем как будто на эту
                                                                        * * кнопку кликнули*/
                {
                    if (a.Content == message)
                    {
                        (someclass as AdminViewModel).clickletter(a);
                        return;
                    }
                }
            }
            else
            {
                foreach (Button a in JoinToServerGameView.lettersbuttons) /*пробегаемся по листу с кнопками и 
                                                                        * ищем где контект кнопки == смске от сервера и потом делаем как будто на эту
                                                                        * * кнопку кликнули*/
                {
                    if (a.Content == message)
                    {
                            (someclass as ClientViewModel).clickletter(a);
                        return;
                    }
                }
            }
        }
    }
}
