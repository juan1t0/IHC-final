using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using System;

public class Client : MonoBehaviour
{
    public GameObject chatContainer;
    public GameObject messagePrefab;
    public string clientName;

    private bool socketReady = false;
    private TcpClient socket;
    public NetworkStream stream;
    public StreamWriter writer;
    public StreamReader reader;

    public void ConnectedToServer()
    {
        if (socketReady)
            return;
        string host = "192.168.43.182";
        int port = 3223;

        try
        {
            socket = new TcpClient(host,port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            socketReady = true;
        }
        catch(Exception e)
        {
            Debug.Log("Socket error: "+e.Message);
        }
    }

    /*private void Update()
    {
        if (socketReady)
        {
            if (stream.DataAvailable)
            {
                string data = reader.ReadLine();
                if (data != null)
                    OnIncomingData(data);
                
            }
        }
    }*/

    private void OnIncomingData(string data)
    {
        GameObject go =  Instantiate(messagePrefab, chatContainer.transform) as GameObject;
        go.GetComponentInChildren<Text>().text = data;
    }

    public void Send(string data)
    {
        if (!socketReady)
            return;
        writer.WriteLine(data);
        writer.Flush();
    }

    public void OnSendButton()
    {
        string message = GameObject.Find("SendInput").GetComponent<InputField>().text;
        Send(message);

    }
}

//mirrar desde el 30 por si hay caso