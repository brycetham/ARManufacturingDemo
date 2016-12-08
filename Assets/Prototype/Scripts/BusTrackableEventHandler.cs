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
            completeText = completeText.Replace("<br>", "\n");
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
            GameObject qrTarget = GameObject.Find("QRTarget");
            int gameStep = qrTarget.GetComponent<QRTrackableEventHandler>().gameStep;
            if (gameStep == thisStep)
            {
                Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);

                // Enable rendering:
                foreach (Renderer component in rendererComponents)
                {
                    component.enabled = true;
                }

                StartCoroutine(Complete(qrTarget));
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

        IEnumerator Complete(GameObject qrTarget)
        {
            qrTarget.GetComponent<QRTrackableEventHandler>().gameStep = 0;
            qrTarget.GetComponent<QRTrackableEventHandler>().gameText = completeText;
            yield return new WaitForSeconds(5);
            qrTarget.GetComponent<QRTrackableEventHandler>().gameStep = thisStep + 1;

        }

    }
}
