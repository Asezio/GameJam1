using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public GameObject rewardEffect;
    Transform effectPos;

    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="Player")
        {
            Debug.Log("item");
            //GameObject currentObj = Instantiate(rewardEffect, effectPos.position, Quaternion.identity);
            //Destroy(currentObj, 0.3f);
            Destroy(gameObject);
        }
    }

}
