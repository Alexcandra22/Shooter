using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> Enemy;
    int enemyLayer;
    public float delayAndSpawnRate = 2;
    public float timeUntilSpawnRateIncrease = 10;
    public static float speed = 0.01f;

    void Start()
    {
        PlayerManager.Instance.allCoroutines.Add(StartCoroutine(EnemySpawner(delayAndSpawnRate)));
        PlayerManager.Instance.allCoroutines.Add(StartCoroutine(ExecuteAfterTime()));
    }

    IEnumerator EnemySpawner(float firstDelay)
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
                Vector3 RandomPosition = new Vector3(Random.Range(-1.2f, 1.2f), gameObject.transform.position.y, gameObject.transform.position.z);
                GameObject currentEnemy = Instantiate(Enemy[(int)Random.Range(0f, 3f)]);
                currentEnemy.transform.position = RandomPosition;
                currentEnemy.GetComponent<EnemyMove>().startingSpeed = speed;
            }

            if (spawnRateCountdown < 0 && delayAndSpawnRate > 1)
            {
                spawnRateCountdown += timeUntilSpawnRateIncrease;
                delayAndSpawnRate -= 0.1f;
            }
        }
    }

    IEnumerator ExecuteAfterTime()
    {
        while (true)
        {
            speed = speed + 0.01f;
            yield return new WaitForSeconds(8f);
        }
    }
}
