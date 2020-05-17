using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegPosUpdate : MonoBehaviour
{
    public Transform target;
    public float maxDistance;
    public float acceleration = 0.1f;
    public float randDist = 0.5f;

    Vector3 newPos;

    private void Start() {
        newPos = gameObject.transform.position;
    }

    void Update() {
        float dis2target = (target.position - gameObject.transform.position).magnitude;

        if (dis2target > maxDistance) {
            float xDist = UnityEngine.Random.Range( - randDist, randDist);
            float zDist = UnityEngine.Random.Range( - randDist, randDist);
            newPos = target.position + new Vector3(xDist, 0.0f, zDist);
        }

        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPos, acceleration);

    }
}
