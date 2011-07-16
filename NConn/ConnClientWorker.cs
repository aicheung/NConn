using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace NConn
{
    class ConnClientWorker
    {
        TcpClient mainSock;
        List<TcpListener> slaveSockets;
        Byte[] paramLenBuf;
        Byte[] paramBuf;

        public ConnClientWorker(TcpClient clientConnection)
        {
            clientConnection = mainSock;
        }

        public void Disconnect()
        {
            mainSock.Close();
        }

        struct AsyncParam
        {
            public ConnClientWorker worker;
            public NetworkStream stream;

            public AsyncParam(ConnClientWorker lpThis, NetworkStream stream)
            {
                worker = lpThis;
                this.stream = stream;
            }
        }

        public void GetParamsAndStart()
        {
            paramLenBuf = new Byte[4];
            NetworkStream paramStream = mainSock.GetStream();
            AsyncParam param = new AsyncParam(this, paramStream);
            paramStream.BeginRead(paramLenBuf, 0, 4, endReadParamLen, param);
        }

        private static void endReadParamLen(IAsyncResult result)
        {
            AsyncParam param = (AsyncParam) result.AsyncState;
            int len = BitConverter.ToInt32(param.worker.paramLenBuf, 0);

            ConnClientWorker worker = param.worker;
            worker.paramBuf = new Byte[len];

            param.stream.BeginRead(worker.paramBuf, 0, len, endReadParam, param);
        }

        private static void endReadParam(IAsyncResult result)
        {
            AsyncParam param = (AsyncParam) result.AsyncState;
            char isCustomPort = BitConverter.ToChar(param.worker.paramBuf, 0);
            string sParams = Encoding.ASCII.GetString(param.worker.paramBuf, 2, param.worker.paramBuf.Length-2);
            if (isCustomPort == 'N')
            {
            }
            else
            {
            }
        }
    }
}
