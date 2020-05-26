using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSensorsValue : MonoBehaviour {
    public GameObject spider;

    private SensorSystem sensors;
    private Text text;
    // Start is called before the first frame update
    void Start() {
        sensors = spider.GetComponent<SensorSystem>();
        text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = sensors.binValue().ToString();
    }
}
