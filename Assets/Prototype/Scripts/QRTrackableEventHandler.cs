using UnityEngine;
using UnityEngine.UI;

namespace Vuforia
{

    public class QRTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
    {

        private TrackableBehaviour mTrackableBehaviour;
        public int gameStep;
        public string gameText;

        void Start()
        {
            gameStep = 1;
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        void Update()
        {
            switch (gameStep)
            {
                case 1:
                    gameText = "Step 1/3\n<size=30>1st Step</size>\nBuild the base.";
                    break;
                case 2:
                    gameText = "Step 2/3\n<size=30>Next Step</size>\nAdd the mid-frame.";
                    break;
                case 3:
                    gameText = "Step 3/3\n<size=30>Last Step</size>\nAdd the ceiling.";
                    break;
                case 4:
                    gameText = "Level: Bus\n<size=30>Complete</size>\nCongratulations";
                    break;
            }

            GameObject.Find("QRText").GetComponent<TextMesh>().text = gameText;

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
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
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

            Debug.Log("QR Code Lost!");
            //active = false;

        }

    }
}
