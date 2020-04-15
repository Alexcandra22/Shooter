using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {

	public float maxSpeed;

	void Update ()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);
        pos += transform.rotation * velocity;
        transform.position = pos;
    }
}
