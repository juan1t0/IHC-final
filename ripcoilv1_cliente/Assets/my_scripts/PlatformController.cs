using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float paddleSpeed = 10f;
    private float xpose = 0.0f;
    public Vector3 playerPos = new Vector3(0, -22f, -400);

    private Client client = new Client();

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

    private void Start()
    {
        client.ConnectedToServer();
    }

    void Update()
    {

        if (client.stream.DataAvailable)
        {
            string data = client.reader.ReadLine();
            if (data != null)
            {
                playerPos = StringToVector(data);
            }

        }

        float xPos = transform.position.x;
        //float xPosaux = 0.0f;
        if (KinectM.instance.IsAvailable)
        {
            xPos = KinectM.instance.PaddlePosition;

        }
        else
        {
            xPos = transform.position.x + (Input.GetAxis("Horizontal") * paddleSpeed);
            
        }
        if (xPos > 2)
        {
            xpose += 2.5f;
        }
        else if (xPos < -2)
        {
            xpose -= 2.5f;
        }
        //Debug.Log("xpos: " + xPos);
        // playerPos = new Vector3(Mathf.Clamp(xPos, -200f, 200f), -22f, -400f);
        playerPos = new Vector3(Mathf.Clamp(xpose, -200f, 200f), -22f, -400f);
      //  Debug.Log("playerPos: " + playerPos);
        transform.position = playerPos;
    }
}
