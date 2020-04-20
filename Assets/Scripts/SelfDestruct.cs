using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

	public float timer = 1f;

	void Update () {
		timer -= Time.deltaTime;

		if(timer <= 0) {

            if (gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Bullet")
            {
                gameObject.SetActive(false);
                timer = 5f;
            }
            else
            {
                Destroy(gameObject);
            }
		}
	}
}
