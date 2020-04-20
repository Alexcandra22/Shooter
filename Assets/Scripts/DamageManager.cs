using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DamageManager : MonoBehaviour
{
	public int health = 1;
	float invulnTimer = 0;
	int correctLayer;
	SpriteRenderer spriteRend;
    SpriteRenderer sprite;

    public delegate void ScoreDelegate();
    public static event ScoreDelegate ScoreEvent;

    void Start()
    {
        PlayerManager.Instance.GameOverEvent += ShowGameOver;
		correctLayer = gameObject.layer;
		spriteRend = GetComponent<SpriteRenderer>();

        if (spriteRend == null)
        {
			spriteRend = transform.GetComponentInChildren<SpriteRenderer>();

			if(spriteRend==null)
            {
				Debug.LogError("Object '"+gameObject.name+"' has no sprite renderer.");
			}
		}
	}

    void OnTriggerEnter2D()
    {
        health--;

        if (health <= 0)
        {
            AddPoint();
        }
    }

    private void AddPoint()
    {
        ScoreEvent();
    }

    void Update()
    {
		if(invulnTimer > 0)
        {
			invulnTimer -= Time.deltaTime;

			if(invulnTimer <= 0)
            {
				gameObject.layer = correctLayer;

				if(spriteRend != null)
                {
					spriteRend.enabled = true;
				}
			}
			else {

				if(spriteRend != null)
                {
					spriteRend.enabled = !spriteRend.enabled;
				}
			}
		}

		if(health <= 0)
        {
			Die();
		}
	}

	void Die()
    {
        if (gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Bullet")
        {
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }   
    
    void ShowGameOver()
    {
        PlayerManager.Instance.StopAllCoroutines();
        SceneManager.LoadScene("GameOver");
    }
}
