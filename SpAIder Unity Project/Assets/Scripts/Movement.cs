using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATE {
	IDLE,
	MOVING,
	LEFT_ROTATION,
	RIGHT_ROTATION,
	TOTAL_ROTATION,
	ROTATING,
	FIXING_POSITION,
	MANUAL_CONTROL,
	_ENUMSIZE
};

public class Movement : MonoBehaviour
{
	public float movSpeed = 0.1f;
	public float rotSpeed = 1.0f;
	public float acceleration = 0.1f;
	public bool manualControl = false;

	private bool fixingFinished = false;

	private float fSpd = 0.0f;
	private float sSpd = 0.0f;
	private float rSpd = 0.0f;

	private int fDir = 0;
	private int sDir = 0;
	private int rDir = 0;

	private Vector3 targetPosition;
	private float targetRotation;

	public bool UpdateStates = false;

	private STATE state;

	private int count2Start = 0;

	private float lerp( float origin, float target, float amount) {
		return origin + (target-origin)*amount;
	}

	public void setState( STATE state ) {
		this.state = state;
	}

	public bool getFixingFinished() {
		return fixingFinished;
	}

	public STATE getState() {
		return state;
	}

	private void Start() {
		targetRotation = transform.rotation.eulerAngles.y;
	}

	// Update is called once per frame
	private void FixedUpdate() {

		fDir = 0;
		sDir = 0;
		rDir = 0;

		switch(state) {
			case STATE.IDLE: idle_state(); break;
			case STATE.MOVING: moving_state(); break;
			case STATE.LEFT_ROTATION: left_rotation_state(); break;
			case STATE.RIGHT_ROTATION: right_rotation_state(); break;
			case STATE.TOTAL_ROTATION: total_rotation_state(); break;
			case STATE.ROTATING: rotating_state(); break;
			case STATE.FIXING_POSITION: fixing_position(); break;
		}

		if (manualControl) {
			fDir = ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) ? 1 : 0) - ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) ? 1 : 0);
			sDir = ((Input.GetKey(KeyCode.D)) ? 1 : 0) - ((Input.GetKey(KeyCode.A)) ? 1 : 0);
			rDir = ((Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.RightArrow)) ? 1 : 0) - ((Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow)) ? 1 : 0);
		}

		fSpd = lerp(fSpd, fDir * movSpeed, acceleration);
		sSpd = lerp(sSpd, sDir * movSpeed, acceleration);
		rSpd = lerp(rSpd, rDir * rotSpeed, acceleration);

		transform.position += fSpd * transform.right + sSpd * -transform.forward;
		transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y + rSpd, 0.0f);
	}

	private void idle_state() {
		if (count2Start >= 100) {
			if (UpdateStates) state = STATE.MOVING;
		} else {
			count2Start++;
		}
	}

	private void moving_state() {
		fDir = 1;
		transform.eulerAngles = new Vector3(0.0f, targetRotation, 0.0f);
	}

	private void left_rotation_state() { 
		targetRotation = transform.rotation.eulerAngles.y - 90;
		fixingFinished = false;
		state = STATE.ROTATING;
	}

	private void right_rotation_state() {
		targetRotation = transform.rotation.eulerAngles.y + 90;
		fixingFinished = false;
		state = STATE.ROTATING;
	}
	private void total_rotation_state() {
		targetRotation = transform.rotation.eulerAngles.y + 180;
		fixingFinished = false;
		state = STATE.ROTATING;
	}

	private void rotating_state() {
		float aDiff = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetRotation);

		if ( Mathf.Abs(aDiff) > 4 ) {
			rDir = (int)Mathf.Sign(aDiff);
		} else {
			transform.eulerAngles = new Vector3(0.0f, lerp(transform.eulerAngles.y, transform.eulerAngles.y + aDiff, acceleration), 0.0f);

			if (UpdateStates) state = STATE.FIXING_POSITION;
		}
	}

	private void fixing_position() {
		targetPosition = new Vector3(Mathf.Round( ( transform.position.x + 5.0f ) / 10) * 10, transform.position.y, Mathf.Round( ( transform.position.z + 5.0f) / 10) * 10);
		Vector3 auxDirs = targetPosition - new Vector3(transform.position.x + 5.0f, transform.position.y, transform.position.z + 5.0f);

		if ( auxDirs.magnitude > 2 * movSpeed ) {
			fDir = Math.Sign(auxDirs.z * transform.right.z + auxDirs.x * transform.right.x);
			sDir = Math.Sign(auxDirs.z * -transform.forward.z + auxDirs.x * -transform.forward.x);
		} else {
			fixingFinished = true;
		}
	}
}
