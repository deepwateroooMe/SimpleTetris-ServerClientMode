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
    public partial class JoinLanGame : Form
    {
        private IPEndPoint ServerEnd
        {
            get
            {
                return listBox1.SelectedItem == null ? null : listBox1.SelectedItem as IPEndPoint;
            }
        }
        public JoinLanGame()
        {
            InitializeComponent();
        }

        private void JoinLanGame_Load(object sender, EventArgs e)
        {
            GameEngine.Instance.Reset();
            LanClient.lanGameJoined = false;

            LanClient.Instance.ServerBroadcastStopEvent += new Action<IPEndPoint>(Client_ServerBroadcastStopEvent);
            LanClient.Instance.ReceiveBroadcast((msg) =>
            {
                listBox1.SafeInvoke(new Action<IPEndPoint>((x) =>
                {
                    if (!listBox1.Items.Contains(x))
                    {
                        listBox1.Items.Add(x);
                    }
                }), msg);
            });
        }

        void Client_ServerBroadcastStopEvent(IPEndPoint obj)
        {
            int index = -1;
            for(int i=0;i<listBox1.Items.Count;i++)
            {
                if (listBox1.Items[i].ToString() == obj.ToString())
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
                listBox1.SafeInvoke(new Action<int>((a) =>
                {
                    listBox1.Items.RemoveAt(a);
                }), index);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("NO Server");
                return;
            }
            if (ServerEnd != null)
            {
                LanClient.Instance.JoinLanGame(ServerEnd.Address);
                button1.AutoSize = true;
                button1.Text = "Waiting...";
                button1.Enabled = false;
            }
        }
    }
}
