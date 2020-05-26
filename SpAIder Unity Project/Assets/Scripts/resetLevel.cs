using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resetLevel : MonoBehaviour
{
    void OnTriggerStay(Collider other) {
        if (other.tag == "Solid") {
            SceneManager.LoadScene("Maze");
        }
    }
}
