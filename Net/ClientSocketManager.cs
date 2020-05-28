#if SERVER

using System.Collections.Generic;
using System;

namespace U3DTools
{
    public class ClientSocketManager
        :Singleton<ClientSocketManager>
    {
        private ClientSocketManager(){
            clientSockets = new HashSet<ClientSocket>();
        }

        public HashSet<ClientSocket> clientSockets;

        public void Register(ClientSocket clientSocket)
        {
            if(clientSocket==null)
                return;
            clientSockets.Add(clientSocket);
        }

        public void UnRegister(ClientSocket clientSocket)
        {
            if(clientSocket==null)
                return;
            clientSockets.Remove(clientSocket);
        }
        // public void Dispose()
        // {
        //     clientSockets.Clear();
        // }
    }
}

#endif