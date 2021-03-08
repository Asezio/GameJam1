using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject reward;
    Transform objPos;
    public float flySpeed;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        objPos = this.gameObject.transform;
    }
    void DestroyItem()
    {
        Destroy(gameObject);
        Instantiate(reward, objPos.position, Quaternion.identity);
    }

    void HorizontalFlyItem()
    {

        rb.velocity = new Vector2(4f, 0f);
    }

    void FlyItem()
    {

        rb.velocity = new Vector2(4f, 0.8f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            HorizontalFlyItem();
        }

        if(col.gameObject.tag=="Enemy")
        {
            DestroyItem();
        }
    }

    

}
