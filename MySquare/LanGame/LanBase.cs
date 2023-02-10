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

namespace MySquare.LanGame {

    public abstract class LanBase {

        protected System.Timers.Timer timerSendSquareGram = new System.Timers.Timer();
        protected TcpListener tcpListener = null;
        protected OnLineDetector detector = new OnLineDetector();
        protected abstract int LOCAL_PORT { get; }
        protected abstract int REMOTE_PORT { get; }
        protected IPEndPoint remoteEndPoint = null;
        
// 小游戏的时候，可以这么一个一个地写和调用回调.但是当游戏大到一定的规模，就需要比较好的系统化的封装        
        public event Action<byte[]> SquaresReceivedEvent;
        public event Action<int> RemoteGameOverEvent;
        public event Action<MagicEnum> MagicToolReceivedEvent;
        public event Action<IPEndPoint> RemoteConnectedEvent;
        public event Action<IPEndPoint> RemoteOffLineEvent;

        void timerSendSquareGram_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            // 定时发送方块报文: 这里应该说的是客户端 呀
            var bytes = GameEngine.Instance.SquareBytes;
            SendMsgToRemote(bytes); // <<<<<<<<<<<<<<<<<<<< 
        }

        public LanBase() {
            tcpListener = new TcpListener(IPAddress.Any, LOCAL_PORT);
            timerSendSquareGram.Interval = 100;// 频率不能太快，否则报错
            timerSendSquareGram.Elapsed += new System.Timers.ElapsedEventHandler(timerSendSquareGram_Elapsed);
        }

        public void SendMsgToRemote(byte[] sendBytes) {
            if (remoteEndPoint == null)
                return;
            try { // 效果问题： 既需要定期，保持时间间隔地发送报文给服务器，又每次都开户新的，应该一定会造成大量的GC等，另外就是网络请求的延迟，所以需要封闭，减少不必要的GC，更是为了降低延迟
                TcpClient tcp = new TcpClient();
                tcp.Connect(new IPEndPoint(remoteEndPoint.Address, REMOTE_PORT)); // 每次都来从头来一遍？！！！
                // 这里的过程要弄清楚一点儿： 这里好像是linux的内核
                var stream = tcp.GetStream();
                stream.Write(sendBytes, 0, sendBytes.Length); // 这里的写，是写到系统的这个缓冲区里，
                stream.Close(); // 然后应用程序就可以不用管了，因为系统底层的framework，会自动把这些发出去
                tcp.Close(); // 释放资源
            }
            catch (SocketException socketEx) {
                Console.WriteLine(socketEx.Message);
            }
        }
        public void SendMsgToRemote(string msg) {
            var sendBuf = Encoding.ASCII.GetBytes(msg);
            SendMsgToRemote(sendBuf);
        }
        void detectorRemote_OffLineEvent(object sender, EventArgs e) {
            if (RemoteOffLineEvent != null) {
                MyEventArgs arg = e as MyEventArgs;
                RemoteOffLineEvent(arg.EndPoint);
            }
        }
        public void StopListen() {
            if (tokenSource == null)
                return;
            try {
                tokenSource.Cancel();
                tokenSource.Dispose();
            }
            catch { }
        }

        CancellationTokenSource tokenSource = null;
        public void StartListen() {
            tokenSource = new CancellationTokenSource();
            CancellationToken ct = tokenSource.Token;
            // 为什么会需要封闭异步任务： 这里是用的系统自带的任务，没有使用线程池;另则不方便享受，异步调用写成同步的流式编程？听起来狠酷的样子，还不是狠懂
            var task = Task.Factory.StartNew(() => { // 每次开启新任务，即使系统自己带有缓冲池，仍然是一个狠大的overhead,
                    ct.ThrowIfCancellationRequested();
                    try {
                        tcpListener.Start();// ?套接字地址只允许使用一次???????????????????
                        Byte[] bytes = new Byte[256];
                        while (true) {
                            if (ct.IsCancellationRequested) {
                                try {
                                    ct.ThrowIfCancellationRequested();
                                }
                                catch {
                                    break;
                                }
                            }
                            Console.WriteLine("Waiting for a connection...==> " + DateTime.Now.Second.ToString());
                            TcpClient client = tcpListener.AcceptTcpClient();
                            ProcessReceivedData(client);
                        }
                    }
                    catch (SocketException ex) {
                        Console.WriteLine("{0}=>接收异常: {1}", GameEngine.IsServer ? "Server" : "Client", ex);
                        // tcpListener.Stop();
                        // tcpListener = new TcpListener(IPAddress.Any, LOCAL_PORT);
                        // StartListen();
                    }
                    finally {
                        tcpListener.Stop();
                    }
                }, ct);
            // ThreadPool.QueueUserWorkItem(obj =>
            // {
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
            // });
        }
        public void StartRepeatSend() {
            timerSendSquareGram.Enabled = true;
        }
        public void StopRepeatSend() {
            timerSendSquareGram.Enabled = false;
        }
        public event Action<int> RemoteScoreChangedEvent;
// 从客户端收到消息数据，要如何处理呢？
        private void ProcessReceivedData(TcpClient client) {
            detector.OffLineEvent += new EventHandler(detectorRemote_OffLineEvent); // 注册下线通知监听回调，这些回调会造成回调地狱吗？
            detector.Start();
            NetworkStream stream = client.GetStream();
            // 这里开始看见：读的速度极慢，读的效率狠低，一次只能说256个字节，太短了！！！            
            int i;
            Byte[] bytes = new Byte[256];
            List<byte> buf = new List<byte>();
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0) { // 只要还没有读完，就以这个256字节长短为片段重复读，直到读完为止
                buf.AddRange(bytes);
            }
            detector.RemoteEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
            detector.Mark();
            var t = buf.ToArray(); // 字节数组
            var dataGramReceived = Serializer.Deserialize<DataGram<List<SquareData>>>(t); // 反序列化 字节数组： 反成特定的类型
            if (dataGramReceived != null) {
                switch (dataGramReceived.cmd) {
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
            } else { // 这里是怎么转过来的，没有看明白
                var magicGram = Serializer.Deserialize<DataGram<MagicEnum>>(t);
                if (magicGram != null) {
                    if (magicGram.cmd == GramConst.MAGIC_TOOL) {
                        if (MagicToolReceivedEvent != null)
                            MagicToolReceivedEvent(magicGram.data);
                    }
                } else {
                    var intGram = Serializer.Deserialize<DataGram<int>>(t);
                    if (intGram != null) {
                        if (intGram.cmd == GramConst.GAME_OVER) {
                            if (RemoteGameOverEvent != null) {
                                GameEngine.Instance.StopListen();
                                GameEngine.Instance.MagicPower = 0;
                                RemoteGameOverEvent(intGram.data);
                            }
                        } else if (intGram.cmd == GramConst.SCORE_CHANGE) {
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

