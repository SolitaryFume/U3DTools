using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace U3DTools
{
    public class ClientSocket : IDisposable
    {
#if SERVER
        public long m_lastPing { get; set; }
#endif
        private byte[] m_receiveBuffers;
        // private ArraySegment<byte> m_receiveBuffers;
        //TODO 合并 m_receiveBuffers,m_msgBuffer;
        private MsgBuffer m_msgBuffer;

        private Socket m_socket;
        // private SocketAsyncEventArgs e;

        public ClientSocket()
        {
            // m_receiveBuffers = new ArraySegment<byte>();
            m_receiveBuffers = new byte[256];
            m_msgBuffer = new MsgBuffer();
        }

        public void SetSocket(Socket socket)
        {
            if (m_socket != null)
                throw new Exception("no dispose !");

            if (socket == null)
                throw new ArgumentNullException(nameof(socket));
            m_socket = socket;
        }

        public void Dispose()
        {
            System.Console.WriteLine("客户端断开连接");
            m_socket?.Close();
            m_socket = null;

            m_msgBuffer.Dispose();
#if SERVER
            m_lastPing = 0;
            ClientSocketManager.Instance.UnRegister(this);
            PoolSingleton<ClientSocket>.Instance.Recycle(this);
#endif

        }

        // public async void ReceiveAsync()
        // {
        //     int receiveCount;
        //     while (m_socket != null && (receiveCount = await m_socket.ReceiveAsync(m_receiveBuffers, SocketFlags.None)) != 0 )
        //     {
        //         m_msgBuffer.AddData(m_receiveBuffers.Take(receiveCount));
        //     }

        //     System.Console.WriteLine("client disconnect !");
        //     Dispose();
        // }

        public void BeginReceive()
        {
            if(m_socket==null || !m_socket.Connected)
                return;
            
            m_socket.BeginReceive(m_receiveBuffers,0,m_receiveBuffers.Length,SocketFlags.None,ReceiveCallBack,m_socket);
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            var count = m_socket.EndReceive(ar);
            if(count<=0)
            {
                Dispose();
            }
            else
            {
                m_msgBuffer.AddData(m_receiveBuffers.Take(count));
            }
        }

        public async void SendAsync(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (m_socket == null || !m_socket.Connected)
                return;

            // await m_socket.SendAsync(data, SocketFlags.None);
            await m_socket.SendAsync(new ArraySegment<byte>(data),SocketFlags.None);
        }

        public static ClientSocket Create(string ip, int port)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var client = new ClientSocket();
            client.SetSocket(socket);
            client.ConnectAsync(ip, port);
            client.BeginReceive();
            return client;
        }
        public async void ConnectAsync(string ip, int port)
        {
            if (m_socket == null || m_socket.Connected)
                return;

            try
            {
                await m_socket.ConnectAsync(ip, port);
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex);
            }
        }
    }
}