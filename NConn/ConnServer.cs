using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace NConn
{
    class ConnServer
    {
        const int port = 29999;
        TcpListener server;

        private static ConnServer inst = null;

        private ConnServer()
        {
            server = null;
        }

        public static ConnServer GetInstance()
        {
            if(inst == null)
                inst = new ConnServer();
            return inst;
        }

        public void StartServer()
        {
            if (server == null)
            {
                string sHost = Dns.GetHostName();
                IPAddress addr = Dns.GetHostAddresses(sHost)[0];
                IPEndPoint ep = new IPEndPoint(addr, port);

                server = new TcpListener(ep);
            }

            server.Start();

            server.BeginAcceptTcpClient(clientCallback, server);
        }

        static void clientCallback(IAsyncResult result)
        {
            TcpListener server = (TcpListener) result.AsyncState;

            server.EndAcceptTcpClient(result);
        }
    }
}
