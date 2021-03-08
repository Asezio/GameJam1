using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject target;
    float moveSpeed;
    public Vector3 directionToTarget;
    //public GameObject explosion;
    public static AIController instance;

    void Start()
    {
        instance = this;
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(1f, 3f);
    }

    void Update()
    {
        MoveMonster();
  
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag=="Player")
        {
            Destroy(gameObject);
            AISpawn.spawnAllowed = true;
        }
    }
    void MoveMonster()
    {
        if(target!=null)
        {
            directionToTarget = (target.transform.position - transform.position).normalized;
            
            rb.velocity = new Vector2(directionToTarget.x * moveSpeed, directionToTarget.y * moveSpeed);
        }

        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}
