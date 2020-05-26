using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public bool colliding = false;

    private void LateUpdate() {
        colliding = false;
    }

    void OnTriggerStay(Collider other) {
        if ( other.tag == "Solid" ) {
            colliding = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Solid") {
            colliding = false;
        }
    }
}
