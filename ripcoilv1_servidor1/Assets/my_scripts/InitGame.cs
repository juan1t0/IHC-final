using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGame : MonoBehaviour
{
    public GameObject localPlayer;
    public GameObject onlinePlayer;
    // Start is called before the first frame update
    void Start()
    {
        int typ = PlayerPrefs.GetInt("type");
        if(typ == 1)
        {
            Instantiate(localPlayer, new Vector3(0, -22, -300), Quaternion.identity);
            Instantiate(onlinePlayer, new Vector3(0, -22, 300), Quaternion.identity);
        }
        else if (typ == 2)
        {
            Instantiate(onlinePlayer, new Vector3(0, -22, -300), Quaternion.identity);
            Instantiate(localPlayer, new Vector3(0, -22, 300), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}   