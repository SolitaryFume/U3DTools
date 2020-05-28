using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace U3DTools
{
    public class ClientSocket : IDisposable
    {
#if SERVER
        public long m_lastPing{get;set;}
#endif
        private byte[] m_receiveBuffers;//TODO 合并 m_receiveBuffers,m_msgBuffer;
        private MsgBuffer m_msgBuffer;

        private Socket m_socket;

        public ClientSocket() 
        {
            m_receiveBuffers = new byte[256];
            m_msgBuffer = new MsgBuffer();
        }
        
        public void SetSocket(Socket socket)
        {
            if(m_socket!=null)  
                throw new Exception("no dispose !");

            if(socket==null)
                throw new ArgumentNullException(nameof(socket));
            m_socket = socket;
        }
 
        public void Dispose()
        {
            m_socket?.Close();
            m_socket = null;
            
            m_msgBuffer.Dispose();
#if SERVER
            m_lastPing = 0;
            ClientSocketManager.Instance.UnRegister(this);
            PoolSingleton<ClientSocket>.Instance.Recycle(this);
#endif

        }

        public async void ReceiveAsync()
        {
            int receiveCount;
            while (m_socket!=null && (receiveCount = await m_socket.ReceiveAsync(m_receiveBuffers,SocketFlags.None)) !=0)
            {
                m_msgBuffer.AddData(m_receiveBuffers.Take(receiveCount));
            }

            System.Console.WriteLine("client disconnect !");
            Dispose();
        }
    
        public async void SendAsync(byte[] data)
        {
            if(data==null)
                throw new ArgumentNullException(nameof(data));

            if(m_socket==null || !m_socket.Connected)
                return;
            
            await m_socket.SendAsync(data,SocketFlags.None);
        }

        public static ClientSocket Create(string ip,int port)
        {
            var socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            var client = new ClientSocket();
            client.SetSocket(socket);
            client.ConnectAsync(ip,port);
            client.ReceiveAsync();
            return client;
        }
        public async void ConnectAsync(string ip,int port)
        {
            if(m_socket==null || m_socket.Connected)
                return;
        
            if(IPAddress.TryParse(ip,out var address)){

                IPEndPoint ipEndPoint = new IPEndPoint(address,port);
                await m_socket.ConnectAsync(ipEndPoint);
            }
        }
    }
}