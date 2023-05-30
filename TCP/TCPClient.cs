using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace prac17
{
    public class TCPClient
    {

        #region Params
        private Socket _clientSocket; //сокет клиента

        public CancellationTokenSource isWorking; //для отмены тасков
        #endregion

        public delegate void NewMessageCallback(string message); //делегат позволяющий сделать подписку на новые сообщения
        public delegate void ConnectedCallback(); //делегат позволяющий сделать подписку на подключение

        public event NewMessageCallback OnNewMessage; //событие нового сообщения
        public event ConnectedCallback OnConnected; //событие подключения
        public void Connect(string host, int port)
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.Connect(host, port); //подключение к серверу
            isWorking = new CancellationTokenSource();
            ReceiveMessages(isWorking.Token); //включение асинхронного таска получения сообщений
        }

        private async Task ReceiveMessages(CancellationToken Token)
        {
            while (!Token.IsCancellationRequested)
            {
                byte[] buffer = new byte[1024];
                await _clientSocket?.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None); //асинхронно ждет смску
                string message = Encoding.UTF8.GetString(buffer);
                OnNewMessage(message);
            }
        }

        public async Task SendMessage(string message) //асинхронно отправляет смску
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await _clientSocket?.SendAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
        }
    }
}
