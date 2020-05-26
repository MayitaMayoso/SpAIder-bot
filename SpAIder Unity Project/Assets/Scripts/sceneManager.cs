using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    private void Start() {
        DontDestroyOnLoad(gameObject);
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            string scn = SceneManager.GetActiveScene().name;

            switch(scn) {
                case "Maze": SceneManager.LoadScene("MazeCenital"); break;
                case "MazeCenital": SceneManager.LoadScene("LegsTest"); break;
                case "LegsTest": SceneManager.LoadScene("MovementTest"); break;
                case "MovementTest": SceneManager.LoadScene("SensorsTest"); break;
                case "SensorsTest": SceneManager.LoadScene("Maze"); break;
            }
        }
    }

}
