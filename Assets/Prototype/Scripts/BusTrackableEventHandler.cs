using UnityEngine;
using System.Collections;

namespace Vuforia
{

    public class BusTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
    {

        private TrackableBehaviour mTrackableBehaviour;
        public int thisStep;
        public string completeText;

        void Start()
        {
//            completeText = completeText.Replace("<br>", "\n");
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        private void OnTrackingFound()
        {
			if (GameObject.Find ("EventSystem").GetComponent<UserInterfaceScript> ().gameStep == thisStep)
            {
                Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);

                // Enable rendering:
                foreach (Renderer component in rendererComponents)
                {
                    component.enabled = true;
                }

                StartCoroutine(Complete());
            }

        }

        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        IEnumerator Complete()
        {
			GameObject.Find("EventSystem").GetComponent<UserInterfaceScript>().pause = true;
			GameObject.Find("EventSystem").GetComponent<UserInterfaceScript>().gameStep = 0;
			GameObject.Find("EventSystem").GetComponent<UserInterfaceScript>().gameText = completeText;
            yield return new WaitForSeconds(5);
			GameObject.Find("EventSystem").GetComponent<UserInterfaceScript>().gameStep = thisStep + 1;
			GameObject.Find("EventSystem").GetComponent<UserInterfaceScript>().pause = false;
			GameObject.Find("EventSystem").GetComponent<UserInterfaceScript>().startTime += 5;

        }

    }
}
