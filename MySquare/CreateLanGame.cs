using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using MySquare.LanGame;

namespace MySquare
{
    public partial class CreateLanGame : Form
    {
        public CreateLanGame()
        {
            InitializeComponent();
        }

        private void CreateLanGame_Load(object sender, EventArgs e)
        {
            GameEngine.Instance.Reset();
            GameEngine.IsServer = true;
            
            LanServer.Instance.RemoteConnectedEvent += new Action<IPEndPoint>(server_ClientConnectedEvent);
            LanServer.Instance.StartBroadcast();
        }

        void server_ClientConnectedEvent(IPEndPoint obj)
        {
            if (obj == null)
                return;
            LanServer.clientConnected = true;
            label1.SafeInvoke(new Action(() =>
            {
                label1.Text = obj.ToString() + " connected!";
                button1.Enabled = true;
            }));
        }
        public event Action GameStartEvent;
        private void button1_Click(object sender, EventArgs e)
        {
            GameEngine.IsLanGameStarted = true;
            LanServer.Instance.StartRepeatSend();
            this.DialogResult = DialogResult.OK;
            if (GameStartEvent != null)
                GameStartEvent();
        }

        private void CreateLanGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            LanServer.Instance.StopBroadcast();
        }
    }
}
