using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentScore : MonoBehaviour
{
    public int actual = 1;
    public int score = 0;
    public TextMesh thetext;
    public int p;
    private Client client = new Client();

    // Start is called before the first frame update
    void Start()
    {
        client.ConnectedToServer();
        Debug.Log("Printing P -----> " + p);
        if(p == 1)
        {
            client.Send("score1");
        }
        else if(p == 2)
        {
            client.Send("score2");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(client.stream.DataAvailable)
        {

            string data = client.reader.ReadLine();
            Debug.Log("rec: " + data);
            if (data != null)
            {
                thetext.text = data;
            }

        }
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
        thetext.text = "Puntaje: " + (score + actual).ToString();
    }
}
