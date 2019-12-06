using System.Collections;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    public float shootRate;
    private float m_shootRateTimeStamp;

    public GameObject m_shotPrefab;

    RaycastHit hit;
    float range = 10000.0f;

    public GameObject server;
    private Server serverScript;

    public Transform origen;
    public string destini;
    public string origin;
    public bool exist = false;
    
    public bool shotin = false;
    private void Start()
    {
        server = GameObject.Find("Server");
        serverScript = server.GetComponent<Server>();
        Debug.Log("Player id: " + PlayerPrefs.GetInt("type"));
    }
    void Update()
    {
        
        if (Input.GetMouseButton(0))//(shotin)//Input.GetMouseButton(0))//touchCount > 0)
        {
            if (Time.time > m_shootRateTimeStamp)
            {
                shootRay();
                m_shootRateTimeStamp = Time.time + shootRate;
            }
            shotin = false;
//            Debug.Log("222222222222222");
        }

    }

    void shootRay()
    {
        Vector3 p = origen.position;//Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Ray ray = new Ray(StringToVector(origin), StringToVector(destini));
        if (Physics.Raycast(ray, out hit, range))
        {
            GameObject laser = GameObject.Instantiate(m_shotPrefab, transform.position, transform.rotation) as GameObject;
            laser.GetComponent<ShotBehavior>().setTarget(hit.point);
            GameObject.Destroy(laser, 2f);
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