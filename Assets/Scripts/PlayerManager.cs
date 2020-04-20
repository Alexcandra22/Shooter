using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    GameObject BulletPrefab;

	int bulletLayer;
    Vector3 bulletOffset = new Vector3(0, 0.5f, 0);
    float delayAndSpawnRate = 0.3f;
    float timeUntilSpawnRateIncrease = 10;
    public Vector3 offset;
    int LifeOfPlayer = 3;
    public List<Coroutine> allCoroutines = new List<Coroutine>();

    public delegate void GameOverDelegate();
    public event GameOverDelegate GameOverEvent;

    public delegate void LifeTextCheckDelegate();
    public event LifeTextCheckDelegate LifeTextCheckEvent;

    private static PlayerManager instance;
    public static PlayerManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        allCoroutines.Add(StartCoroutine(BulletsCreating(delayAndSpawnRate)));
    }

    public IEnumerator BulletsCreating(float firstDelay)
    {
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
                offset = transform.rotation * bulletOffset;
                //GameObject bulletGO = Instantiate(BulletPrefab, transform.position + offset, transform.rotation);
                GameObject bulletGO = PoolObject.Instance.GetPooledObject();

                if (bulletGO != null)
                {
                    bulletGO.transform.position = transform.position + offset;
                    bulletGO.transform.rotation = transform.rotation;
                    bulletGO.SetActive(true);
                }
            }

            if (spawnRateCountdown < 0 && delayAndSpawnRate > 1)
            {
                spawnRateCountdown += timeUntilSpawnRateIncrease;
                delayAndSpawnRate -= 0.1f;
            }
        }
    }

    void StopAllCoroutine()
    {
        foreach (Coroutine cor in allCoroutines)
        {
            StopCoroutine(cor);
        }
    }

    void OnTriggerEnter2D()
    {
        DealDamage();
    }
    
    public void DealDamage()
    {
        LifeOfPlayer--;
        LifeTextCheckEvent();

        if (LifeOfPlayer == 0)
        {
            GameOverEvent();
        }
    }
}
