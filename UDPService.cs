using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

enum csUdpConnStatus
{
    Closed,
    Opened
};

// 데이터 수신 이벤트핸들링 함수 delegate 원형
public delegate void UdpDataArrivalHandler();

class UDPService
{
    private int buffersize = 65536;

    // Socket Objects
    private UdpClient clientForServer = null;
    private IPEndPoint broadcastEP = null; 

    // Current connection status
    private csUdpConnStatus serverStatus = csUdpConnStatus.Closed;

    // Threads for socket
    private Thread threadRcv = null;
    private Thread threadChkDeath = null;

    // Server 수신 데이터
    private string RcvMessage = "";
    private List<byte> RcvByteList = new List<byte>();

    // 수신이벤트를 위한 델리게이트
    private UdpDataArrivalHandler DataArrivalCallback;

    //===============================================================
    //  Constructor 1 : set set Receiving event handler to null
    //  Constructor 2 : set Receiving event handler to the user defined one.
    //  Constructor 3 : set Receiving event handler and Receivig buffer size
    //===============================================================
    public UDPService()
    {
        DataArrivalCallback = null;
    }

    public UDPService(UdpDataArrivalHandler callback)
    {
        DataArrivalCallback = new UdpDataArrivalHandler(callback);
    }

    public UDPService(UdpDataArrivalHandler callback, int iRxBufferSize)
    {
        buffersize = iRxBufferSize;
        DataArrivalCallback = new UdpDataArrivalHandler(callback);
    }

    //===============================================================
    //  Server : Start
    //===============================================================
    public void openUdpServer(int serverPort)
    {
        if (serverStatus == csUdpConnStatus.Opened) return;

        // 1단계 : Start
        try
        {
            // server 객체 얻기
            clientForServer = new UdpClient(serverPort);
            // server측 접속 IP 객체 얻기
            broadcastEP = new IPEndPoint(IPAddress.Any, serverPort);
        }
        catch (SocketException e)
        {
            Console.WriteLine(e);
            return;
        }

        serverStatus = csUdpConnStatus.Opened;
    }

    public void PrepareClient(int localPort)
    {
        if (serverStatus == csUdpConnStatus.Opened) return;

        try
        {
            // server 객체 얻기
            clientForServer = new UdpClient(localPort);
            // set End Point
            broadcastEP = new IPEndPoint(IPAddress.Any, localPort);

            threadRcv = new Thread(ReceiveThreadMain);
            threadRcv.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            serverStatus = csUdpConnStatus.Closed;
        }

        serverStatus = csUdpConnStatus.Opened;
    }

    //===============================================================
    //  Server : Close
    //===============================================================
    public void ServerClose()
    {
        if (clientForServer != null) clientForServer.Close();

        // 순서 바뀌면 안됨
        if (threadRcv != null && threadRcv.IsAlive)
        {
            threadRcv.Abort();
            threadRcv.Join();
        }

        if (threadChkDeath != null && threadChkDeath.IsAlive)
        {
            threadChkDeath.Abort();
            threadChkDeath.Join();
        }

        serverStatus = csUdpConnStatus.Closed;
    }

    //===============================================================
    //  Server Status
    //===============================================================
    public csUdpConnStatus ServerStatus()
    {
        return serverStatus;
    }

    public byte[] GetRcvBytes()
    {
        lock (RcvByteList)
        {
            byte[] tmp = RcvByteList.ToArray();
            RcvByteList.Clear();
            return tmp;
        }
    }

    //===============================================================
    //  Receive Thread Main
    //===============================================================
    private void ReceiveThreadMain()
    {
        try
        {
            byte[] bytebuff;
            while (true)
            {
                Thread.Sleep(1);

                bytebuff = clientForServer.Receive(ref broadcastEP);

                lock (RcvByteList)
                {
                    RcvByteList.AddRange(bytebuff);
                }

                // 데이터 수신 callback 함수 호출
                if (DataArrivalCallback != null) { DataArrivalCallback(); }
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.ToString());
        }
        finally
        {
            clientForServer.Close();
            serverStatus = csUdpConnStatus.Closed;
        }
    }
}