using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorSystem : MonoBehaviour
{
    public GameObject centerCollider;
    public GameObject leftCollider;
    public GameObject rightCollider;
    public GameObject backCollider;

    public int binValue() {
        bool cColl = centerCollider.GetComponent<Sensor>().colliding;
        bool lColl = leftCollider.GetComponent<Sensor>().colliding;
        bool rColl = rightCollider.GetComponent<Sensor>().colliding;

        return (lColl ? 1 : 0) * 1 + (cColl ? 1 : 0) * 2 + (rColl ? 1 : 0) * 4;
    }

    public bool[] completeValue() {
        bool cColl = centerCollider.GetComponent<Sensor>().colliding;
        bool lColl = leftCollider.GetComponent<Sensor>().colliding;
        bool rColl = rightCollider.GetComponent<Sensor>().colliding;
        bool bColl = backCollider.GetComponent<Sensor>().colliding;

        bool[] arr = new bool[4];
        arr[0] = cColl;
        arr[1] = lColl;
        arr[2] = bColl;
        arr[3] = rColl;

        return arr;
    }
}
