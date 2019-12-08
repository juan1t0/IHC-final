using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cualquiera : MonoBehaviour
{
    public Camera serverCamera;
    public Camera player1Camera;
    public Camera player2Camera;

    private void Start()
    {
        int typ = PlayerPrefs.GetInt("type");
        if(typ == 0)
        {
            serverCamera.enabled = true;
            serverCamera.tag = "MainCamera";
            player1Camera.enabled = false;
            player2Camera.enabled = false;
        }
        else if (typ == 1){
            player1Camera.enabled = true;
            player1Camera.tag = "MainCamera";
            serverCamera.enabled = false;
            player2Camera.enabled = false;
        }
        else if(typ == 2)
        {
            player2Camera.enabled = true;
            player2Camera.tag = "MainCamera";
            player1Camera.enabled = false;
            serverCamera.enabled = false;
        }
    }
}
