using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Controls;
using prac17.Model;
using System.Linq;

namespace prac17.ViewModel.Helpers.TCPLogic
{
    internal class ClientLogic
    {
        public CancellationTokenSource isWroking;
        bool isAdminOrClient;
        public string Login;
        public Socket server;
        ClientViewModel cl;
        AdminViewModel ad;
        public ClientLogic(string Login, Socket server, string IP, bool isAdmin, object sender) //конструктор для создания клиента, получает логин, сокет сервера, айпи
        {
            this.isAdminOrClient = isAdminOrClient;
            if (isAdminOrClient == true)
                ad = sender as AdminViewModel;
            else
                cl = sender as ClientViewModel;
            this.Login = Login;
            this.server = server;
            server.Connect(IP, 8888);
            isWroking = new CancellationTokenSource(); //канселейшентокен для отмены всех тасков асинхронных
        }
        public async Task Receive(CancellationToken Token) //получение сообщения
        {
            while (!Token.IsCancellationRequested) //если таски не отменены (клиент все еще подключен к серверу т.е. играет то получаем смски
            {
                byte[] bytes = new byte[1024];
                await server.ReceiveAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
                string message = Encoding.UTF8.GetString(bytes);
                if (ad != null)
                {
                    ReceivingFromServer receivingFromServer = new ReceivingFromServer(ad, true);
                    receivingFromServer.ReciveLetterFromServer(message);
                }
                else
                {
                    if (message.Contains("/connect"))
                    {
                        string word = message.Substring(0, message.IndexOf('/'));
                        ClientViewModel.PickedWord.ThisWord = word;
                    }
                    else
                    {
                        ReceivingFromServer receivingFromServer = new ReceivingFromServer(cl, false);
                        receivingFromServer.ReciveLetterFromServer(message);
                    }
                }
            }
        }
        public async Task Send(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await server.SendAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
        }
    }
}
