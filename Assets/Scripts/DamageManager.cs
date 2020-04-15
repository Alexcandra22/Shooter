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

    void Start()
    {
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
        if (gameObject.layer == 8 || gameObject.layer == 13 || gameObject.layer == 14)
        {
            PlayerManager.Instance.LifeOfPlayer--;

            if (PlayerManager.Instance.LifeOfPlayer == 0)
            {
                ShowGameOver();
            }
        }
        else
        {
            health--;
            if (health <= 0)
            {
                AddPoint();
            }
        }
    }

    private void AddPoint()
    {
        if (gameObject.layer != 8 && gameObject.layer !=12 && gameObject.layer !=13)
        {
            CanvasManager.Instance.score++;
        }
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
        Destroy(gameObject);
    }   
    
    void ShowGameOver()
    {
        PlayerManager.Instance.StopAllCoroutines();
        SceneManager.LoadScene("GameOver");
    }
}
