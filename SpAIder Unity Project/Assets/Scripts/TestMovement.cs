using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public int changeSpeed = 100;

    public int test = 0;
    private int[,] cityGrid;

    public GameObject NoRoad;
    public GameObject Road0;
    public GameObject Road1;
    public GameObject Road2A;
    public GameObject Road2B;
    public GameObject Road3;
    public GameObject Road4;
    public GameObject[] Houses;

    [Range(0, 3)]
    public int Road1Rot = 0;
    [Range(0, 3)]
    public int Road2ARot = 0;
    [Range(0, 3)]
    public int Road2BRot = 0;
    [Range(0, 3)]
    public int Road3Rot = 0;

    public int testLife = 0;

    private void BuildCity() {
        cityGrid = new int[9, 9];

        for (int j = 0; j < 9; j++) {
            for (int i = 0; i < 9; i++) {
                cityGrid[j, i] = (j % 2 == 0 || i % 2 == 0) ? 1 : 0;
            }
        }

                foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }

        float tileSize = NoRoad.GetComponent<Renderer>().bounds.size.x;
        float zStart = tileSize * -9 / 2 - tileSize / 2 + 5;
        float xStart = tileSize * -9 / 2 - tileSize / 2 + 5;
        for (int j = 0; j < 9; j++) {
            for (int i = 0; i < 9; i++) {
                if (cityGrid[j, i] == 1) {
                    GameObject currentTile = Road1;
                    int tileAngle = 0;

                    int left = (i>0) ? cityGrid[j, i - 1] * 4 : 0;
                    int right = (i<8) ? cityGrid[j, i + 1] * 1: 0;
                    int up = (j<8) ? cityGrid[j + 1, i] * 2: 0;
                    int down = (j>0) ? cityGrid[j - 1, i] * 8: 0;
                    int tileType = right + up + left + down;

                    switch (tileType) {
                        case 0: // ========================= ROAD 0
                            currentTile = Road0;
                            break;
                        case 1: // ========================= ROAD 1
                            currentTile = Road1;
                            tileAngle = Road1Rot * 90;
                            break;
                        case 2:
                            currentTile = Road1;
                            tileAngle = 90 + Road1Rot * 90;
                            break;
                        case 4:
                            currentTile = Road1;
                            tileAngle = 180 + Road1Rot * 90;
                            break;
                        case 8:
                            currentTile = Road1;
                            tileAngle = 270 + Road1Rot * 90;
                            break;
                        case 5: // ========================= ROAD 2A
                            currentTile = Road2A;
                            tileAngle = Road2ARot * 90;
                            break;
                        case 10:
                            currentTile = Road2A;
                            tileAngle = 90 + Road2ARot * 90;
                            break;
                        case 3: // ========================= ROAD 2B
                            currentTile = Road2B;
                            tileAngle = Road2BRot * 90;
                            break;
                        case 6:
                            currentTile = Road2B;
                            tileAngle = 90 + Road2BRot * 90;
                            break;
                        case 12:
                            currentTile = Road2B;
                            tileAngle = 180 + Road2BRot * 90;
                            break;
                        case 9:
                            currentTile = Road2B;
                            tileAngle = 270 + Road2BRot * 90;
                            break;
                        case 7: // ========================= ROAD 3
                            currentTile = Road3;
                            tileAngle = Road3Rot * 90;
                            break;
                        case 14:
                            currentTile = Road3;
                            tileAngle = 90 + Road3Rot * 90;
                            break;
                        case 13:
                            currentTile = Road3;
                            tileAngle = 180 + Road3Rot * 90;
                            break;
                        case 11:
                            currentTile = Road3;
                            tileAngle = 270 + Road3Rot * 90;
                            break;
                        case 15: // ========================= ROAD 4
                            currentTile = Road4;
                            break;
                    }

                    Instantiate(currentTile, new Vector3(xStart + tileSize * j, 0, zStart + tileSize * i), Quaternion.Euler(0, tileAngle, 0)).transform.parent = gameObject.transform;
                } else {
                    Instantiate(NoRoad, new Vector3(xStart + tileSize * j, 0, zStart + tileSize * i), Quaternion.identity).transform.parent = gameObject.transform;
                    if (Houses.Length > 0) {
                        Instantiate(Houses[UnityEngine.Random.Range(0, Houses.Length)], new Vector3(xStart + tileSize * j, 0, zStart + tileSize * i), Quaternion.identity).transform.parent = gameObject.transform;
                    }
                }
            }
        }
    }

    private void Start() {
        cityGrid = new int[3, 3];
        BuildCity();
    }
}
