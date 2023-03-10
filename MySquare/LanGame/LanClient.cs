using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using MySquare.Utilities;
using MySquare.DataGram;

namespace MySquare.LanGame {

    public class LanClient : LanBase {

        class UdpState {
            public UdpClient Udp { set; get; } // 这里为什么选用的是UDP？
            public IPEndPoint EndPoint { set; get; } // 目的：标明客户端身份，是哪个客户端？
        }
        protected override int REMOTE_PORT {
            get { return 4563; }
        }
        protected override int LOCAL_PORT {
            get { return 9173; }
        }
        private List<OnLineDetector> broadcastDetectorList = new List<OnLineDetector>();
        public event Action<IPEndPoint> ServerBroadcastStopEvent;
        public static bool lanGameJoined = false;
        private LanClient() {
        }
        private static LanClient _Instance = null;
        public static LanClient Instance {
            get {
                if (_Instance == null)
                    _Instance = new LanClient();
                return _Instance;
            }
        }
        // 当某个客户端下线的时候，会知会服务器，服务器收到通知后所作的工作： 定义在这里，广播告知其它这个客户端的小伙伴们，这个客户端下线了，并从自己的连接的客户端链表里移除这个客户端
        void broadcastDetector_OffLineEvent(object sender, EventArgs e) {
            if (ServerBroadcastStopEvent != null) {
                var end = (e as MyEventArgs).EndPoint;
                ServerBroadcastStopEvent(end); // 广播通知其它必要的客户端
                broadcastDetectorList.Remove(sender as OnLineDetector); // 自己的维护管理
            }
        }
        public void ReceiveBroadcast(Action<IPEndPoint> callback) {
            ThreadPool.QueueUserWorkItem(obj => {
                    // 只要未加入游戏，则不断循环接收广播： 这里好像有点儿没有看懂，是在做什么
                    while (!lanGameJoined) {
                        try {
                            IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, LOCAL_PORT);
                            UdpClient udpClient = new UdpClient(remoteIpEndPoint);
                        
                            UdpState s = new UdpState() { Udp = udpClient, EndPoint = remoteIpEndPoint };
                            udpClient.BeginReceive(r => {
                                    var state = r.AsyncState as UdpState;
                                    var u = state.Udp;
                                    remoteEndPoint = state.EndPoint;
                                    byte[] receiveBytes = u.EndReceive(r, ref remoteEndPoint);
#region 检测某服务器广播是否停止
                                    var o = broadcastDetectorList.Find(d => {
                                            string a = d.RemoteEndPoint.Address.ToString() + ":" + d.RemoteEndPoint.Port.ToString();
                                            string b = remoteEndPoint.Address.ToString() + ":" + remoteEndPoint.Port.ToString();
                                            return a == b;
                                        });
                                    OnLineDetector detector = null;
                                    if (o == null) {
                                        detector = new OnLineDetector();
                                        detector.RemoteEndPoint = remoteEndPoint;
                                        broadcastDetectorList.Add(detector);
                                        detector.OffLineEvent += new EventHandler(broadcastDetector_OffLineEvent);
                                        detector.Start();
                                    }
                                    else {
                                        detector = o;
                                    }
                                    detector.Mark();
#endregion
                                    DataGram<FieldSize> gram = Serializer.Deserialize<DataGram<FieldSize>>(receiveBytes);
                                    udpClient.Close();
                                    if (gram != null && gram.cmd == GramConst.LAN_GAME_CREATED)
                                        callback(remoteEndPoint);
                                }, s);
                        }
                        catch (Exception ex) {
                            Console.WriteLine("接收广播："+ex.ToString());
                        }
                    }
                });
        }
        public void JoinLanGame(IPAddress serverIp) {
            if (lanGameJoined == false) {
                lanGameJoined = true;
                DataGram<List<SquareData>> data = new DataGram<List<SquareData>>() { cmd = GramConst.REMOTE_CONNECTED, data = null };
                var buf = Serializer.Serialize(data);
                SendMsgToRemote(buf);
                StartListen();
                StartRepeatSend();
            }
        }
    }
}
