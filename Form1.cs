using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Threading;

namespace TCP_UDP_IMAGE
{
    public partial class Form1 : Form
    {
        String interIP_str;
        String exterIP_str;

        int port_server;
        bool check_port = false;

        TcpListener tcpListener;
        TcpClient tcpServer;
        NetworkStream tcpNS;
        //StreamReader tcpSR;
        //StreamWriter tcpSW;
        //bool conn_tcp = false;

        UdpClient udpServer;
        IPEndPoint udpClientEP;
        //string ip_client;
        //int port_client;
        //bool conn_udp = false;]

        bool receive_image = false;

        byte[] buffer_rx;
        byte[] buff_opti;

        String message_tx;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                interIP_str = myInterIP();
                //exterIP_str = myExterIP();

                lb_inip.Text = interIP_str;
                //lb_exip.Text = exterIP_str;
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.ToString());
            }

            try
            {
                port_server = 8000;
                IPAddress addr = IPAddress.Parse(interIP_str);

                TcpListener listener = new TcpListener(addr, port_server);
                listener.Start();
                listener.Stop();
                listener = null;

                tB_myport.Text = port_server.ToString();
                tB_myport.Enabled = false;

                btn_check.Text = "o";
                check_port = true;
            }
            catch (Exception)
            {
                tB_myport.Text = "None";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeServer();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            openServer();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            closeServer();
        }

        private void btn_check_Click(object sender, EventArgs e)
        {
            checkPort();
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && textBox1.Text != "")
            {
                sendMessage(textBox1.Text);
            }
        }

        private void cb_cam_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_cam.Checked)
            {
                receive_image = true;
                sendMessage("FLAG_RTR");
            }
            else if (!cb_cam.Checked)
            {
                receive_image = false;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tcpListener != null || tcpServer != null || udpServer != null)
            {
                MessageBox.Show("서버를 완전히 닫아주세요.");
            }
            else
            {
                this.Close();
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void allStopToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //********************************************************************************************
        //********************************************************************************************
        //**************************************Private Method****************************************
        //********************************************************************************************
        //********************************************************************************************
        private string myInterIP()
        {
            String interIP = "None";

            IPAddress[] addrs = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            if (addrs.Length == 1)
            {
                lb_inip.Visible = true;
                comboBox1.Visible = false;

                IPAddress addr = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
                interIP = addr.ToString();
            }
            else if (addrs.Length > 1)
            {
                lb_inip.Visible = false;
                comboBox1.Visible = true;

                foreach (IPAddress addr in addrs)
                {
                    comboBox1.Items.Add(addr.ToString());
                    if (addr.ToString().Length < 16)
                    {
                        comboBox1.SelectedText = addr.ToString();
                        interIP = addr.ToString();
                    }
                }
            }

            return interIP;
        }

        private string myExterIP()
        {
            //string url = "http://automation.whatismyip.com/n09230945.asp";
            string url = "http://search.naver.com/search.naver?sm=tab_hty.top&where=nexearch&ie=utf8&query=내+아이피+확인&x=35&y=18";
            //string exterIP = new System.Net.WebClient().DownloadString(url);

            WebRequest req = WebRequest.Create(url);
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream());
            string strHtml = sr.ReadToEnd();

            return strHtml;
        }

        private void checkPort()
        {
            if (check_port == false)
            {
                try
                {
                    IPAddress addr = IPAddress.Parse(interIP_str);
                    port_server = Convert.ToInt16(tB_myport.Text.ToString());

                    TcpListener listener = new TcpListener(addr, port_server);
                    listener.Start();
                    listener.Stop();
                    listener = null;

                    tB_myport.Text = port_server.ToString();
                    tB_myport.Enabled = false;

                    btn_check.Text = "o";
                    check_port = true;

                    MessageBox.Show("Port Set.");
                }
                catch (Exception)
                {
                    MessageBox.Show("You Can'ts Use this Port.");
                }
            }
            else if (check_port == true)
            {
                check_port = false;

                tB_myport.Enabled = true;
                btn_check.Text = "c";
            }
        }


        private void openServer()
        {
            if (tB_myport.Enabled == false)
            {
                if (rB_tcp.Checked && tcpListener == null)
                {
                    tcpListener = new TcpListener(IPAddress.Parse(interIP_str), port_server);
                    tcpListener.Start();

                    tcpListener.BeginAcceptTcpClient(tcpAcceptTcpClientCallback, tcpListener);

                    listBox1.Items.Add("TCP Server Open.");
                    listBox1.Items.Add("TCP Server Wait for Client.");
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;

                    btn_start.Enabled = false;
                    btn_close.Enabled = true;
                    gB_mode.Enabled = false;
                    btn_check.Enabled = false;

                    textBox1.Enabled = true;

                }
                else if (rB_udp.Checked && udpServer == null)
                {
                    udpServer = new UdpClient(port_server);
                    udpServer.BeginReceive(udpBeginReceiveCallback, udpServer);

                    listBox1.Items.Add("UDP Server Open.");
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;

                    btn_start.Enabled = false;
                    btn_close.Enabled = true;
                    gB_mode.Enabled = false;
                    btn_check.Enabled = false;

                    textBox1.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Please Select Mode.");
                }
            }
            else if (tB_myport.Enabled == true)
            {
                MessageBox.Show("Please Select Port.");
            }
        }

        private void closeServer()
        {
            if (rB_tcp.Checked)
            {
                if (tcpListener != null)
                {
                    tcpListener.Stop();
                    tcpListener = null;
                }
                if (tcpServer != null)
                {
                    tcpServer.Close();
                    tcpServer = null;
                }
                if (tcpNS != null)
                {
                    tcpNS.Close();
                    tcpNS = null;
                }
            }
            else if (rB_udp.Checked)
            {
                if (udpServer != null)
                {
                    udpServer.Close();
                    udpServer = null;
                    udpClientEP = null;
                }
            }

            btn_start.Enabled = true;
            btn_close.Enabled = false;
            gB_mode.Enabled = true;
            btn_check.Enabled = true;

            textBox1.Enabled = false;

            textBox1.Text = "";
        }

        private void sendMessage(string message)
        {
            if (rB_tcp.Checked && tcpServer != null && tcpNS != null)
            {
                message_tx = message;
                byte[] buffer_tx = Encoding.UTF8.GetBytes(message_tx);
                tcpNS.BeginWrite(buffer_tx, 0, buffer_tx.Length, tcpBeginWriteCallback, tcpNS);
                textBox1.Text = "";
            }
            else if (rB_udp.Checked && udpServer != null && udpClientEP != null)
            {
                message_tx = message;
                byte[] buffer_tx = Encoding.UTF8.GetBytes(message_tx);
                udpServer.BeginSend(buffer_tx, buffer_tx.Length, udpClientEP, udpBeginWriteCallback, udpServer);
                textBox1.Text = "";
            }
            else
            {
                listBox1.Items.Add("Send Failed.");
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
        }
        //********************************************************************************************
        //********************************************************************************************
        //*************************************Callback Method****************************************
        //********************************************************************************************
        //********************************************************************************************

        //--------------------------------------------------------------------------------------------
        //-------------------------------------TCP Callback-------------------------------------------
        //--------------------------------------------------------------------------------------------
        private void tcpAcceptTcpClientCallback(IAsyncResult ar)       // Cross Thread Error
        {
            try
            {
                TcpListener listener = (TcpListener)ar.AsyncState;

                tcpServer = listener.EndAcceptTcpClient(ar);
                tcpNS = tcpServer.GetStream();
                //tcpSR = new StreamReader(tcpNS);
                //tcpSW = new StreamWriter(tcpNS);

                String remoteIP = tcpServer.Client.RemoteEndPoint.ToString();
                Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Connect From " + remoteIP });

                buffer_rx = new byte[tcpServer.ReceiveBufferSize];                              //8192byte
                tcpNS.BeginRead(buffer_rx, 0, buffer_rx.Length, tcpBeginReadCallback, tcpNS);
            }
            catch (ObjectDisposedException)
            {
                Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "TCP Server Close." });
                return;
            }
        }

        private void tcpBeginReadCallback(IAsyncResult ar)          // Cross Thread Error
        {
            try
            {
                NetworkStream ns = (NetworkStream)ar.AsyncState;
                int len = ns.EndRead(ar);

                if (len > 0)
                {
                    if (receive_image == false)
                    {
                        String message_rx = Encoding.UTF8.GetString(buffer_rx, 0, len);
                        Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Receive: " + message_rx });

                        buffer_rx = new byte[tcpServer.ReceiveBufferSize];
                        ns.BeginRead(buffer_rx, 0, buffer_rx.Length, tcpBeginReadCallback, ns);
                    }
                    else if (receive_image == true)
                    {
                        //buff_opti = new byte[count];
                        //Array.Copy(buffer_rx, buff_opti, len);
                        Invoke(new ViewImageDataDelegate(PB_setImage), new object[] { pictureBox1, buffer_rx, len });

                        buffer_rx = new byte[tcpServer.ReceiveBufferSize];
                        ns.BeginRead(buffer_rx, 0, buffer_rx.Length, tcpBeginReadCallback, ns);

                        sendMessage("FLAG_RTR");
                    }
                }
                else
                {
                    Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Client Left." });
                    Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Click Close Button." });
                    //closeServer();
                }
            }
            catch (ObjectDisposedException)
            {
                Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "TCP Server Close." });
                return;
            }
            catch (IOException)
            {
                Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "TCP Server Close." });
                return;
            }
        }

        private void tcpBeginWriteCallback(IAsyncResult ar)
        {
            NetworkStream ns = (NetworkStream)ar.AsyncState;
            ns.EndWrite(ar);

            if (receive_image == false)
            {
                Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Transmit: " + message_tx });
                textBox1.Text = "";
            }
        }
        //--------------------------------------------------------------------------------------------
        //-------------------------------------UDP Callback-------------------------------------------
        //--------------------------------------------------------------------------------------------
        private void udpBeginReceiveCallback(IAsyncResult ar)
        {
            try
            {
                UdpClient u = (UdpClient)ar.AsyncState;

                //udpClientEP = null;
                buffer_rx = u.EndReceive(ar, ref udpClientEP);
                if (receive_image == false)
                {
                    string buffer_str = Encoding.UTF8.GetString(buffer_rx);

                    // 접속된 클라이언트의 IP 주소와 포트 출력
                    Invoke(new AddListBoxDelegate(listBox_Add),
                        new object[] { listBox1, "Receive from " + udpClientEP.Address.ToString() 
                    + ":" + udpClientEP.Port.ToString() });
                    Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Receive: " + buffer_str });

                    u.BeginReceive(udpBeginReceiveCallback, u);
                }
                else if (receive_image == true)
                {
                    Invoke(new ViewImageDataDelegate(PB_setImage), new object[] { pictureBox1, buffer_rx, buffer_rx.Length});

                    u.BeginReceive(udpBeginReceiveCallback, u);

                    sendMessage("FLAG_RTR");
                }
            }
            catch (ObjectDisposedException)
            {
                Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "UDP Server Close." });
                return;
            }
        }

        private void udpBeginWriteCallback(IAsyncResult ar)
        {
            UdpClient u = (UdpClient)ar.AsyncState;

            Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "number of bytes sent: " + u.EndSend(ar).ToString() });
            Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Transmit: " + message_tx });
        }

        private delegate void AddListBoxDelegate(ListBox list, string str);
        private void listBox_Add(ListBox list, string str)
        {
            list.Items.Add(str);
            list.SelectedIndex = list.Items.Count - 1;
        }

        private delegate void EnabledControlDelegate(Control control, bool b);
        private void control_Enabled(Control control, bool b)
        {
            control.Enabled = b;
        }

        private delegate void ViewImageDataDelegate(PictureBox pb, byte[] data, int len);
        private void PB_setImage(PictureBox pb, byte[] data, int len)
        {
            try
            {
                MemoryStream ms = new MemoryStream(data, 0, len);
                Bitmap bitmap = new Bitmap(ms);
                pb.Image = bitmap;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception)
            {
                Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Failed Image Load." });
                return;
            }
        }
    }
}
