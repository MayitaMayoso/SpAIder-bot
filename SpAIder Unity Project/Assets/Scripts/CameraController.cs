using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float speed = 1.0f;
    public float acceleration = 0.1f;
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private float currXSpd = 0.0f;
    private float currYSpd = 0.0f;
    private float currZSpd = 0.0f;


    private float lerp(float origin, float target, float amount) {
        return origin + (target - origin) * amount;
    }

    // Update is called once per frame
    void FixedUpdate() {
        int xdir = (Input.GetKey("d") ? 1 : 0) - (Input.GetKey("a") ? 1 : 0);
        int ydir = (Input.GetKey("q") ? 1 : 0) - (Input.GetKey("e") ? 1 : 0);
        int zdir = (Input.GetKey("w") ? 1 : 0) - (Input.GetKey("s") ? 1 : 0);
        int booster = (Input.GetKey("left shift") ? 5 : 1);

        currXSpd = lerp(currXSpd, xdir * speed * booster, acceleration);
        currYSpd = lerp(currYSpd, ydir * speed * booster, acceleration);
        currZSpd = lerp(currZSpd, zdir * speed * booster, acceleration);
        transform.Translate(Vector3.forward * currZSpd + Vector3.up * currYSpd + Vector3.right * currXSpd);

        if (Input.GetMouseButton(0)) {
            float rotateHorizontal = Input.GetAxis("Mouse X");
            float rotateVertical = Input.GetAxis("Mouse Y");

            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            pitch = Mathf.Clamp(pitch, -90, 90);

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }
}
