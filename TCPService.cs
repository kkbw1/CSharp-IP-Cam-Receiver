using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

enum csConnStatus
{
    Closed,
    Listening,
    Connecting,
    Connected
};

static class TSocket
{
    public static char sSTX() { return Convert.ToChar(0x02); }
    public static char sETX() { return Convert.ToChar(0x03); }
    public static char sEOT() { return Convert.ToChar(0x04); }
    public static char sENQ() { return Convert.ToChar(0x05); }
    public static char sACK() { return Convert.ToChar(0x06); }
    public static char sNAK() { return Convert.ToChar(0x15); }
    public static char sCR() { return Convert.ToChar(13); }
    public static char sLF() { return Convert.ToChar(10); }
    public static string sCRLF() { return "\r\n"; }

    public static string HostName()
    {
        return Dns.GetHostName();
    }

    public static IPAddress[] HostAddresses()
    {
        string hostname = HostName();
        IPAddress[] ips = Dns.GetHostAddresses(hostname);
        return ips;
    }
}

//======================================================================================================
// Server Class
//======================================================================================================
// Data receive event handling function delegate
public delegate void ServerDataArrivalHandler();

class TServer
{
    private int buffersize = 65536;

    // Socket object for server
    private TcpListener listener = null;        // only for listening
    private TcpClient clientForServer = null;
    private NetworkStream streamServer = null;

    // current status object
    private csConnStatus serverStatus = csConnStatus.Closed;

    // receive thread for server
    private Thread threadServerRcv = null;
    private Thread threadChkPartnerDeath = null;

    // received data for server
    private string serverRcvMessage = "";
    private List<byte> serverRcvByteList = new List<byte>();

    // delegate for recieve event
    private ServerDataArrivalHandler DataArrivalCallback;

    //===============================================================
    //  Constructor 1 : set set Receiving event handler to null
    //  Constructor 2 : set Receiving event handler to the user defined one.
    //  Constructor 3 : set Receiving event handler and Receivig buffer size
    //===============================================================
    public TServer()
    {
        DataArrivalCallback = null;
    }

    public TServer(ServerDataArrivalHandler callback)
    {
        DataArrivalCallback = new ServerDataArrivalHandler(callback);
    }

    public TServer(ServerDataArrivalHandler callback, int iRxBufferSize)
    {
        buffersize = iRxBufferSize;
        DataArrivalCallback = new ServerDataArrivalHandler(callback);
    }

    //===============================================================
    //  Server : Start
    //===============================================================
    public void ServerStartListen(string serverIP, int serverPort)
    {
        if (serverStatus == csConnStatus.Listening) return;
        if (serverStatus == csConnStatus.Connected) return;

        // Step 1 : Start
        try
        {
            // Getting IP object for server
            IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);

            // Getting server object
            listener = new TcpListener(serverAddress);

            // Start
            listener.Start();
        }
        catch (SocketException e)
        {
            Console.WriteLine(e);
            return;
        }

        // Step 2 : start listening
        bool success = ServerBeginListen();

