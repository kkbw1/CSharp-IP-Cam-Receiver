using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;

namespace TCP_UDP_IMAGE
{
    public partial class Form1 : Form
    {
        const string FILE_START = "@@START@@";
        const string FILE_END = "@@END@@";

        static readonly byte[] SOI = new byte[] { 0xFF, 0xD8 };
        static readonly byte[] EOI = new byte[] { 0xFF, 0xD9 };

        String strInterIP;
        int iPortServer;
        bool bCheckPort = false;

        private TServer tServer;
        private UDPService uServer;
        private List<byte> listRxBuffer = new List<byte>();
        private List<byte> listFileBuffer = new List<byte>();
        private byte[] JpegImg;
        private bool bFileSending = false;

        bool bDisplayImage = false;

        String strMsgTx;

        //TcpListener tcpListener;
        //TcpClient tcpServer;
        //NetworkStream tcpNS;

        //UdpClient udpServer;
        //IPEndPoint udpClientEP;

        //byte[] bufferRx;
       
        //********************************************************************************************//
        //                                                                                            //
        //                                   Event Handlers                                           //
        //                                                                                            //
        //********************************************************************************************//

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /* Getting the internal IP address in my local network area */
            try
            {
                strInterIP = myInterIP();
                lb_inip.Text = strInterIP;
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.ToString());
            }

