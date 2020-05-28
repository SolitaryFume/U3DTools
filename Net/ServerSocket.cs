#if SERVER
using System.Net;
using System.Net.Sockets;
using System;

namespace U3DTools
{
    public class ServerSocket : Singleton<ServerSocket>,IDisposable
    {   
        public static string ip;
        public static int port = 88;

        private Socket m_socket;

        private ServerSocket(){}
        public void Dispose()
        {
            m_socket.Close();
        }

        public async void Start()
        {
            IPAddress ipAddress = IPAddress.Any;
            if (!string.IsNullOrEmpty(ip))
            {
                ipAddress = IPAddress.Parse(ip);
            }
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m_socket.Bind(ipEndPoint);
            m_socket.Listen(100);

            System.Console.WriteLine($"server init >> {ipEndPoint.ToString()}");
            while (m_socket != null)
            {
                Socket socket = await m_socket.AcceptAsync();
                System.Console.WriteLine("new clinet connect!");
                var client = PoolSingleton<ClientSocket>.Instance.Fetch();
                client.SetSocket(socket);
                ClientSocketManager.Instance.Register(client);
            }
        }
    }
}
#endif