        serverStatus = csConnStatus.Listening;
    }

    //===============================================================
    //  Server : Start listening [Async mode]
    //===============================================================
    private bool ServerBeginListen()
    {
        // Prevents accepting different connection with same port while listening
        try
        {
            listener.BeginAcceptTcpClient(new AsyncCallback(DoAcceptTcpClientCallback), listener);
        }
        catch { return false; }

        return true;
    }

    //---------------------------------------------------------------
    //  Listen CallBack method
    //---------------------------------------------------------------
    private void DoAcceptTcpClientCallback(IAsyncResult ar)
    {
        // exception process in case socket close happens while listening
        try 
        {
            // Get the listener that handles the client request.
            listener = (TcpListener)ar.AsyncState;

            // End the operation
            clientForServer = listener.EndAcceptTcpClient(ar);

            // Get stream object
            streamServer = clientForServer.GetStream();

            // Start receive thread
            threadServerRcv = new Thread(ServerReceiveThreadMain);
            threadServerRcv.IsBackground = true;
            threadServerRcv.Start();

            // Start thread that checks unexpected connection death with partner
            threadChkPartnerDeath = new Thread(ServerCheckPartnerDeathThread);
            threadChkPartnerDeath.IsBackground = true;
            threadChkPartnerDeath.Start();

            serverStatus = csConnStatus.Connected;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            serverStatus = csConnStatus.Closed;
        }
        finally
        {
            // stop listener after connection established or cancelled
            listener.Stop();
            listener = null;
        }
    }

    //===============================================================
    //  Server : Close
    //===============================================================
    public void ServerClose()
    {
        if (clientForServer != null) clientForServer.Close();
        if (listener != null) listener.Stop();

        while (true)
        {   
            // if server close happen when listening,
            // waiting until DoAcceptTcpClientCallback() finishes the listener object.
            if (listener == null) break;
        }

        // Close receive thread and connect death check thread
        // ** sequence of stopping threads has to be fixed. 
        if (threadServerRcv != null && threadServerRcv.IsAlive)
        {
            threadServerRcv.Abort();
            threadServerRcv.Join();
        }

        if (threadChkPartnerDeath != null && threadChkPartnerDeath.IsAlive)
        {
            threadChkPartnerDeath.Abort();
            threadChkPartnerDeath.Join();
        }

        serverStatus = csConnStatus.Closed;
    }

    //===============================================================
    //  Server Status
    //===============================================================
    public csConnStatus ServerStatus()
    {
        return serverStatus;
    }

    //===============================================================
    //  Server Send
    //===============================================================
    public void ServerSend(string st)
    {
        try
        {
            if (streamServer == null || !streamServer.CanWrite) return;
            byte[] msg = Encoding.UTF8.GetBytes(st);
            streamServer.Write(msg, 0, msg.Length);
        }
        catch { }
    }

    //===============================================================
    //  Push out receive data to user application
    //===============================================================
    public byte[] GetRcvBytes()
    {
        lock (serverRcvByteList)    //<- synchronize between threads
        {
            byte[] tmp = serverRcvByteList.ToArray();
            serverRcvByteList.Clear();
            return tmp;
        }
    }

    //===============================================================
    //  Server Receive Thread Main
    //===============================================================
    private void ServerReceiveThreadMain()
    {
        try
        {
            byte[] bytebuff = new byte[buffersize];
            while (true)
            {
                Thread.Sleep(1);

                if (streamServer == null) continue;
                if (!streamServer.CanRead) continue;

                List<byte> ListByteBuff = new List<byte>();
                byte[] byteTemp;

                //StringBuilder strbuff = new StringBuilder();

                int nbyteRead = 0;
                // loop for when arrived data is larger than receive buffer size
                do
                {
                    nbyteRead = streamServer.Read(bytebuff, 0, bytebuff.Length);    // blocking
                    if (nbyteRead == 0)
                    { 
                        // Confirm partner close connection
                        serverStatus = csConnStatus.Closed;
                        ServerClose();              //<- has to call the method.
                    }

                    byteTemp = new byte[nbyteRead];
                    Array.Copy(bytebuff, 0, byteTemp, 0, nbyteRead);
                    ListByteBuff.AddRange(byteTemp);

                    //strbuff.AppendFormat("{0}", Encoding.UTF8.GetString(bytebuff, 0, nbyteRead));
                }
                while (streamServer.DataAvailable);

                // Copy to recive buffer to the received data
                lock (serverRcvByteList)
                {
                    serverRcvByteList.AddRange(ListByteBuff);
                }

                // Call Data receive callback
                if (DataArrivalCallback != null) { DataArrivalCallback(); }
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.ToString());
        }
    }

    //===============================================================
    //  Server Check Partner Death Thread
    //===============================================================
    private void ServerCheckPartnerDeathThread()
    {
        try
        {
            while (true)
            {
                Thread.Sleep(5);

                // check continuously if connection is lost
                if (serverStatus == csConnStatus.Connected)
                {
                    if (clientForServer != null && clientForServer.Client != null)
                    {
                        if (clientForServer.Client.Connected == false)
                            serverStatus = csConnStatus.Closed;
                    }
                }
                if (serverStatus != csConnStatus.Connected) break;
            }
        }
        catch { }
    }
}

//======================================================================================================
// Client Class
//======================================================================================================
// Data receive event handling function delegate
public delegate void ClientDataArrivalHandler();

class TClient
{
    private int buffersize = 65536;

    // Socket object for client
    private TcpClient clientForClient = null;
    private NetworkStream streamClient = null;

    // current status object
    private csConnStatus clientStatus = csConnStatus.Closed;

    // receive thread for client
    private Thread threadClientRcv = null;
    private Thread threadChkPartnerDeath = null;

    // Receive data for client
    private string clientRcvMessage = "";
    private List<byte> clientRcvByteList = new List<byte>();

    // Delegate for receive event
    private ClientDataArrivalHandler DataArrivalCallback;
    //===============================================================
    //  Constructor 1 : set set Receiving event handler to null.
    //  Constructor 2 : set Receiving event handler to the user defined one.
    //  Constructor 3 : set Receiving event handler and Receivig buffer size.
    //===============================================================
    public TClient()
    {
        DataArrivalCallback = null;
    }
    public TClient(ClientDataArrivalHandler callback)
    {
        DataArrivalCallback = new ClientDataArrivalHandler(callback);
    }

    public TClient(ClientDataArrivalHandler callback, int iRxBufferSize)
    {
        buffersize = iRxBufferSize;
        DataArrivalCallback = new ClientDataArrivalHandler(callback);
    }

    //===============================================================
    //  Client : Try Connect [Async mode]
    //===============================================================
    public void ClientBeginConnect(string serverIP, int serverPort, string clientIP)
    {
        if (clientStatus == csConnStatus.Connected) return;
        if (clientStatus == csConnStatus.Connecting) return;
        if (serverIP == "") return;

        // Get server IP object
        IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);

