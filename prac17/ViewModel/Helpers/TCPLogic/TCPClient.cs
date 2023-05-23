using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace pratice_6_messenger
{
    public class TCPClient
    {
        private Socket _clientSocket;

        private List<string> _messages = new List<string>();
        public List<string> Messages
        {
            get { return _messages; }
        }
        private CancellationTokenSource isWorking;

        #region Params
        public string Name;
        #endregion

        public delegate void NewMessageCallback(string message);

        public event NewMessageCallback OnNewMessage;

        public void Connect(string host, int port)
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.Connect(host, port);
            isWorking = new CancellationTokenSource();
            ReceiveMessages(isWorking.Token);
        }
            
        public void Disconnect() { }

        private async Task ReceiveMessages(CancellationToken Token)
        {
            while(!Token.IsCancellationRequested)
            {
                byte[] buffer = new byte[1024];
                await _clientSocket?.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
                string message = Encoding.UTF8.GetString(buffer);
                OnNewMessage(message);
            }
        }

        public async Task SendMessage(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await _clientSocket?.SendAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
        }
    }
}
