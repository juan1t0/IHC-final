using UnityEngine;
using System.Collections;
using System;
using System.Net.Sockets;
using System.IO;

public class MikesManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Client client_mike = new Client();
    private Client client_spin_p1 = new Client();
    private Client client_neck_p1 = new Client();
    private Client client_shoulder_p1 = new Client();
    private Client client_elbow_p1 = new Client();
    private Client client_hand_p1 = new Client();
    private Client client_spin_p2 = new Client();
    private Client client_neck_p2 = new Client();
    private Client client_shoulder_p2 = new Client();
    private Client client_elbow_p2 = new Client();
    private Client client_hand_p2 = new Client();

    public Transform MikeObject;
    public Transform spin_p1;
    public Transform neck_p1;
    public Transform shoulder_p1;
    public Transform elbow_p1;
    public Transform hand_p1;
    public Transform spin_p2;
    public Transform neck_p2;
    public Transform shoulder_p2;
    public Transform elbow_p2;
    public Transform hand_p2;
    void Start()
    {
        /*client_mike.ConnectedToServer();
        client_mike.Send("mike");

        client_spin_p1.ConnectedToServer();
        client_spin_p1.Send("spin1");

        client_neck_p1.ConnectedToServer();
        client_neck_p1.Send("neck1");

        client_shoulder_p1.ConnectedToServer();
        client_shoulder_p1.Send("shoulder1");

        client_elbow_p1.ConnectedToServer();
        client_elbow_p1.Send("elbow1");

        client_hand_p1.ConnectedToServer();
        client_hand_p1.Send("hand1");

        client_spin_p2.ConnectedToServer();
        client_spin_p2.Send("spin2");

        client_neck_p2.ConnectedToServer();
        client_neck_p2.Send("neck2");

        client_shoulder_p2.ConnectedToServer();
        client_shoulder_p2.Send("shoulder2");

        client_elbow_p2.ConnectedToServer();
        client_elbow_p2.Send("elbow2");

        client_hand_p2.ConnectedToServer();
        client_hand_p2.Send("hand2");*/
    }

    // Update is called once per frame
    void Update()
    {
        string data;
        if (client_mike.stream.DataAvailable)
        {
            data = client_mike.reader.ReadLine();
            if (data != null)
            {
                MikeObject.position = StringToVector(data);
            }

        }
        if (client_spin_p1.stream.DataAvailable)
        {
            data = client_spin_p1.reader.ReadLine();
            if (data != null)
            {
                spin_p1.position = StringToVector(data);
            }

        }
        if (client_spin_p2.stream.DataAvailable)
        {
            data = client_spin_p2.reader.ReadLine();
            if (data != null)
            {
                spin_p2.position = StringToVector(data);
            }

        }
        if (client_neck_p1.stream.DataAvailable)
        {
            data = client_neck_p1.reader.ReadLine();
            if (data != null)
            {
                  neck_p1.position = StringToVector(data);
            }

        }
        if (client_neck_p2.stream.DataAvailable)
        {
            data = client_neck_p2.reader.ReadLine();
            if (data != null)
            {
                neck_p2.position = StringToVector(data);
            }

        }
        if (client_shoulder_p1.stream.DataAvailable)
        {
            data = client_shoulder_p1.reader.ReadLine();
            if (data != null)
            {
                shoulder_p1.position = StringToVector(data);
            }

        }
        if (client_shoulder_p2.stream.DataAvailable)
        {
            data = client_shoulder_p2.reader.ReadLine();
            if (data != null)
            {
                shoulder_p2.position = StringToVector(data);
            }

        }
        if (client_elbow_p1.stream.DataAvailable)
        {
            data = client_elbow_p1.reader.ReadLine();
            if (data != null)
            {
                elbow_p1.position = StringToVector(data);
            }

        }
        if (client_elbow_p2.stream.DataAvailable)
        {
            data = client_elbow_p2.reader.ReadLine();
            if (data != null)
            {
                elbow_p2.position = StringToVector(data);
            }
        }
        if (client_hand_p1.stream.DataAvailable)
        {
            data = client_hand_p1.reader.ReadLine();
            if (data != null)
            {
                hand_p1.position = StringToVector(data);
            }
        }
        if (client_hand_p2.stream.DataAvailable)
        {
            data = client_hand_p2.reader.ReadLine();
            if (data != null)
            {
                hand_p2.position = StringToVector(data);
            }

        }
    }
    private Vector3 StringToVector(string pos)
    {
        Debug.Log(pos);
        if (pos.StartsWith("(") && pos.EndsWith(")"))
        {
            pos = pos.Substring(1, pos.Length - 2);
        }
        string[] posArray = pos.Split(',');

        Vector3 result = new Vector3(
            float.Parse(posArray[0]),
            float.Parse(posArray[1]),
            float.Parse(posArray[2])
        );

        return result;
    }
}
