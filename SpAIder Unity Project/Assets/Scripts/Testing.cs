using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;


public class Testing : MonoBehaviour
{
	public class testing
	{


		public float initX;
		public float initY;
		public float initZ;
		public float initRotX;
		public float initRotY;
		public float initRotZ;
		public float tolerance;
	}

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

			testing t = new testing();
			float Timer;
			float currentTime = Time.timeSinceLevelLoad * 1000f;

			if (!isTesting) {
				switch (test) {
					case 0:
						beginForwardTest(t);
						isTesting = true;
						Timer = currentTime + 5000;
						break;
					case 1:
						beginRotationRightTest(t);
						isTesting = true;
						Timer = currentTime + 5000;
						break;
					case 2:
						beginRotationTotalTest(t);
						isTesting = true;
						Timer = currentTime + 5000;
						break;
					case 3:
						beginRotationLeftTest(t);
						isTesting = true;
						Timer = currentTime + 5000;
						break;

				}
			} else {
				if ( Timer < currentTime ) {
					switch (test) {
						case 0:
							endForwardTest(t);
							isTesting = false;
							test++;
							break;
						case 1:
							endRotationRightTest(t);
							isTesting = false;
							test++;
							break;
						case 2:
							endRotationTotalTest(t);
							isTesting = false;
							test++;
							break;
						case 3:
							endRotationLeftTest(t);
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

	private void beginForwardTest(testing t) {

		Debug.Log("Movement forward at rotation 0");
		// start 
		t.initX = spider.transform.position.x;
		t.initY = spider.transform.position.y;
		t.initZ = spider.transform.position.z;
		t.initRotX = spider.transform.eulerAngles.x;
		t.initRotY = spider.transform.eulerAngles.y;
		t.initRotZ = spider.transform.eulerAngles.x;
		t.tolerance = 0.21f;

		//Spider functions
		bm.setState(STATE.MOVING);

	}

	private void endForwardTest( testing t ) {

		//Spider test
		//Looking at the x variable
		Assert.IsTrue(spider.transform.position.x > t.initX);
		//Looking at the y variable
		Assert.IsTrue(spider.transform.position.y == t.initY);
		//Looking at the z variable
		Assert.IsTrue(spider.transform.position.z == t.initZ);

		Debug.Log("TEST PASSED");
	}

	private void beginRotationRightTest(testing t) {

		Debug.Log("Movement forward at rotation right");
		t.initX = spider.transform.position.x;
		t.initY = spider.transform.position.y;
		t.initZ = spider.transform.position.z;
		t.initRotX = spider.transform.eulerAngles.x;
		t.initRotY = spider.transform.eulerAngles.y;
		t.initRotZ = spider.transform.eulerAngles.x;

		//Spider functions
		bm.setState(STATE.RIGHT_ROTATION);

	}

	private void endRotationRightTest(testing t) {

		//Spider test
		//Looking at the x variable
		Assert.AreApproximatelyEqual(spider.transform.position.x, t.initX, t.tolerance);
		//Looking at the y variable
		Assert.IsTrue(spider.transform.position.y == t.initY);
		//Looking at the z variable
		Assert.IsTrue(spider.transform.position.z < t.initZ);

		Debug.Log("TEST PASSED");
	}

	private void beginRotationTotalTest(testing t) 	{


		Debug.Log("Movement forward at rotation total");
		t.initX = spider.transform.position.x;
		t.initY = spider.transform.position.y;
		t.initZ = spider.transform.position.z;
		t.initRotX = spider.transform.eulerAngles.x;
		t.initRotY = spider.transform.eulerAngles.y;
		t.initRotZ = spider.transform.eulerAngles.x;

	
		//Spider functions
		bm.setState(STATE.TOTAL_ROTATION);

	}

	private void endRotationTotalTest(testing t) {


		//Spider test
		//Looking at the x variable
		Assert.IsTrue(spider.transform.position.x < t.initX);
		//Looking at the y variable
		Assert.IsTrue(spider.transform.position.y == t.initY);
		//Looking at the z variable
		Assert.AreApproximatelyEqual(spider.transform.position.z, t.initZ, t.tolerance);

		Debug.Log("TEST PASSED");
	}

	private void beginRotationLeftTest(testing t)
	{


		Debug.Log("Movement forward at rotation Left");
		t.initX = spider.transform.position.x;
		t.initY = spider.transform.position.y;
		t.initZ = spider.transform.position.z;
		t.initRotX = spider.transform.eulerAngles.x;
		t.initRotY = spider.transform.eulerAngles.y;
		t.initRotZ = spider.transform.eulerAngles.x;


		//Spider functions
		bm.setState(STATE.LEFT_ROTATION);

	}

	private void endRotationLeftTest(testing t)
	{

		//Spider test
		//Looking at the x variable
		Assert.IsTrue(spider.transform.position.x < t.initX);
		//Looking at the y variable
		Assert.IsTrue(spider.transform.position.y == t.initY);
		//Looking at the z variable
		Assert.AreApproximatelyEqual(spider.transform.position.z, t.initZ, t.tolerance);

		Debug.Log("TEST PASSED");
	}

	private void testMovementForward(){
		Debug.Log("Movement forward at rotation 0");

		





	

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
