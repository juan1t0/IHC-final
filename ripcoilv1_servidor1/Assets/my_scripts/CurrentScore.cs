using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentScore : MonoBehaviour
{
    public int actual = 0;
    public int score = 0;
    public TextMesh thetext;
    public GameObject server;
    private Server serverScript;
    public int n_score = 0;

    // Start is called before the first frame update
    void Start()
    {
        server = GameObject.Find("Server");
        serverScript = server.GetComponent<Server>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCurrentScore(int x)
    {
        actual = x;
    }
    public void RefreshCurrentScore()
    {
        actual = 0;
    }
    public void UpdateText()
    {
        string s = (score + actual).ToString();
        if(score + actual < 10)
        {
            s.Insert(0, "0");
        }
        thetext.text = (score + actual).ToString();
        Debug.LogWarning("se actualizo a: " + thetext.text);
        if (n_score == 1)
        {
            serverScript.sendDataS(thetext.text, "score1");
        }
        else if(n_score == 2)
        {
            serverScript.sendDataS(thetext.text, "score2");
        }
    }
}
