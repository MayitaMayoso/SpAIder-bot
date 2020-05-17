using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public GameObject spider;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) spider.GetComponent<BasicMovement>().setState(STATE.IDLE);
        if (Input.GetKeyDown(KeyCode.Alpha2)) spider.GetComponent<BasicMovement>().setState(STATE.MOVING);
        if (Input.GetKeyDown(KeyCode.Alpha3)) spider.GetComponent<BasicMovement>().setState(STATE.LEFT_ROTATION);
        if (Input.GetKeyDown(KeyCode.Alpha4)) spider.GetComponent<BasicMovement>().setState(STATE.RIGHT_ROTATION);
        if (Input.GetKeyDown(KeyCode.Alpha5)) spider.GetComponent<BasicMovement>().setState(STATE.TOTAL_ROTATION);
        if (Input.GetKeyDown(KeyCode.Alpha6)) spider.GetComponent<BasicMovement>().setState(STATE.FIXING_POSITION);
        if (Input.GetKeyDown(KeyCode.Alpha7)) spider.GetComponent<BasicMovement>().setState(STATE.MANUAL_CONTROL);
    }
}
