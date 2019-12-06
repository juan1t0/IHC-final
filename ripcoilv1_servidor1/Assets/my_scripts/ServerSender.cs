using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ServerSender : MonoBehaviour
{
    public GameObject server;
    private Server serverScript;

    //public Transform spin_p1;
    //public Transform neck_p1;
    //public Transform shoulder_p1;
    //public Transform elbow_p1;
    //public Transform hand_p1;
    public Transform spin_p2;
    public Transform neck_p2;
    public Transform shoulder_p2;
    public Transform elbow_p2;
    public Transform hand_p2;

    // Start is called before the first frame update
    void Start()
    {
        server = GameObject.Find("Server");
        serverScript = server.GetComponent<Server>();
        Debug.Log("Player id: " + PlayerPrefs.GetInt("type"));
    }

    // Update is called once per frame
    void Update()
    {
        //serverScript.sendData(PosToString(spin_p1.position));
        //serverScript.sendData(PosToString(neck_p1.position));
        //serverScript.sendData(PosToString(shoulder_p1.position));
        //serverScript.sendData(PosToString(elbow_p1.position));
        //serverScript.sendData(PosToString(hand_p1.position));
        //Debug.Log("hand1: " + PosToString(hand_p1.position));
        serverScript.sendDataS(PosToString(spin_p2.position),"spin2");
        serverScript.sendDataS(PosToString(neck_p2.position), "neck2");
        serverScript.sendDataS(PosToString(shoulder_p2.position), "shoulder2");
        serverScript.sendDataS(PosToString(elbow_p2.position), "elbow2");
        serverScript.sendDataS(PosToString(hand_p2.position), "hand2");
//        Debug.Log("hand2: " + PosToString(hand_p2.position));
    }
    private String PosToString(Vector3 pos)
    {
        String result = pos.ToString();
        return result;
    }
}
