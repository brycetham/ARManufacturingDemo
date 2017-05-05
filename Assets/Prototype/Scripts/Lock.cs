using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vuforia {
	public class Lock : MonoBehaviour {

		public GameObject camera;
		public GameObject pointer;

		// Use this for initialization
		void Start () {
			camera = GameObject.Find ("ARCamera");
		}
		
		// Update is called once per frame
		void Update () {

			// Lock rotation
			transform.rotation = camera.transform.rotation;

			// Turn on/off mesh renderer
			if (GameObject.Find ("EventSystem").GetComponent<UserInterfaceScript> ().annotateFlag) {
				foreach (Transform child in transform) {
					child.gameObject.SetActive (true);
				}
				pointer.SetActive (true);
			} else {
				foreach (Transform child in transform) {
					child.gameObject.SetActive (false);
				}
				pointer.SetActive (false);
			}

		}
	}
}
