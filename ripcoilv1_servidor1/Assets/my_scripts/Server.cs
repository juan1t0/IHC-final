using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class Server : MonoBehaviour
{
    private List<ServerClient> Clients;
    private List<ServerClient> disconnectClients;
    public int port = 3223;
    private TcpListener server;
    private bool serverStarted;
    public GameObject rayi;
    public RayGun rayScrip;
    private void Start()
    {
//        Debug.Log("server star");
        Clients = new List<ServerClient>();
        disconnectClients = new List<ServerClient>();
        if (PlayerPrefs.GetInt("type") != 0)
        {
            Debug.Log("Socket error: No server");
//            return;
        }
        try
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            StartListening();
            serverStarted = true;
            Debug.Log("Server has been started on port " + port.ToString());
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e.Message);
        }
    }
    private void Update()
    {
       // Debug.LogWarning("entra update");
        if (!serverStarted)
            return;
        foreach (ServerClient cli in Clients)
        {
            if (!IsConnected(cli.tcp))
            {
                cli.tcp.Close();
                disconnectClients.Add(cli);
                continue;
            }
            else
            {
                NetworkStream s = cli.tcp.GetStream();
                if (s.DataAvailable)
                {
                    StreamReader reader = new StreamReader(s, true);
                    string data = reader.ReadLine();
                    
                    if (data != null)
                    {
                        if (cli.clientName == "shot")
                        {
                            string[] dataR = separeString(data);
                            
                            rayi = GameObject.Find("shot");
                            rayScrip = rayi.GetComponent<RayGun>();
                            //                        Debug.Log("Player id: " + PlayerPrefs.GetInt("type"));
                            rayScrip.destini = dataR[1];
                            rayScrip.origin = dataR[0];

                            rayScrip.shotin = true;
                            Debug.Log("shoting " + data);
                            sendDataS(data, "shot");
                            return;
                        }
                        cli.clientName = data;
                        Debug.Log(cli.clientName + " ete----------");
                       // OnInComingData(cli, data);
                    }
                }
            }
        }
    }

    public void sendData(string data)
    {
        Broadcast(data, Clients);
    }
    public void sendDataS(string data, string to ) {
//        Debug.Log("::" + data + ">>" + to);
        foreach (ServerClient cli in Clients)
        {
            try
            {
                if(cli.clientName == to)
                {
                  //  Debug.Log("para: " + to);
                    StreamWriter writer = new StreamWriter(cli.tcp.GetStream());
                    writer.WriteLine(data);
                    writer.Flush();
                }
            }
            catch (Exception e)
            {
                Debug.Log("write error: " + e.Message + ", to client " + cli.clientName);
            }
        }
    }

    private void OnInComingData(ServerClient cli, string data)
    {
        Debug.Log(cli.clientName + " has sent the following message: " + data);
        Broadcast(data, Clients);
    }

    private void StartListening()
    {
        server.BeginAcceptTcpClient(AcceptTcpClient, server);
    }
    private bool IsConnected(TcpClient cli)
    {
        try
        {
            if (cli != null && cli.Client != null && cli.Client.Connected)
            {
                if (cli.Client.Poll(0, SelectMode.SelectRead))
                {
                    return !(cli.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                }
                return true;
            }
            else
                return false;
        }
        catch
        {
            return false;
        }
    }
    private void AcceptTcpClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;
        Clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar)));
        StartListening();
        //send a message to everyone,say someone has conected
        Debug.Log("Client Connected");
    }
    private string[] separeString(string concat)
    {
        string[] Arrey = concat.Split('%');
        return Arrey;
    }

    private void Broadcast(string data, List<ServerClient> clis)
    {
        foreach (ServerClient cli in clis)
        {
            try
            {
                StreamWriter writer = new StreamWriter(cli.tcp.GetStream());
                writer.WriteLine(data);
                writer.Flush();
            }
            catch (Exception e)
            {
                Debug.Log("write error: " + e.Message + ", to client " + cli.clientName);
            }
        }
    }
}
public class ServerClient
{
    public TcpClient tcp;
    public string clientName;
//    public string theTrueName;

    public ServerClient(TcpClient clientSocket)
    {
  //      clientName = "Guest blabla";
        tcp = clientSocket;
    }
}


//mirrar desde el 30 por si hay caso