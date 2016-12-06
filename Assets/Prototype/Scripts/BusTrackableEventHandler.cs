/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class BusTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;

        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        void Update()
        {
            GameObject qrTarget = GameObject.Find("QRTarget");
            bool active = qrTarget.GetComponent<QRTrackableEventHandler>().active;
            if (!active)
            {
                Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
                Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

                // Disable rendering:
                foreach (Renderer component in rendererComponents)
                {
                    component.enabled = false;
                }

                // Disable colliders:
                foreach (Collider component in colliderComponents)
                {
                    component.enabled = false;
                }

                Debug.Log("QR Code was lost so object is untracked.");
            } 

        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
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

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
            GameObject qrTarget = GameObject.Find("QRTarget");
            bool active = qrTarget.GetComponent<QRTrackableEventHandler>().active;
            if (active)
            {
                Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
                Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

                // Enable rendering:
                foreach (Renderer component in rendererComponents)
                {
                    component.enabled = true;
                }

                // Enable colliders:
                foreach (Collider component in colliderComponents)
                {
                    component.enabled = true;
                }

                if (mTrackableBehaviour.TrackableName == "buspart1")
                {
                    Debug.Log("Bus Part 1 Found!");
                }
                else if (mTrackableBehaviour.TrackableName == "buspart2")
                {
                    Debug.Log("Bus Part 2 Found!");
                }
            }
            else
            {
                Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
                Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

                // Disable rendering:
                foreach (Renderer component in rendererComponents)
                {
                    component.enabled = false;
                }

                // Disable colliders:
                foreach (Collider component in colliderComponents)
                {
                    component.enabled = false;
                }

                Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            }
        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        #endregion // PRIVATE_METHODS
    }
}
