using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    public int score;

    public TMP_Text ScoreText;
    public TMP_Text LifeOfPlayer;

    private static CanvasManager instance;
    public static CanvasManager Instance { get { return instance; } }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        score = 0;
    }

    void Start()
    {
        DamageManager.ScoreEvent += ChangeScoreText;
        PlayerManager.Instance.LifeTextCheckEvent += ChangeLifeOfPlayerText;
    }

    void ChangeScoreText()
    {
        score++;
        ScoreText.text = "Score: " + score;
    }

    void ChangeLifeOfPlayerText()
    {
        score++;
        ScoreText.text = "Score: " + score;
    }
}
