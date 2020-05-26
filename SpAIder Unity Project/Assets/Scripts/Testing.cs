using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;
using UnityEngine.UI;



public class Testing : MonoBehaviour
{
	public int timePerTest = 1000;
	private int test = 0;
	float timer;

	public float tolerance = 0.5f;

	private STATE[] testList;

	public GameObject spider;

	public bool enableTesting = true;
	private bool isTesting = false;

	private Movement bm;
	private string text;

	private void Start(){
		bm = spider.GetComponent<Movement>();
		testList = new STATE[] { STATE.MOVING, STATE.MOVING, STATE.LEFT_ROTATION, STATE.FIXING_POSITION, STATE.MOVING, STATE.MOVING, STATE.LEFT_ROTATION, STATE.FIXING_POSITION
		, STATE.MOVING, STATE.MOVING, STATE.LEFT_ROTATION, STATE.FIXING_POSITION, STATE.MOVING, STATE.MOVING, STATE.TOTAL_ROTATION, STATE.FIXING_POSITION
		, STATE.MOVING, STATE.MOVING, STATE.RIGHT_ROTATION, STATE.FIXING_POSITION, STATE.MOVING, STATE.MOVING, STATE.RIGHT_ROTATION, STATE.FIXING_POSITION
		, STATE.MOVING, STATE.MOVING, STATE.RIGHT_ROTATION, STATE.FIXING_POSITION, STATE.MOVING, STATE.MOVING, STATE.TOTAL_ROTATION, STATE.FIXING_POSITION};
	}

	private void FixedUpdate() {
		if (enableTesting) {
			float currentTime = Time.timeSinceLevelLoad * 1000f;

			if (!isTesting) {
				testing(testList[test]);
				isTesting = true;
				timer = currentTime + timePerTest;

			} else {
				if (currentTime > timer) {
					isTesting = false;
					test++;
					if ( test >= testList.Length ) {
						test = 0;
					}
				}
			}
		}

		gameObject.GetComponent<Text>().text = text + "\n" + transStr();


	}

	private string transStr() {
		return "X: " + Mathf.Round(spider.GetComponent<Transform>().position.x).ToString() + "		Z: " + Mathf.Round(spider.GetComponent<Transform>().position.z).ToString() + "\nAngle: " + Mathf.Round(spider.GetComponent<Transform>().rotation.eulerAngles.y).ToString();
	}

	private void testing( STATE state ) {
		switch (state) {
			case STATE.MOVING: TestForward(); break;
			case STATE.LEFT_ROTATION: TestRotationLeft(); break;
			case STATE.RIGHT_ROTATION: TestRotationRight(); break;
			case STATE.TOTAL_ROTATION: TestRotationTotal(); break;
			case STATE.FIXING_POSITION: TestFixing(); break;
		}
	}

	private void TestForward() {
		text = "Moving forward";
		bm.setState(STATE.MOVING);
	}

	private void TestRotationRight() {
		text = "Rotating right";
		bm.setState(STATE.RIGHT_ROTATION);
	}

	private void TestRotationLeft() {
		text = "Rotating left";
		bm.setState(STATE.LEFT_ROTATION);
	}

	private void TestRotationTotal() {
		text = "Total rotation";
		bm.setState(STATE.TOTAL_ROTATION);

	}

	private void TestFixing() {
		text = "Fixing position";
		bm.setState(STATE.FIXING_POSITION);

	}

	private void manualTesting() {
		if (Input.GetKeyDown(KeyCode.Alpha1)) spider.GetComponent<Movement>().setState(STATE.IDLE);
		if (Input.GetKeyDown(KeyCode.Alpha2)) spider.GetComponent<Movement>().setState(STATE.MOVING);
		if (Input.GetKeyDown(KeyCode.Alpha3)) spider.GetComponent<Movement>().setState(STATE.LEFT_ROTATION);
		if (Input.GetKeyDown(KeyCode.Alpha4)) spider.GetComponent<Movement>().setState(STATE.RIGHT_ROTATION);
		if (Input.GetKeyDown(KeyCode.Alpha5)) spider.GetComponent<Movement>().setState(STATE.TOTAL_ROTATION);
		if (Input.GetKeyDown(KeyCode.Alpha6)) spider.GetComponent<Movement>().setState(STATE.FIXING_POSITION);
		if (Input.GetKeyDown(KeyCode.Alpha7)) spider.GetComponent<Movement>().setState(STATE.MANUAL_CONTROL);
	}
}
