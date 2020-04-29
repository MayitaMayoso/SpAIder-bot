using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float acceleration = 0.1f;

    private float currSpd = 0.0f;

    private float lerp( float origin, float target, float amount) {
        return origin + (target-origin)*amount;
    }

    // Update is called once per frame
    void FixedUpdate() {
        int dir = (Input.GetKey("up")?1:0) - (Input.GetKey("down")?1:0);

        currSpd = lerp(currSpd, dir*speed, acceleration);
        transform.position += new Vector3(0, 0, currSpd);
    }
}
