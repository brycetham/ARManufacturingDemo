using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expand : MonoBehaviour {

	private float expandTime;
	private bool isExpanded;
	private Hashtable origins;

	// Use this for initialization
	void Start () {

		// Initialize values
		expandTime = 0;
		isExpanded = false;

		// Initialize hash table
		origins = new Hashtable();
		foreach (Transform child in transform) {
			origins [child] = child.position;
		}
	}

	// Update is called once per frame
	void Update () {

		// Activate expansion
		if (Input.GetKeyDown ("e") && expandTime < 0) {
			expandTime = 1;
			isExpanded = !isExpanded;
		}

		// Perform expansion
		if (expandTime > 0) {
			foreach (Transform child in transform) {
				child.position *= isExpanded ? 1.05f : 0.95f;
			}
		}

		// Fix position
		if (expandTime < 0 && !isExpanded) {
			foreach (Transform child in transform) {
				child.position = (Vector3) origins [child];
			}
		}

		// Update timer
		expandTime -= Time.deltaTime;
	}
}
