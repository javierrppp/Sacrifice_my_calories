using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public Text scoreText;

    private static ScoreController scoreController;

    public static ScoreController _instance
    {
        get
        {
            if (!scoreController)
            {
                scoreController = FindObjectOfType(typeof(ScoreController)) as ScoreController;

                if (!scoreController)
                {
                    GameObject _foodFactory = new GameObject();
                    scoreController = _foodFactory.AddComponent<ScoreController>();
                }
                else
                {
                    scoreController.Init();
                }
            }

            return scoreController;
        }
    }

    private void Init()
    {

    }

    public void changeScore(int score)
    {
        this.scoreText.text = "Score:" + score;
    }

    public void clearScore()
    {
        this.scoreText.text = "Score: 0";
    }
}
