using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour {

	public int step = 1;
	public int maxSteps;

	private Vector3 startMarker;
	private Vector3 endMarker;
	private float startTime;

	// Use this for initialization
	void Start () {
		startTime = -1;
		startMarker = transform.position;
		endMarker = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("left")) {
			Left ();
		}

		if (Input.GetKeyDown ("right")) {
			Right ();
		}

		float distCovered = (Time.time - startTime) * 1000;
		float fracJourney = distCovered / 500;
		transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
	}

	public void Left() {
		if (step > 1 && Time.time > startTime + 1) {
			step--;
			startMarker = transform.position;
			endMarker = transform.position - new Vector3 (0, 0, 500);
			startTime = Time.time;
		}
	}

	public void Right() {
		if (step < maxSteps && Time.time > startTime + 1) {
			step++;
			startMarker = transform.position;
			endMarker = transform.position + new Vector3 (0, 0, 500);
			startTime = Time.time;
		}
	}
}
