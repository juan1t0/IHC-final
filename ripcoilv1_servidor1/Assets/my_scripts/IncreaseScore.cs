using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseScore : MonoBehaviour
{
    public bool Mike = false;
    public bool Target = false;
    public GameObject the_text;
    private CurrentScore scoree;
    // Start is called before the first frame update
    void Start()
    {
        scoree = the_text.GetComponent<CurrentScore>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {

        string col_name = collision.gameObject.name;
        Debug.Log("entre " + col_name + " y ");
        if (col_name == "shot_prefab(Clone)" && Mike )
        {
            if (Target) { return; }
            Debug.Log("colision mike " + scoree.actual);
            scoree.SetCurrentScore(scoree.actual + 1);
            scoree.UpdateText();
        }
        else if (col_name == "mike" && Target)
        {
            if (Mike)
            {
                Debug.Log("colision tar " + scoree.actual);
                scoree.SetCurrentScore(scoree.actual + 1);
            }
            Debug.Log("colision tartar " + scoree.actual);
            scoree.SetCurrentScore(scoree.actual + 2);
            scoree.UpdateText();
        }
        
    }
}
