﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
	public GameObject BulletPrefab;
	int bulletLayer;
    Vector3 bulletOffset = new Vector3(0, 0.5f, 0);
    float delayAndSpawnRate = 0.3f;
    float timeUntilSpawnRateIncrease = 10;
    public int LifeOfPlayer = 3;
    public List<Coroutine> allCoroutines = new List<Coroutine>();

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
                Vector3 offset = transform.rotation * bulletOffset;
                GameObject bulletGO = Instantiate(BulletPrefab, transform.position + offset, transform.rotation);
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
}