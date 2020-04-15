using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private static float maxSpeed;
    public float startingSpeed;

    void Update()
    {
        if (maxSpeed == 0)
        {
            maxSpeed = startingSpeed;
        }

        gameObject.transform.position += Vector3.down * startingSpeed;
    }

}
