using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour {

    public int cityWidth = 40;
    public int cityHeight = 40;
    public int iterations = 30;
    public float turnChances = 0.8f;
    public float centerChances = 0.01f;
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


    private int[,] cityGrid;
    private int[] drunkMan;

    // Start is called before the first frame update
    void Start() {
        RenderSettings.fog = false;
        GenerateCity();
    }

    private void Update() {
        if (Input.GetKeyDown("space")) {
            foreach (Transform child in transform) {
                GameObject.Destroy(child.gameObject);
            }
            GenerateCity();
        }
    }

    private void GenerateCity() {
        if (cityWidth % 2 != 0) cityWidth += 1;
        if (cityWidth / 2 % 2 != 0) cityWidth += 2;
        if (cityHeight % 2 != 0) cityHeight += 1;
        if (cityHeight / 2 % 2 != 0) cityHeight += 2;

        // Initialize the cityGrid and the drunkMan
        cityGrid = new int[cityHeight, cityWidth];
        drunkMan = new int[] { cityWidth / 2, cityHeight / 2 };

        // Generate the city pattern
        cityGrid[drunkMan[1], drunkMan[0]] = 1;

        int direction = 0;
        for (int i = 0; i < iterations; i++) {
            if (drunkMan[0]%2==0 && drunkMan[1]%2==0)
                if (UnityEngine.Random.value < turnChances)
                    direction = UnityEngine.Random.Range(0, 4);

            switch (direction) {
                case 0: drunkMan[0] += 1; break;
                case 1: drunkMan[1] += 1; break;
                case 2: drunkMan[0] -= 1; break;
                case 3: drunkMan[1] -= 1; break;
            }

            if (drunkMan[0] < 2 || drunkMan[0] > cityWidth-2 || drunkMan[1] < 2 || drunkMan[1] > cityHeight - 2 || UnityEngine.Random.value < centerChances) {
                drunkMan[0] = cityWidth / 2;
                drunkMan[1] = cityHeight / 2;
            }

            cityGrid[drunkMan[1], drunkMan[0]] = 1;
        }

        // Generate the game objects out of the citty pattern
        float tileSize = NoRoad.GetComponent<Renderer>().bounds.size.x;
        float zStart = tileSize * -cityWidth / 2 - tileSize / 2;
        float xStart = tileSize * -cityHeight / 2 - tileSize / 2;
        for (int j = 0; j < cityHeight; j++) {
            for (int i = 0; i < cityWidth; i++) {
                if (cityGrid[j, i] == 1) {
                    GameObject currentTile = Road1;
                    int tileAngle = 0;

                    int tileType = cityGrid[j, i + 1] + cityGrid[j + 1, i] * 2 + cityGrid[j, i - 1] * 4 + cityGrid[j - 1, i] * 8;

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
                    if (Houses.Length > 0 && j>0 && j<cityHeight-1 && i>0 && i<cityWidth-1) {
                        int tileType = cityGrid[j, i + 1] + cityGrid[j + 1, i + 1] + cityGrid[j + 1, i] + cityGrid[j + 1, i - 1] + cityGrid[j, i - 1] + cityGrid[j - 1, i - 1] + cityGrid[j - 1, i] + cityGrid[j - 1, i + 1];
                        if (tileType > 0)
                            Instantiate(Houses[UnityEngine.Random.Range(0, Houses.Length)], new Vector3(xStart + tileSize * j, 0, zStart + tileSize * i), Quaternion.identity).transform.parent = gameObject.transform;
                    }
                }
            }
        }
    }
}
