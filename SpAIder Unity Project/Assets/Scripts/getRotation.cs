using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getRotation : MonoBehaviour {
    public Transform trans;

    private void Update() {
        gameObject.GetComponent<Text>().text = "Angle: \n" + Mathf.Round((trans.eulerAngles.y<180)?trans.eulerAngles.y:-360+trans.eulerAngles.y).ToString() + "º";
    }
}
