using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private BoxCollider2D attackPoint;
    private PlayerController player;
    private Rigidbody2D rd;
    // Start is called before the first frame update
    void Awake()
    {
        attackPoint = GetComponent<BoxCollider2D>();
        player = GetComponentInParent<PlayerController>();
        rd = GetComponentInParent<Rigidbody2D>();
        attackPoint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.GetComponent<ItemController>() != null && rd.velocity.y < 0f)
        {
            player.JumpLow();
            Destroy(coll.gameObject, 1f);
            coll.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
