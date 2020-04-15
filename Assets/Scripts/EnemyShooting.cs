using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{
    Vector3 bulletOffset = new Vector3(0, -0.8f, 0);
	public GameObject bulletPrefab;

    public float delayAndSpawnRate = 1f;
    public float timeUntilSpawnRateIncrease = 10;

    Transform player;


	void Start()
    {
        PlayerManager.Instance. allCoroutines.Add(StartCoroutine(BulletsCreating(delayAndSpawnRate)));
    }

    public IEnumerator BulletsCreating(float firstDelay)
    { 
        Transform CurrentEnemyTransform = gameObject.transform;
        float spawnRateCountdown = timeUntilSpawnRateIncrease;
        float spawnCountdown = firstDelay;

        while (true)
        {
            yield return null;
            spawnRateCountdown -= Time.deltaTime;
            spawnCountdown -= Time.deltaTime;

            if (spawnCountdown < 0)
            {
                spawnCountdown += delayAndSpawnRate;
                GameObject enemyBulletGO = Instantiate(bulletPrefab, gameObject.transform.position + bulletOffset, transform.rotation);
            }

            if (spawnRateCountdown < 0 && delayAndSpawnRate > 1)
            {
                spawnRateCountdown += timeUntilSpawnRateIncrease;
                delayAndSpawnRate -= 0.1f;
            }
        }
    }
}
