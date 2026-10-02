using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreDisplay : MonoBehaviour
{
    public static int scoreValue = 0;
    private Text score;
    private List<GameObject> coins;
    public void Start()
    {
        coins = new List<GameObject>(GameObject.FindGameObjectsWithTag("Coin"));

        score = GetComponent<Text>();
        if (score == null)
            Debug.Log("null reference!!!");
    }
    public void Update()
    {
        score.text = "Coins Collected: " + scoreValue.ToString() + " / " + coins.Count;
    }
}