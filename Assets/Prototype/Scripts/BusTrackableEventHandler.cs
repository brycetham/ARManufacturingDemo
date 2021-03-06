﻿using UnityEngine;
using System.Collections;

using UnityEngine.UI;

namespace Vuforia
{

	public class BusTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
	{

		private TrackableBehaviour mTrackableBehaviour;
		public int thisStep;
		public string completeText;

		void Start ()
		{
			mTrackableBehaviour = GetComponent<TrackableBehaviour> ();
			if (mTrackableBehaviour) {
				mTrackableBehaviour.RegisterTrackableEventHandler (this);
			}
		}

		public void OnTrackableStateChanged (
			TrackableBehaviour.Status previousStatus,
			TrackableBehaviour.Status newStatus)
		{
			if (newStatus == TrackableBehaviour.Status.DETECTED ||
			             newStatus == TrackableBehaviour.Status.TRACKED ||
			             newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
				OnTrackingFound ();
			} else {
				OnTrackingLost ();
			}
		}

		private void OnTrackingFound ()
		{
			if (GameObject.Find ("EventSystem").GetComponent<UserInterfaceScript> ().gameStep == thisStep) {
				Renderer[] rendererComponents = GetComponentsInChildren<Renderer> (true);

				// Enable rendering:
				foreach (Renderer component in rendererComponents) {
					component.enabled = true;
				}

				StartCoroutine (GameObject.Find ("EventSystem").GetComponent<UserInterfaceScript> ().NextStep (thisStep + 1, completeText));
			}

		}

		private void OnTrackingLost ()
		{
			Renderer[] rendererComponents = GetComponentsInChildren<Renderer> (true);

			// Disable rendering:
			foreach (Renderer component in rendererComponents) {
				component.enabled = false;
			}

			Debug.Log ("Trackable " + mTrackableBehaviour.TrackableName + " lost");
		}

		// For testing purposes only.
		void Update ()
		{
			if (GameObject.Find ("EventSystem").GetComponent<UserInterfaceScript> ().gameStep == thisStep) {
				if (Input.GetKeyDown ("space"))
					StartCoroutine (GameObject.Find ("EventSystem").GetComponent<UserInterfaceScript> ().NextStep (thisStep + 1, completeText));
			}

		}

	}
}
