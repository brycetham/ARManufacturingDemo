using UnityEngine;
using UnityEngine.UI;

namespace Vuforia
{

    public class QRTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
    {

        private TrackableBehaviour mTrackableBehaviour;
        public int step;

		public GameObject eventSystem;

		public AudioSource instructions;

        void Start()
        {
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
				newStatus == TrackableBehaviour.Status.TRACKED)
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
			GameObject es = GameObject.Find ("EventSystem");

			if (es.GetComponent<UserInterfaceScript> ().gameStep != 0) {

				// print (GameObject.Find ("EventSystem").GetComponent<UserInterfaceScript> ().gameStep);
				
            	Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);

            	// Enable rendering:
				foreach (Renderer component in rendererComponents) {
					component.enabled = true;
				}

				es.GetComponent<UserInterfaceScript> ().gameStep = step;
				es.GetComponent<UserInterfaceScript> ().Tracked ();

				if (!instructions.isPlaying) {
					instructions.Play ();
				}
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

            //Debug.Log("QR Code Lost!");
            //active = false;
        }

    }
}
