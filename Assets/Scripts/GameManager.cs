using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{    public int score;    public Text scoreText;
    public GameObject playerPrefab;

    void Start()
    {
        score = 0;
    }
    void Update()
    {        ChangeScoreText();
                if(score >= 990)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    public void ChangeScoreText()
    {
        if(score < 10)
        {
            scoreText.text = "000" + score.ToString();
        }

        if(score >= 10 && score < 100)
        {
            scoreText.text = "00" + score.ToString();
        }

        if (score >= 100 && score < 1000)
        {
            scoreText.text = "0" + score.ToString();
        }

        if (score > 1000)
        {
            scoreText.text = score.ToString();
        }
    }
}
