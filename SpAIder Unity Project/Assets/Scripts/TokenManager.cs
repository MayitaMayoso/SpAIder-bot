using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if ( other.tag == "Token" ) {
            GameObject.Destroy(other.gameObject);
        }
    }
}
