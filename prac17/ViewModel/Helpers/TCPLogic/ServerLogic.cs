using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace prac17.ViewModel.Helpers.TCPLogic
{
    internal class ServerLogic
    {
        public Socket socket;
        public List<Socket> clients = new List<Socket>();
        private string Pickedword;
        public ServerLogic(Socket socket, string Pickedword)
        {
            this.socket = socket;
            this.Pickedword = Pickedword;
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8888);
            socket.Bind(ipPoint);
            socket.Listen(1000);
        }
        public async Task ListenToClients()
        {
            while (true)
            {
                var client = await this.socket.AcceptAsync();
                this.clients.Add(client);
                this.RecieveMessage(client);
                await client.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(Pickedword + "/connect")), SocketFlags.None);
            }
        }
        public async Task RecieveMessage(Socket client)
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                await client.ReceiveAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
                string message = Encoding.UTF8.GetString(bytes);
                foreach (var item in clients)
                    SendMessage(item, message);
            }

        }
        public async Task SendMessage(Socket client, string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await client.SendAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
        }
    }
}
