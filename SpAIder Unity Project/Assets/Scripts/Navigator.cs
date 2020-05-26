using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour {
	public GameObject crossMark;

	private Movement movement;
	private SensorSystem sensors;

	private Vector3 lastChoice;
	private bool choosing = false;

	public class Cross {
		public bool northColl;
		public bool southColl;
		public bool westColl;
		public bool eastColl;

		public int northVisit = 0;
		public int southVisit = 0;
		public int westVisit = 0;
		public int eastVisit = 0;

		public int b2i(bool b) {
			return (b) ? -1 : 0;
		}

		public Cross( bool[] collisions, float dir) {
			float auxDir = Mathf.Round(dir / 90) * 90;

			switch(auxDir) {
				case 0:
				case 360:
					eastColl = collisions[0]; northColl = collisions[1]; westColl = collisions[2]; southColl = collisions[3];
					eastVisit = b2i(eastColl); northVisit = b2i(northColl); westVisit = 1; southVisit = b2i(southColl);
					break;
				case 90:
					eastColl = collisions[1]; northColl = collisions[2]; westColl = collisions[3]; southColl = collisions[0];
					eastVisit = b2i(eastColl); northVisit = 1; westVisit = b2i(westColl); southVisit = b2i(southColl);
					break;
				case 180:
					eastColl = collisions[2]; northColl = collisions[3]; westColl = collisions[0]; southColl = collisions[1];
					eastVisit = 1; northVisit = b2i(northColl); westVisit = b2i(westColl); southVisit = b2i(southColl);
					break;
				case 270:
					eastColl = collisions[3]; northColl = collisions[0]; westColl = collisions[1]; southColl = collisions[2];
					eastVisit = b2i(eastColl); northVisit = b2i(northColl); westVisit = b2i(westColl); southVisit = 1;
					break;
				default:
					eastColl = false; northColl = false; westColl = false; southColl = false;
					eastVisit = b2i(eastColl); northVisit = b2i(northColl); westVisit = b2i(westColl); southVisit = b2i(southColl);
					break;
			}
		}

		public void Move( float dir , Movement mov) {
			float auxDir = Mathf.Round(dir / 90) * 90;

			bool cColl = false, lColl = false, rColl = false;
			int cVisit = 0, lVisit = 0, rVisit = 0;

			switch (auxDir) {
				case 0:
					cColl = eastColl; lColl = northColl; rColl = southColl;
					cVisit = eastVisit; lVisit = northVisit; rVisit = southVisit;
					break;
				case 90:
					cColl = southColl; lColl = eastColl; rColl = westColl;
					cVisit = southVisit; lVisit = eastVisit; rVisit = westVisit;
					break;
				case 180:
					cColl = westColl; lColl = southColl; rColl = northColl;
					cVisit = westVisit; lVisit = southVisit; rVisit = northVisit;
					break;
				case 270:
					cColl = northColl; lColl = westColl; rColl = eastColl;
					cVisit = northVisit; lVisit = westVisit; rVisit = eastVisit;
					break;
			}

			int collValue = (lColl ? 1 : 0) * 1 + (cColl ? 1 : 0) * 2 + (rColl ? 1 : 0) * 4;
			int choice;
			switch (collValue) {
				case 0:
					choice = getSmaller(lVisit, cVisit, rVisit);
					switch (choice) {
						case 0:
							mov.setState(STATE.LEFT_ROTATION);
							setAngleVisited(auxDir - 90);
							break;
						case 1:
							mov.setState(STATE.MOVING);
							setAngleVisited(auxDir);
							break;
						case 2:
							mov.setState(STATE.RIGHT_ROTATION);
							setAngleVisited(auxDir + 90);
							break;
					}
					break;
				case 1:
					choice = getSmaller(cVisit, rVisit);
					switch (choice) {
						case 0:
							mov.setState(STATE.MOVING);
							setAngleVisited(auxDir);
							break;
						case 1:
							mov.setState(STATE.RIGHT_ROTATION);
							setAngleVisited(auxDir + 90);
							break;
					}
					break;
				case 2:
					choice = getSmaller(lVisit, rVisit);
					switch (choice) {
						case 0:
							mov.setState(STATE.LEFT_ROTATION);
							setAngleVisited(auxDir - 90);
							break;
						case 1:
							mov.setState(STATE.RIGHT_ROTATION);
							setAngleVisited(auxDir + 90);
							break;
					}
					break;
				case 3:
					mov.setState(STATE.RIGHT_ROTATION);
					setAngleVisited(auxDir + 90);
					break;
				case 4:
					choice = getSmaller(lVisit, cVisit);
					switch (choice) {
						case 0:
							mov.setState(STATE.LEFT_ROTATION);
							setAngleVisited(auxDir - 90);
							break;
						case 1:
							mov.setState(STATE.MOVING);
							setAngleVisited(auxDir);
							break;
					}
					break;
				case 6:
					mov.setState(STATE.LEFT_ROTATION);
					setAngleVisited(auxDir - 90);
					break;
				case 7:
					mov.setState(STATE.TOTAL_ROTATION);
					setAngleVisited(auxDir + 180);
					break;
			}
		}

		public void setAngleVisited( float dir ) {
			float auxDir = dir - Mathf.Floor(dir / 360) * 360;
			switch(auxDir) {
				case 0:
				case 360:
					eastVisit++;
					break;
				case 90:
					southVisit++;
					break;
				case 180:
					westVisit++;
					break;
				case 270:
					northVisit++;
					break;
			}
		}

		public int getSmaller( int a, int b) {
			return ( a <= b ) ? 0 : 1;
		}

		public int getSmaller(int a, int b, int c) {
			return (a <= b && a <= c) ? 0 : ( b <= c ) ? 1 : 2;
		}
	}


	private Dictionary<Vector2,Cross> crossDict;

	private void Start() {
		movement = gameObject.GetComponent<Movement>();
		sensors = gameObject.GetComponent<SensorSystem>();
		lastChoice = transform.position;
		crossDict = new Dictionary<Vector2, Cross>();
		movement.setState(STATE.IDLE);
	}

	private float pos2ten( float value ) {
		return Mathf.Round((value + 5.0f) / 10) * 10;
	}

	private float dist2LastCross() {
		Vector3 fixedPos = new Vector3(pos2ten(transform.position.x), transform.position.y, pos2ten(transform.position.z));
		Vector3 lastFixedPos = new Vector3(lastChoice.x, transform.position.y, lastChoice.z);
		return (fixedPos - lastFixedPos).magnitude;
	}

	private void Update() {
		if (movement.getState() != STATE.IDLE) {
			if (!choosing) {
				Vector3 targetPosition = new Vector3(Mathf.Round((transform.position.x + 5.0f) / 10) * 10, transform.position.y, Mathf.Round((transform.position.z + 5.0f) / 10) * 10);
				float auxDist = (targetPosition - new Vector3(transform.position.x + 5.0f, transform.position.y, transform.position.z + 5.0f)).magnitude;

				if (auxDist < 0.5f) {
					int collValue = sensors.binValue();
					Vector2 newKey = new Vector2(pos2ten(transform.position.x), pos2ten(transform.position.z));

					if (collValue != 5) {
						if (!crossDict.ContainsKey(newKey)) {
							crossDict.Add(newKey, new Cross(sensors.completeValue(), transform.rotation.eulerAngles.y));
							Instantiate(crossMark, new Vector3(pos2ten(transform.position.x) - 5, transform.position.y, pos2ten(transform.position.z) - 5), Quaternion.identity);
							if (crossDict.Count == 1) {
								crossDict[newKey].northVisit = crossDict[newKey].b2i(crossDict[newKey].northColl);
							}
						}

						crossDict[newKey].Move(transform.rotation.eulerAngles.y, movement);

						choosing = true;

						lastChoice = transform.position;

						if ( movement.getState() == STATE.MOVING && sensors.centerCollider.GetComponent<Sensor>().colliding) {
							movement.setState(STATE.LEFT_ROTATION);
						}
					}
				}

			} else {
				if (movement.getFixingFinished()) {
					movement.setState(STATE.MOVING);
					if (dist2LastCross() > 9.0f) {
						choosing = false;
					}

				}
			}
		}
	}
}