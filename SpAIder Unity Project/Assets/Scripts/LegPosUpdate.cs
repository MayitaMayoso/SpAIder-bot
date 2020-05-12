using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegPosUpdate : MonoBehaviour
{
    public Transform target;
    public float maxDistance;
    public float acceleration = 0.1f;

    Vector3 newPos;

    private void Start() {
        newPos = gameObject.transform.position;
    }

    void Update() {
        float dis2target = (target.position - gameObject.transform.position).magnitude;

        if (dis2target > maxDistance) {
            newPos = target.position;
        }

        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPos, acceleration);

        Debug.Log(dis2target);
    }
}
