using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    // Start is called before the first frame update
    void OntriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            coll.GetComponent<PlayerController>().isClimbing = true;
        }
    }
    void OntriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            coll.GetComponent<PlayerController>().isClimbing = false;
        }
    }
}