        // Get client IP object
        // port = 0 means getting random port
        int dumport = 0;
        IPEndPoint clientAddress = new IPEndPoint(IPAddress.Parse(clientIP), dumport);

        // Get client object
        clientForClient = new TcpClient(clientAddress);

        // Start trying to connect
        IPAddress serverIpAddress = IPAddress.Parse(serverIP);
        clientForClient.BeginConnect(serverIpAddress, serverPort, new AsyncCallback(DoConnectTcpClientCallback), clientForClient);

        clientStatus = csConnStatus.Connecting;
    }

    //---------------------------------------------------------------
    //  Connect Callback method
    //---------------------------------------------------------------
    private void DoConnectTcpClientCallback(IAsyncResult ar)
    {
        try
        {
            clientForClient = (TcpClient)ar.AsyncState;
            clientForClient.EndConnect(ar);
            streamClient = clientForClient.GetStream(); // Get stream object

            // Start receive thread 
            threadClientRcv = new Thread(ClientReceiveThreadMain);
            threadClientRcv.IsBackground = true;
            threadClientRcv.Start();

            // Start thread that checks partner connect death
            threadChkPartnerDeath = new Thread(ClientCheckPartnerDeathThread);
            threadChkPartnerDeath.IsBackground = true;
            threadChkPartnerDeath.Start();

            clientStatus = csConnStatus.Connected;
        }
        catch
        {
            clientStatus = csConnStatus.Closed;
        }
    }

    //===============================================================
    //  Client : Close
    //===============================================================
    public void ClientClose()
    {
        if (clientForClient != null) clientForClient.Close();
        clientForClient = null;

        if (threadClientRcv != null && threadClientRcv.IsAlive)
        {
            threadClientRcv.Abort();
            threadClientRcv.Join();
        }

        if (threadChkPartnerDeath != null && threadChkPartnerDeath.IsAlive)
        {
            threadChkPartnerDeath.Abort();
            threadChkPartnerDeath.Join();
        }

        clientStatus = csConnStatus.Closed;
    }

    //===============================================================
    //  Client Status
    //===============================================================
    public csConnStatus ClientStatus()
    {
        return clientStatus;
    }

    //===============================================================
    //  Client Send
    //===============================================================
    public void ClientSend(string st)
    {
        try
        {
            if (streamClient == null || !streamClient.CanWrite) return;
            byte[] msg = Encoding.UTF8.GetBytes(st);
            streamClient.Write(msg, 0, msg.Length);
        }
        catch { }
    }

    //===============================================================
    //  push out received data to user application
    //===============================================================
    public string GetRcvMsg()
    {
        lock (clientRcvMessage)         //<- snychorize between threads
        {
            string tmp = clientRcvMessage;
            clientRcvMessage = "";
            return tmp;
        }
    }

    public byte[] GetRcvBytes()
    {
        lock (clientRcvByteList)        //<- snychorize between threads
        {
            byte[] tmp = clientRcvByteList.ToArray();
            clientRcvByteList.Clear();
            return tmp;
        }
    }
    //===============================================================
    //  Client Receive Thread Main
    //===============================================================
    private void ClientReceiveThreadMain()
    {
        try
        {
            byte[] bytebuff = new byte[buffersize];
            while (true)
            {
                Thread.Sleep(1);

                if (streamClient == null) continue;
                if (!streamClient.CanRead) continue;

                byte[] byteTemp;
                List<byte> ListByteBuff = new List<byte>();

                //StringBuilder strbuff = new StringBuilder();
                
                int nbyteRead = 0;
                // Loop for when arrived data is larger than receive buffer size
                do
                {
                    nbyteRead = streamClient.Read(bytebuff, 0, bytebuff.Length);    // blocking
                    if (nbyteRead == 0)
                    {   
                        // Detect partner close the connection
                        clientStatus = csConnStatus.Closed;
                        ClientClose();
                    }

                    byteTemp = new byte[nbyteRead];
                    Array.Copy(bytebuff, 0, byteTemp, 0, nbyteRead);
                    ListByteBuff.AddRange(byteTemp);
                }
                while (streamClient.DataAvailable);

                // Copy to receive buffer
                lock (clientRcvByteList)
                {
                    clientRcvByteList.AddRange(ListByteBuff);
                }

                // Call data receive callback method
                if (DataArrivalCallback != null) { DataArrivalCallback(); }
            }
        }
        catch
        { }
    }

    //===============================================================
    //  Client Check Partner Death Thread
    //===============================================================
    private void ClientCheckPartnerDeathThread()
    {
        try
        {
            while (true)
            {
                Thread.Sleep(5);

                // checks routinely if the connect is lost
                if (clientStatus == csConnStatus.Connected)
                {
                    if (clientForClient != null && clientForClient.Client != null)
                    {
                        if (clientForClient.Client.Connected == false)
                            clientStatus = csConnStatus.Closed;
                    }
                }
                if (clientStatus != csConnStatus.Connected) break;
            }
        }
        catch { }
    }
}