using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    public Text coinNum;
    public Text healthNum;
    public Text timeNum;

    public static int coins;
    public int health;
    public float timeMax;
    private float timeLeft;

    // Start is called before the first frame update
    void Awake()
    {
        coins = 10000000;
        health = PlayerController.health;
        timeLeft = timeMax;
    }

    // Update is called once per frame
    void Update()
    {
        health = PlayerController.health;
        timeLeft -= Time.deltaTime;
        coinNum.text = coins.ToString();
        healthNum.text = health.ToString();
        timeNum.text = Mathf.Floor(timeLeft).ToString();

    }

    //private void 
}
