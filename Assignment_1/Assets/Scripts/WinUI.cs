using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    public GameObject ball;
    private int coins;

    void Start()
    {
        if (ball == null)
        {
            ball = GameObject.Find("Ball");
        }

        GetComponent<Text>().text = "";
        coins = new List<GameObject>(GameObject.FindGameObjectsWithTag("Coin")).Count;
    }

    void Update()
    {
        if (ball.transform.position.y < -50)
        {
            GetComponent<Text>().text = "You Won!\nYou collected " + (coins - new List<GameObject>(GameObject.FindGameObjectsWithTag("Coin")).Count) + " coins.";
        }
    }
}
