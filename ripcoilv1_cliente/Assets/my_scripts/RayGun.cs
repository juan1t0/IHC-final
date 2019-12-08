using System.Collections;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    public float shootRate;
    private float m_shootRateTimeStamp;

    public GameObject m_shotPrefab;

    RaycastHit hit;
    float range = 10000.0f;

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

    private string[] separeStrings(string concat) {
        string[] Array = concat.Split('%');
        return Array;
    }

    void Start()
    {
        client.ConnectedToServer();
        client.Send("shot");
    }

    void Update()
    {
        
        if (Input.GetMouseButton(0) && PlayerPrefs.GetInt("type") == 2)
        {
            if (Time.time > m_shootRateTimeStamp)
            {
                Vector3 position = Camera.main.transform.position;
                Vector3 direction = Camera.main.transform.TransformDirection(Vector3.forward);
                client.Send(position.ToString() + "%" + direction.ToString());
                shootRay(position, direction);
                m_shootRateTimeStamp = Time.time + shootRate;
            }
        }
        else if(PlayerPrefs.GetInt("type") == 1)
        {
            if (client.stream.DataAvailable)
            {

                string data = client.reader.ReadLine();
                Debug.Log("rec: " + data);
                if (data != null)
                {
                    string[] temp = separeStrings(data);
                    Vector3 position = StringToVector(temp[0]);
                    Vector3 direction = StringToVector(temp[1]);
                    shootRay(position, direction);
                    m_shootRateTimeStamp = Time.time + shootRate;
                }

            }
        }

    }

    void shootRay(Vector3 position, Vector3 direction)
    {

        Ray ray = new Ray(position, direction);

        if (Physics.Raycast(ray, out hit, range))
        {
            GameObject laser = GameObject.Instantiate(m_shotPrefab, transform.position, transform.rotation) as GameObject;
            laser.GetComponent<ShotBehavior>().setTarget(hit.point);
            GameObject.Destroy(laser, 2f);


        }

    }



}
