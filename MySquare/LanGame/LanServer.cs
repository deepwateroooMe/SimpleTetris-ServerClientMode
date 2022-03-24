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

namespace MySquare.LanGame
{
    public class LanServer:LanBase
    {
        private static LanServer _Instance = null;
        public static LanServer Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new LanServer();
                return _Instance;
            }
        }
        protected override int REMOTE_PORT
        {
            get { return 9173; }
        }
        protected override int LOCAL_PORT
        {
            get { return 4563; }
        }

        public static bool clientConnected = false;

        private UdpClient udpClient = new UdpClient();
        private System.Timers.Timer timerBroadcast = new System.Timers.Timer();
        
        private LanServer()
        {
            timerBroadcast.Interval = 10;
            timerBroadcast.Elapsed += new System.Timers.ElapsedEventHandler(timerBroadcast_Elapsed);
        }
        public void StartBroadcast()
        {
            clientConnected = false;
            timerBroadcast.Enabled = true;
            StartListen();
        }
     
        public void StopBroadcast()
        {
            timerBroadcast.Enabled = false;
        }
        void timerBroadcast_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            SendBroadcastGram();
        }

        /// <summary>
        /// 发送广播报文，建立主机
        /// </summary>
        private void SendBroadcastGram()
        {
            try
            {
                FieldSize fieldSize = new FieldSize() { horizontalCount = 12, verticalCount = 18 };
                DataGram<FieldSize> broadcastData = new DataGram<FieldSize>() { cmd = GramConst.LAN_GAME_CREATED, data = fieldSize };
                byte[] sendBytes = Serializer.Serialize(broadcastData);
                if (!clientConnected)
                {
                    udpClient.Send(sendBytes, sendBytes.Length, IPAddress.Broadcast.ToString(), REMOTE_PORT);
                }
                else
                {
                    timerBroadcast.Enabled = false;
                    Console.WriteLine("Timer disabled");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
