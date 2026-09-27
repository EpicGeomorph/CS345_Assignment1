using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreDisplay : MonoBehaviour
{
    public static int scoreValue = 0;
    private Text score;
    public void Start()
    {
        score = GetComponent<Text>();
        if (score == null)
            Debug.Log("null reference!!!");
    }
    public void Update()
    {
        score.text = "Score: " + scoreValue.ToString();
    }
}