            /* Checking if the port is available for TCP and UDP both */
            try
            {
                iPortServer = 8000;
                IPAddress addr = IPAddress.Parse(strInterIP);

                TcpListener listener = new TcpListener(addr, iPortServer);
                listener.Start();
                listener.Stop();
                listener = null;

                UdpClient udpServ = new UdpClient(iPortServer);
                udpServ.Close();
                udpServ = null;

                tB_myport.Text = iPortServer.ToString();
                tB_myport.Enabled = false;

                btn_check.Text = "o";
                bCheckPort = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message:\r\n" + ex.ToString(), "Error: The port may be already used.");
                tB_myport.Text = "None";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            btn_start.Enabled = true;
            btn_close.Enabled = false;
            gB_mode.Enabled = true;
            btn_check.Enabled = true;

            textBox1.Enabled = false;

            textBox1.Text = "";

            if (tServer != null) tServer.ServerClose();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (tB_myport.Enabled == false)
            {
                if (rB_tcp.Checked)
                {
                    if (tServer == null) tServer = new TServer(TcpGetDataArrived);
                    tServer.ServerStartListen(comboBox1.Text, Convert.ToInt16(tB_myport.Text));

                    btn_start.Enabled = false;
                    btn_close.Enabled = true;
                    gB_mode.Enabled = false;
                    btn_check.Enabled = false;

                    //textBox1.Enabled = true;
                }
                else if (rB_udp.Checked)
                {
                    if (uServer == null) uServer = new UDPService(UdpGetDataArrived);
                    uServer.PrepareClient(Convert.ToInt16(tB_myport.Text));

                    btn_start.Enabled = false;
                    btn_close.Enabled = true;
                    gB_mode.Enabled = false;
                    btn_check.Enabled = false;

                    //textBox1.Enabled = true;
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

        private void btn_close_Click(object sender, EventArgs e)
        {
            if (rB_tcp.Checked)
            {
                btn_start.Enabled = true;
                btn_close.Enabled = false;
                gB_mode.Enabled = true;
                btn_check.Enabled = true;

                textBox1.Enabled = false;

                textBox1.Text = "";

                if (tServer != null) tServer.ServerClose();
            }
            else if (rB_udp.Checked)
            {
                btn_start.Enabled = true;
                btn_close.Enabled = false;
                gB_mode.Enabled = true;
                btn_check.Enabled = true;

                textBox1.Enabled = false;

                textBox1.Text = "";

                if (uServer != null) uServer.ServerClose();
            }
        }

        private void btn_check_Click(object sender, EventArgs e)
        {
            if (bCheckPort == false)
            {
                try
                {
                    IPAddress addr = IPAddress.Parse(strInterIP);
                    iPortServer = Convert.ToInt16(tB_myport.Text.ToString());

                    TcpListener listener = new TcpListener(addr, iPortServer);
                    listener.Start();
                    listener.Stop();
                    listener = null;

                    UdpClient udpServer = new UdpClient(iPortServer);
                    udpServer.Close();
                    udpServer = null;

                    tB_myport.Text = iPortServer.ToString();
                    tB_myport.Enabled = false;

                    btn_check.Text = "o";
                    bCheckPort = true;

                    MessageBox.Show("Port Set.", "Port");
                }
                catch (Exception)
                {
                    MessageBox.Show("This port is not available.", "Port");
                }
            }
            else if (bCheckPort == true)
            {
                bCheckPort = false;

                tB_myport.Enabled = true;
                btn_check.Text = "c";
            }
        }

        private void btnSaveImg_Click(object sender, EventArgs e)
        {
            if (JpegImg != null && JpegImg.Length != 0)
            {
                try
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Jpeg(*.jpg)|*.jpg";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo fiSave = new FileInfo(sfd.FileName + sfd.DefaultExt);
                        FileStream fsSave = new FileStream(fiSave.FullName, FileMode.Create);

                        fsSave.Write(JpegImg, 0, JpegImg.Length);
                        fsSave.Close();
                        fsSave = null;

                        MessageBox.Show("The image is saved to " + fiSave.FullName + " successfully.", "Saved successfully");

                        fiSave = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error has occured");
                }
                finally
                {

                }
            }
            else
            {
                MessageBox.Show("Image data doesn't exist.", "Save Image");
            }
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
                bDisplayImage = true;
            else if (!cb_cam.Checked)
                bDisplayImage = false;
        }

        private void tmrConnCheck_Tick(object sender, EventArgs e)
        {
            if (rB_tcp.Checked)
            {
                if (tServer == null)
                {
                    lblStat.Text = "Conn: NULL";
                }
                else
                {
                    csConnStatus conn = tServer.ServerStatus();
                    lblStat.Text = "Conn: " + conn.ToString();
                }
            }
            else if (rB_udp.Checked)
            {
                if (uServer == null)
                {
                    lblStat.Text = "Conn: NULL";
                }
                else
                {
                    csUdpConnStatus conn = uServer.ServerStatus();
                    lblStat.Text = "Conn: " + conn.ToString();
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (uServer.ServerStatus() != csUdpConnStatus.Closed || tServer.ServerStatus() != csConnStatus.Closed)
                MessageBox.Show("Close and dispose all the servers before exit.");
            else
                this.Close();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        //********************************************************************************************//
        //                                                                                            //
        //                               User Defined Sub-routines                                    //
        //                                                                                            //
        //********************************************************************************************//
        private void ArrivedDataProcessing(byte[] data)
        {
            listRxBuffer.AddRange(data);
            String strRxBuffer = Encoding.ASCII.GetString(listRxBuffer.ToArray());
            int idxFileStart = strRxBuffer.IndexOf(FILE_START);
            int idxFileEnd = strRxBuffer.IndexOf(FILE_END);

            if (idxFileStart != -1 && idxFileEnd != -1)
            {
                if (idxFileStart < idxFileEnd)
                {
                    bFileSending = false;

                    byte[] fileTemp = new byte[idxFileEnd - idxFileStart];
                    listRxBuffer.CopyTo(idxFileStart, fileTemp, 0, idxFileEnd - idxFileStart);
                    listRxBuffer.RemoveRange(0, idxFileEnd + FILE_END.Length);

                    listFileBuffer.AddRange(fileTemp);
                    listFileBuffer.RemoveRange(0, FILE_START.Length);

                    if (JpegImg != null || JpegImg.Length != 0)
                        JpegImg = null;
                    JpegImg = listFileBuffer.ToArray();
                    listFileBuffer.Clear();

                    Invoke(new ViewImageDataDelegate(PB_setImage), new object[] { pictureBox2, JpegImg, JpegImg.Length });
                }
                else if (idxFileStart > idxFileEnd && bFileSending == true)
                {

                }
            }
            if (idxFileStart != -1 && idxFileEnd == -1 && bFileSending == false)
            {
                bFileSending = true;

                byte[] fileTemp = new byte[listRxBuffer.Count - idxFileStart];
                listRxBuffer.CopyTo(idxFileStart, fileTemp, 0, fileTemp.Length);
                listRxBuffer.RemoveRange(idxFileStart, fileTemp.Length);

                listFileBuffer.AddRange(fileTemp);
            }
            else if (idxFileStart == -1 && idxFileEnd != -1 && bFileSending == true)
            {
                bFileSending = false;

                byte[] fileTemp = new byte[idxFileEnd];
                listRxBuffer.CopyTo(0, fileTemp, 0, fileTemp.Length);
                listRxBuffer.RemoveRange(0, fileTemp.Length + FILE_END.Length);

                listFileBuffer.AddRange(fileTemp);
                listFileBuffer.RemoveRange(0, FILE_START.Length);

                if (JpegImg != null)
                    JpegImg = null;
                
                JpegImg = listFileBuffer.ToArray();
                listFileBuffer.Clear();

                Invoke(new ViewImageDataDelegate(PB_setImage), new object[] { pictureBox2, JpegImg, JpegImg.Length });
            }
            else if (idxFileStart == -1 && idxFileEnd == -1 && bFileSending == true)
            {
                listFileBuffer.AddRange(listRxBuffer);
                listRxBuffer.Clear();
            }
            else
            {
                if (bDisplayImage)
                {
                    showPreviewImage(listRxBuffer);
                }
                else
                {
                    Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "================" });
                    Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Length: " + strRxBuffer.Length.ToString() });
                    if (strRxBuffer.Length > 500)
                        Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Data: too big to convert to String" });
                    else
                        Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Data: " + strRxBuffer });

                    listRxBuffer.Clear();
                }
            }
        }

        private void TcpGetDataArrived()
        {
            byte[] temp = tServer.GetRcvBytes();
            if (temp.Length != 0)
            {
                ArrivedDataProcessing(temp);
            }
        }

        private void UdpGetDataArrived()
        {
            byte[] temp = uServer.GetRcvBytes();
            if (temp.Length != 0)
            {
                ArrivedDataProcessing(temp);
            }
        }

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
                        interIP = addr.ToString();
                    }
                }
                comboBox1.SelectedText = interIP;
            }

