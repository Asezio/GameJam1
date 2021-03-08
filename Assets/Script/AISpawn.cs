using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawn : MonoBehaviour
{
    public Transform spawnPoint1;
    public GameObject monsters;
    public static bool spawnAllowed;
    public static AISpawn instance;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        spawnAllowed = true;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="Player"&& spawnAllowed)
        {
            SpawnMonster();
        }    
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && spawnAllowed)
        {
            SpawnMonster();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag=="Player")
        {
            spawnAllowed = false;
        }
    }
    void SpawnMonster()
    {
        Instantiate(monsters, spawnPoint1.position, Quaternion.identity);
        spawnAllowed = false;
    }

}

