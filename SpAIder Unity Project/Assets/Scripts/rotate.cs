using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public float xspeed = 0;
    public float yspeed = 0;
    public float zspeed = 0;

    // Update is called once per frame
    void Update() {
        transform.eulerAngles += new Vector3(xspeed, yspeed, zspeed);
    }
}
