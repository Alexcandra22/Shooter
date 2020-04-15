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

    void Update()
    {
        ScoreText.text = "Score: " + score;
        LifeOfPlayer.text = PlayerManager.Instance.LifeOfPlayer.ToString();
    }
}