            return interIP;
        }

        private void sendMessage(string message)
        {
            /* send a message through TCP */
            if (rB_tcp.Checked && tServer.ServerStatus() == csConnStatus.Connected)
            {
                strMsgTx = message;
                byte[] buffer_tx = Encoding.UTF8.GetBytes(strMsgTx);

                //tcpNS.BeginWrite(buffer_tx, 0, buffer_tx.Length, tcpBeginWriteCallback, tcpNS);

                if(textBox1.Text != "")
                    textBox1.Text = "";
            }
            /* send a message through UDP */
            else if (rB_udp.Checked && uServer.ServerStatus() == csUdpConnStatus.Opened)
            {
                strMsgTx = message;
                byte[] buffer_tx = Encoding.UTF8.GetBytes(strMsgTx);

                //udpServer.BeginSend(buffer_tx, buffer_tx.Length, udpClientEP, udpBeginWriteCallback, udpServer);

                if (textBox1.Text != "")
                    textBox1.Text = "";
            }
            /* sending messages is not possible */
            else
            {
                listBox1.Items.Add("Send Failed.");
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
        }

        private void showPreviewImage(List<byte> listBuffer)
        {
            int idxStart = -1;
            int idxEnd = -1;

            int packetLength = listBuffer.Count;
            for (int i = 0; i < packetLength - 1; i++)
            {
                if (listBuffer[i] == SOI[0] && listBuffer[i + 1] == SOI[1])
                {
                        idxStart = i;           // where 0xFF is located before 0xD8
                        break;
                }
            }

            for (int i = 0; i < packetLength - 1; i++)
            {
                if (listBuffer[i] == EOI[0] && listBuffer[i + 1] == EOI[1])
                {
                    idxEnd = i + 1;       // where 0xD9 is located after 0xFF 
                    break;
                }
            }

            byte[] buffPreviewJpeg;
            if (idxStart != -1 && idxEnd != -1)
            {
                if (idxStart < idxEnd)
                {
                    //Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "1" });

                    /* copy data from idxStart to idxEnd */
                    buffPreviewJpeg = new byte[idxEnd - idxStart + 1];
                    listBuffer.CopyTo(idxStart, buffPreviewJpeg, 0, buffPreviewJpeg.Length);
                    listBuffer.RemoveRange(idxStart, buffPreviewJpeg.Length);

                    //byte[] buffFirst;
                    //byte[] buffLast;
                    //if (idxStart != 0)
                    //{
                    //    /*take the data before the idxStart and after idxEnd */
                    //    buffFirst = new byte[idxStart];
                    //    buffLast = new byte[packetLength - idxEnd - 1];
                    //    Array.Copy(buffRecv, 0, buffFirst, 0, idxStart);
                    //    Array.Copy(buffRecv, idxEnd, buffLast, 0, packetLength - idxEnd - 1);
                    //}
                    //else
                    //{
                    //    buffLast = new byte[packetLength - idxEnd - 1];
                    //    Array.Copy(buffRecv, idxEnd, buffLast, 0, packetLength - idxEnd - 1);
                    //}

                    ///* copy data from idxStart to idxEnd */
                    //buffJpeg = new byte[idxEnd - idxStart + 1];
                    //Array.Copy(buffRecv, idxStart, buffJpeg, 0, idxEnd - idxStart + 1);

                    //listStacked.AddRange(buffLast);

                    /* display the jpeg data in the picturebox */
                    Invoke(new ViewImageDataDelegate(PB_setImage), new object[] { pictureBox1, buffPreviewJpeg, buffPreviewJpeg.Length });
                }
                else if (idxStart > idxEnd)
                {
                    //Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "2" });

                    ///* shouldn't be data existed between idxEnd and idxStart */
                    //byte[] buffFirst = new byte[idxEnd + 1];
                    //byte[] buffLast = new byte[packetLength - idxStart];

                    ///* copy data before and including idxEnd */
                    //Array.Copy(buffRecv, 0, buffFirst, 0, idxEnd + 1);
                    ///* copy data after and including idxStart */
                    //Array.Copy(buffRecv, idxStart, buffLast, 0, packetLength - idxStart);

                    ///* append the data until idxEnd and convert to the jpeg data */
                    //listStacked.AddRange(buffFirst);
                    //buffJpeg = listStacked.ToArray();

                    ///* make new list buffer to add new data that is not transferred completely */
                    //listStacked.Clear();
                    //listStacked.AddRange(buffLast);
                }
            }
            else if (idxStart != -1 && idxEnd == -1)        // only idxStart is detected
            {
                //Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "3" });

                ///* in this case, idxStart should be always 0 */
                //byte[] buffFirst = new byte[idxStart];
                //byte[] buffLast = new byte[packetLength - idxStart];

                //Array.Copy(buffRecv, 0, buffFirst, 0, buffFirst.Length);
                //Array.Copy(buffRecv, idxStart, buffLast, 0, buffLast.Length);

                //listStacked.AddRange(buffFirst);
                //buffJpeg = listStacked.ToArray();

                //listStacked.Clear();
                //listStacked.AddRange(buffLast);

                //if (buffJpeg.Length > 4)
                //{
                //    /* display the jpeg data in the picturebox */
                //    Invoke(new ViewImageDataDelegate(PB_setImage), new object[] { pictureBox1, buffJpeg, buffJpeg.Length });
                //}
            }
            else if (idxStart == -1 && idxEnd != -1)        // only idxEnd is detected
            {
                //Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "4" });

                //byte[] buffFirst = new byte[idxEnd + 1];
                //byte[] buffLast = new byte[packetLength - idxEnd - 1];

                //Array.Copy(buffRecv, 0, buffFirst, 0, buffFirst.Length);
                //Array.Copy(buffRecv, idxEnd + 1, buffLast, 0, buffLast.Length);

                //listStacked.AddRange(buffFirst);
                //buffJpeg = listStacked.ToArray();

                //listStacked.Clear();
                //listStacked.AddRange(buffLast);

                ///* display the jpeg data in the picturebox */
                //Invoke(new ViewImageDataDelegate(PB_setImage), new object[] { pictureBox1, buffJpeg, buffJpeg.Length });
            }
            else if (idxStart == -1 && idxEnd == -1)        // none of the idxs is detected
            {
                //Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "5" });

                //byte[] buff = new byte[packetLength];
                //Array.Copy(buffRecv, buff, buff.Length);

                //listStacked.AddRange(buff);
            }
        }

        //--------------------------------------------------------------------------------------------
        //-------------------------------------  Delegates  ------------------------------------------
        //--------------------------------------------------------------------------------------------
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
                Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, pb.Name + ": Failed Image Load." });
                return;
            }
        }

        ////********************************************************************************************
        ////********************************************************************************************
        ////*************************************Callback Method****************************************
        ////********************************************************************************************
        ////********************************************************************************************

        ////--------------------------------------------------------------------------------------------
        ////-------------------------------------TCP Callback-------------------------------------------
        ////--------------------------------------------------------------------------------------------
        //private void tcpAcceptTcpClientCallback(IAsyncResult ar)       //  To prevent cross thread error
        //{
        //    try
        //    {
        //        TcpListener listener = (TcpListener)ar.AsyncState;

        //        tcpServer = listener.EndAcceptTcpClient(ar);
        //        tcpNS = tcpServer.GetStream();

        //        String remoteIP = tcpServer.Client.RemoteEndPoint.ToString();
        //        Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Connect From " + remoteIP });

        //        bufferRx = new byte[tcpServer.ReceiveBufferSize];      // The default value is 8192 bytes.
        //        tcpNS.BeginRead(bufferRx, 0, bufferRx.Length, tcpBeginReadCallback, tcpNS);
        //    }
        //    catch (ObjectDisposedException)
        //    {
        //        Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "TCP Server Close." });
        //        return;
        //    }
        //}

        //private void tcpBeginReadCallback(IAsyncResult ar)          //  To prevent cross thread error
        //{
        //    try
        //    {
        //        NetworkStream ns = (NetworkStream)ar.AsyncState;
        //        int len = ns.EndRead(ar);

        //        if (len > 0)
        //        {
        //            if (bDisplayImage == false)
        //            {
        //                String message_rx = Encoding.UTF8.GetString(bufferRx, 0, len);
        //                Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Length=" + len.ToString() });
        //                Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Receive: " + message_rx });
        //            }
        //            else if (bDisplayImage == true)
        //            {
        //                //showPreviewImage(len, bufferRx, listBufferRemained);
        //            }

        //            bufferRx = new byte[tcpServer.ReceiveBufferSize];      // The default value is 8192 bytes.
        //            ns.BeginRead(bufferRx, 0, bufferRx.Length, tcpBeginReadCallback, ns);
        //        }
        //        else
        //        {
        //            Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Client left." });
        //            Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Click the close button." });
        //            //closeServer();
        //        }
        //    }
        //    catch (ObjectDisposedException)
        //    {
        //        Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "TCP Server has been Closed." });
        //        return;
        //    }
        //    catch (IOException)
        //    {
        //        Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "TCP Server has been Closed." });
        //        return;
        //    }
        //}

        //private void tcpBeginWriteCallback(IAsyncResult ar)
        //{
        //    NetworkStream ns = (NetworkStream)ar.AsyncState;
        //    ns.EndWrite(ar);

        //    Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Transmit: " + strMsgTx });
        //    textBox1.Text = "";
        //}

        ////--------------------------------------------------------------------------------------------
        ////-------------------------------------UDP Callback-------------------------------------------
        ////--------------------------------------------------------------------------------------------
        //private void udpBeginReceiveCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        UdpClient u = (UdpClient)ar.AsyncState;

        //        //IPEndPoint UdpSource = new IPEndPoint(0, 0);
        //        //byte[] UdpBuffer = u.EndReceive(ar, ref UdpSource);

        //        byte[] UdpBuffer = u.EndReceive(ar, ref udpClientEP);
        //        string UdpBufferStr = Encoding.UTF8.GetString(UdpBuffer);

        //        if (bDisplayImage == false)
        //        {
        //            // 접속된 클라이언트의 IP 주소와 포트 출력
        //            Invoke(new AddListBoxDelegate(listBox_Add),
        //                new object[] { listBox1, "Receive from " + udpClientEP.Address.ToString() 
        //            + ":" + udpClientEP.Port.ToString() });
        //            Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Receive: " + UdpBufferStr });
        //            Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Length: " + UdpBuffer.Length });
        //        }
        //        else if (bDisplayImage == true)
        //        {


        //            //showPreviewImage(UdpBuffer.Length, UdpBuffer, listBufferRemained);

        //            //sendMessage("UDPACK");
        //        }

        //        u.BeginReceive(udpBeginReceiveCallback, u);
        //    }
        //    catch (ObjectDisposedException)
        //    {
        //        Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "UDP Server Close." });
        //        return;
        //    }
        //    catch (Exception)
        //    {
        //        Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Exception" });
        //        return;
        //    }
        //}

        //private void udpBeginWriteCallback(IAsyncResult ar)
        //{
        //    UdpClient u = (UdpClient)ar.AsyncState;

        //    Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "number of bytes sent: " + u.EndSend(ar).ToString() });
        //    Invoke(new AddListBoxDelegate(listBox_Add), new object[] { listBox1, "Transmit: " + strMsgTx });
        //}
    }
}
