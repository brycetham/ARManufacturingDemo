using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vuforia {

	public class Expand : MonoBehaviour {

		private float expandTime;
		private bool isExpanded;
		private Hashtable origins;

		private Hashtable colors;

		// Use this for initialization
		void Start () {

			// Initialize values
			expandTime = 0;
			isExpanded = false;

			// Initialize hash tables
			origins = new Hashtable();
			foreach (Transform child in transform) {
				origins [child] = child.position;
			}
			colors = new Hashtable();
			foreach (Transform child in transform) {
				if (child.name.Substring (0, 1).Equals("P")) {
					foreach (Transform part in child) {
						if (!part.name.Equals ("Label")) {
							colors [part] = part.GetComponent<MeshRenderer> ().material.color;
						}
					}
				}
			}
		}

		// Update is called once per frame
		void Update () {

			// Activate expansion
			if (Input.GetKeyDown ("e") || Input.GetKeyDown(KeyCode.Escape) /*|| Input.GetButtonDown("BUTTON_TYPE_MAGNET")*/ ) {
				expand ();
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

			// Invoke blinking
			foreach (Transform child in transform) {
				if (child.name.StartsWith ("P")) {
					blink (child);
				}
			}
		}

		// Activate expansion
		public void expand() {
			if (expandTime < 0) {
				expandTime = 1;
				isExpanded = !isExpanded;
			}
		}

		// Activate blink
		public void blink(Transform child) {
			foreach (Transform part in child) {
				if (!part.name.Equals ("Label") && !part.name.Equals ("Pointer")) {
//					if (GameObject.Find ("EventSystem").GetComponent <UserInterfaceScript > ().annotateFlag) {
						Color flashColor = Color.Lerp (Color.white, (Color)colors [part], Mathf.PingPong (Time.time, 1));
						part.GetComponent<MeshRenderer> ().material.color = flashColor;
//					} else {
//						part.GetComponent<MeshRenderer> ().material.color = (Color)colors [part];
//					}
				}
			}
		}

	}
}
