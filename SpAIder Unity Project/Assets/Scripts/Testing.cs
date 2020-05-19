using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;


public class Testing : MonoBehaviour
{
	public GameObject spider;

	public bool enableTesting = true;
	private bool isTesting = false;

	private BasicMovement bm;

	private void Start(){

		bm = spider.GetComponent<BasicMovement>();
		bm.updateState = false;
		
		/*

		if(isTesting){
			Debug.Log("-----TESTS-----");

			testMovementForward();	
			//setSpiderOnInit();

			testMovementRotateLeft();
			//setSpiderOnInit();


			testMovementRotateRight();
			//setSpiderOnInit();


			testMovementIdle();
			//setSpiderOnInit();


			testMovementTotalRotation();


		}
		*/

	}

	private void FixedUpdate() {
		if (enableTesting) {


			if (!isTesting) {
				switch (test) {
					case 0:
						beginForwardTest();
						isTesting = true;
						Timer = currentTime + 5000;
						break;
				}
			} else {
				if ( Timer < currentTime ) {
					switch (test) {
						case 0:
							endForwardTest();
							isTesting = false;
							test++;
							break;
					}
				}
			}
		}
	}

	private void setSpiderOnInit(){
		spider.transform.position = new Vector3(-5.0f, 2.2f, -5.0f);
		spider.transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);

	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha1)) spider.GetComponent<BasicMovement>().setState(STATE.IDLE);
		if (Input.GetKeyDown(KeyCode.Alpha2)) spider.GetComponent<BasicMovement>().setState(STATE.MOVING);
		if (Input.GetKeyDown(KeyCode.Alpha3)) spider.GetComponent<BasicMovement>().setState(STATE.LEFT_ROTATION);
		if (Input.GetKeyDown(KeyCode.Alpha4)) spider.GetComponent<BasicMovement>().setState(STATE.RIGHT_ROTATION);
		if (Input.GetKeyDown(KeyCode.Alpha5)) spider.GetComponent<BasicMovement>().setState(STATE.TOTAL_ROTATION);
		if (Input.GetKeyDown(KeyCode.Alpha6)) spider.GetComponent<BasicMovement>().setState(STATE.FIXING_POSITION);
		if (Input.GetKeyDown(KeyCode.Alpha7)) spider.GetComponent<BasicMovement>().setState(STATE.MANUAL_CONTROL);
	}

	private void testMovementForward(){
		Debug.Log("Movement forward at rotation 0");


		float initX = spider.transform.position.x;
		float initY = spider.transform.position.y;
		float initZ = spider.transform.position.z;
		float initRotX = spider.transform.eulerAngles.x;
		float initRotY = spider.transform.eulerAngles.y;
		float initRotZ = spider.transform.eulerAngles.x;
		float tolerance = 0.21f;

		//Spider functions
		bm.setState(STATE.MOVING);

		//Spider test
		//Looking at the x variable
		Assert.IsTrue(spider.transform.position.x > initX);
		//Looking at the y variable
		Assert.IsTrue(spider.transform.position.y == initY);
		//Looking at the z variable
		Assert.IsTrue(spider.transform.position.z == initZ);

		Debug.Log("Movement forward at rotation 90");
		initX = spider.transform.position.x;
		initY = spider.transform.position.y;
		initZ = spider.transform.position.z;
		initRotX = spider.transform.eulerAngles.x;
		initRotY = spider.transform.eulerAngles.y;
		initRotZ = spider.transform.eulerAngles.x;

		//Rotate it 90 degrees
		bm.right_rotation_state();
		bm.rotating_state();
		bm.fixing_position();

		//Spider functions
		bm.moving_state();
		bm.move();


		//Spider test
		//Looking at the x variable
		Assert.AreApproximatelyEqual(spider.transform.position.x, initX, tolerance);
		//Looking at the y variable
		Assert.IsTrue(spider.transform.position.y == initY);
		//Looking at the z variable
		Assert.IsTrue(spider.transform.position.z < initZ);

		Debug.Log("Movement forward at rotation 180");
		initX = spider.transform.position.x;
		initY = spider.transform.position.y;
		initZ = spider.transform.position.z;
		initRotX = spider.transform.eulerAngles.x;
		initRotY = spider.transform.eulerAngles.y;
		initRotZ = spider.transform.eulerAngles.x;

		//Rotate it 180 degrees
		bm.right_rotation_state();
		bm.rotating_state();
		bm.fixing_position();

		//Spider functions

		bm.moving_state();
		bm.move();

		//Spider test
		//Looking at the x variable
		Assert.IsTrue(spider.transform.position.x < initX);
		//Looking at the y variable
		Assert.IsTrue(spider.transform.position.y == initY);
		//Looking at the z variable
		Assert.AreApproximatelyEqual(spider.transform.position.z, initZ, tolerance);


		Debug.Log("Movement forward at rotation 270");
		initX = spider.transform.position.x;
		initY = spider.transform.position.y;
		initZ = spider.transform.position.z;
		initRotX = spider.transform.eulerAngles.x;
		initRotY = spider.transform.eulerAngles.y;
		initRotZ = spider.transform.eulerAngles.x;

		//Rotate it 270 degrees
		bm.right_rotation_state();
		bm.rotating_state();
		bm.fixing_position();

		//Spider functions
		bm.moving_state();
		bm.move();
		//Spider test
		//Looking at the x variable
		Assert.AreApproximatelyEqual(spider.transform.position.x, initX, tolerance);
		//Looking at the y variable
		Assert.IsTrue(spider.transform.position.y == initY);
		//Looking at the z variable
		Assert.IsTrue(spider.transform.position.z > initZ);
		Debug.Log("TEST PASSED");

	}
	private void testMovementRotateRight(){
		Debug.Log("Rotation Right");
		float initRotY = spider.transform.eulerAngles.y;

		//float tolerance = 0.1f;
		Debug.Log(spider.transform.rotation);

		bm.right_rotation_state();
		bm.rotating_state();
		bm.fixing_position();

		Debug.Log(spider.transform.rotation);


		Assert.IsTrue(spider.transform.eulerAngles.y > initRotY);
		Debug.Log("TEST PASSED");

	}
	private void testMovementRotateLeft(){

		Debug.Log("Rotation Left");
		float initRotY = spider.transform.eulerAngles.y;

		//float tolerance = 0.1f;
		Debug.Log(spider.transform.eulerAngles);

		bm.left_rotation_state();
		bm.rotating_state();
		bm.fixing_position();

		Debug.Log(spider.transform.eulerAngles);

		Assert.IsTrue(spider.transform.eulerAngles.y < initRotY);

		Debug.Log("TEST PASSED");


	}
	private void testMovementIdle(){

		Debug.Log("Idle state");
		float initX = spider.transform.position.x;
		float initY = spider.transform.position.y;
		float initZ = spider.transform.position.z;
		float tolerance = 0.26f;

		bm.idle_state();
		bm.move();

		Assert.AreApproximatelyEqual(spider.transform.position.x, initX, tolerance);
		Assert.AreApproximatelyEqual(spider.transform.position.y, initY, tolerance);
		Assert.AreApproximatelyEqual(spider.transform.position.z, initZ, tolerance);

		Debug.Log("TEST PASSED");

	}
	private void testMovementTotalRotation(){

		Debug.Log("Total Rotation");
		float initRotY = spider.transform.eulerAngles.y;

		Debug.Log(spider.transform.eulerAngles);

		bm.total_rotation_state();
		bm.rotating_state();
		bm.fixing_position();



		Assert.IsTrue(spider.transform.eulerAngles.y > initRotY);

		Debug.Log(spider.transform.eulerAngles);
		Debug.Log("TEST PASSED");


	}
}
