using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySquare.Utilities;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using MySquare.DataGram;

namespace MySquare.LanGame
{
    public abstract class LanBase
    {
        protected System.Timers.Timer timerSendSquareGram = new System.Timers.Timer();
        protected TcpListener tcpListener = null;
        protected OnLineDetector detector = new OnLineDetector();
        protected abstract int LOCAL_PORT { get; }
        protected abstract int REMOTE_PORT { get; }
        protected IPEndPoint remoteEndPoint = null;

        public event Action<byte[]> SquaresReceivedEvent;
        public event Action<int> RemoteGameOverEvent;
        public event Action<MagicEnum> MagicToolReceivedEvent;
        public event Action<IPEndPoint> RemoteConnectedEvent;
        public event Action<IPEndPoint> RemoteOffLineEvent;

        void timerSendSquareGram_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //定时发送方块报文
            var bytes = GameEngine.Instance.SquareBytes;
            SendMsgToRemote(bytes);
        }
        public LanBase()
        {
            tcpListener = new TcpListener(IPAddress.Any, LOCAL_PORT);

            timerSendSquareGram.Interval = 100;//频率不能太快，否则报错
            timerSendSquareGram.Elapsed += new System.Timers.ElapsedEventHandler(timerSendSquareGram_Elapsed);
        }
        public void SendMsgToRemote(byte[] sendBytes)
        {
            if (remoteEndPoint == null)
                return;
            try
            {
                TcpClient tcp = new TcpClient();
                tcp.Connect(new IPEndPoint(remoteEndPoint.Address, REMOTE_PORT));
                var stream = tcp.GetStream();
                stream.Write(sendBytes, 0, sendBytes.Length);
                stream.Close();
                tcp.Close();
            }
            catch (SocketException socketEx)
            {
                Console.WriteLine(socketEx.Message);

            }
        }
        public void SendMsgToRemote(string msg)
        {
            var sendBuf = Encoding.ASCII.GetBytes(msg);
            SendMsgToRemote(sendBuf);
        }
        void detectorRemote_OffLineEvent(object sender, EventArgs e)
        {
            if (RemoteOffLineEvent != null)
            {
                MyEventArgs arg = e as MyEventArgs;
                RemoteOffLineEvent(arg.EndPoint);
            }
        }
        public void StopListen()
        {
            if (tokenSource == null)
                return;
            try
            {
                tokenSource.Cancel();
                tokenSource.Dispose();
            }
            catch
            { }
        }
        CancellationTokenSource tokenSource = null;
        public void StartListen()
        {
            tokenSource = new CancellationTokenSource();
            CancellationToken ct = tokenSource.Token;
            
            var task = Task.Factory.StartNew(() =>
            {
                ct.ThrowIfCancellationRequested();
                try
                {
                    tcpListener.Start();//?套接字地址只允许使用一次???????????????????

                    Byte[] bytes = new Byte[256];
                    while (true)
                    {
                        if (ct.IsCancellationRequested)
                        {
                            try
                            {
                                ct.ThrowIfCancellationRequested();
                            }
                            catch
                            {
                                
                                break;
                            }
                        }

                        Console.WriteLine("Waiting for a connection...==> " + DateTime.Now.Second.ToString());
                        TcpClient client = tcpListener.AcceptTcpClient();
                        ProcessReceivedData(client);
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("{0}=>接收异常: {1}", GameEngine.IsServer ? "Server" : "Client", ex);
                    //tcpListener.Stop();
                    //tcpListener = new TcpListener(IPAddress.Any, LOCAL_PORT);
                    //StartListen();
                }
                finally
                {
                    tcpListener.Stop();
                }
            }, ct);

            //ThreadPool.QueueUserWorkItem(obj =>
            //{
            //    try
            //    {
            //        tcpListener.Start();
            //        Byte[] bytes = new Byte[256];
            //        while (true)
            //        {
            //            Console.WriteLine("Waiting for a connection...==> " + DateTime.Now.Second.ToString());
            //            TcpClient client = tcpListener.AcceptTcpClient();
            //            ProcessReceivedData(client);
            //        }
            //    }
            //    catch (SocketException ex)
            //    {
            //        Console.WriteLine("SocketException: {0}", ex);
            //    }
            //    finally
            //    {
            //        tcpListener.Stop();
            //    }
            //});
        }

        public void StartRepeatSend()
        {
            timerSendSquareGram.Enabled = true;
        }
        public void StopRepeatSend()
        {
            timerSendSquareGram.Enabled = false;
        }
        public event Action<int> RemoteScoreChangedEvent;
        private void ProcessReceivedData(TcpClient client)
        {
            detector.OffLineEvent += new EventHandler(detectorRemote_OffLineEvent);
            detector.Start();

            NetworkStream stream = client.GetStream();
            int i;
            Byte[] bytes = new Byte[256];
            List<byte> buf = new List<byte>();
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                buf.AddRange(bytes);
            }

            detector.RemoteEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
            detector.Mark();

            var t = buf.ToArray();
            var dataGramReceived = Serializer.Deserialize<DataGram<List<SquareData>>>(t);
            if (dataGramReceived != null)
            {
                switch (dataGramReceived.cmd)
                {
                    case GramConst.SQUARE_DATA:
                        if (SquaresReceivedEvent != null)
                            SquaresReceivedEvent(t);
                        break;
                    case GramConst.REMOTE_CONNECTED:
                        remoteEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                        if (RemoteConnectedEvent != null)
                            RemoteConnectedEvent(remoteEndPoint);
                        break;
                }
            }
            else
            {
                var magicGram = Serializer.Deserialize<DataGram<MagicEnum>>(t);
                if (magicGram != null)
                {
                    if (magicGram.cmd == GramConst.MAGIC_TOOL)
                    {
                        if (MagicToolReceivedEvent != null)
                            MagicToolReceivedEvent(magicGram.data);
                    }
                }
                else
                {
                    var intGram = Serializer.Deserialize<DataGram<int>>(t);
                    if (intGram != null)
                    {
                        if (intGram.cmd == GramConst.GAME_OVER)
                        {
                            if (RemoteGameOverEvent != null)
                            {
                                GameEngine.Instance.StopListen();
                                GameEngine.Instance.MagicPower = 0;
                                RemoteGameOverEvent(intGram.data);
                            }
                        }
                        else if (intGram.cmd == GramConst.SCORE_CHANGE)
                        {
                            if (RemoteScoreChangedEvent != null)
                                RemoteScoreChangedEvent(intGram.data);
                        }
                    }
                }
            }
            client.Close();
        }
    }
}
