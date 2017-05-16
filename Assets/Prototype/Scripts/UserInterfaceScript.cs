using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

namespace Vuforia
{
	public class UserInterfaceScript : MonoBehaviour
	{
		public float timer;
		private float stepStartTime;

		public bool pause;
		public int gameStep = 0;
		public string gameText;

		private Dictionary<int, string> steps;
		public bool transitionFlag;

		public bool annotateFlag = false;

		public AudioSource background;
		public AudioSource tracked;

		// Use this for initialization
		void Start ()
		{
			steps = new Dictionary<int, string> () {
				{ 0, "Ready?" },
				{ 1, "Begin with the base frame." },
				{ 2, "Add the front mount and front axle." },
				{ 3, "Add the middle mount and rear axle." },
				{ 4, "Add the rear mount." },
				{ 5, "Add the frame top." },
				{ 6, "Add the rear lights" },
				{ 7, "Add the rear components." },
				{ 8, "Add the axle covers." },
				{ 9, "Add the interior base." },
				{ 10, "Add the rear fixtures." },
				{ 11, "Add the seats." },
				{ 12, "Add the rear view mirrors." },
				{ 13, "Add the wall foundations." },
				{ 14, "Add the rear wall." },
				{ 15, "Add the rear side windows." },
				{ 16, "Add the front side windows." },
				{ 17, "Add windshield." },
				{ 18, "Add the rear side windows." },
				{ 19, "Add the hubcap." },
				{ 20, "Add the front fixtures." },
				{ 21, "Add the top fixture base." },
				{ 22, "Add the top fixtures." },
				{ 23, "Add the wheels." }
			};

			background.Play ();

			// For testing purposes only.
			// this.BeginGame ();
		}
		
		// Update is called once per frame
		void Update ()
		{
			// Resize background plane
//			GameObject.Find("BackgroundPlane").transform.position = new Vector3(0, 0, 1500);

			// StepText
			if (gameStep > 0 && gameStep <= steps.Count) {
				pause = false;
				GameObject.Find ("StepText").GetComponent<Text> ().text = "Step: " + gameStep.ToString () + "/" + (steps.Count - 1);
			} else {
				pause = true;
			}

			// TimeText
			if (!pause) {
				timer += Time.deltaTime;
				GameObject.Find ("TimeText").GetComponent<Text> ().text = "Time: " +
				Mathf.Floor (timer / 60).ToString ("00") + ":" +
				Mathf.Floor (timer % 60).ToString ("00");
			}

			GameObject.Find ("GameText").GetComponent<Text> ().text = "";
			GameObject.Find ("ListText").GetComponent<Text> ().text = steps [gameStep];

//			if (GetComponent<CardboardMagnetSensor>().clicked()) {
//				GameObject.Find ("ListText").GetComponent<Text> ().text = "TRIGGERED!";
//			}

			// GameText & ListText
			if (transitionFlag && timer > stepStartTime + 3) {
				GameObject.Find ("GameText").GetComponent<Text> ().text = "";
				GameObject.Find ("ListText").GetComponent<Text> ().text = steps [gameStep];
				transitionFlag = false;
			}
								
		}

		public void BeginGame ()
		{
			StartCoroutine (NextStep (1, "Ready?"));
		}

		public IEnumerator NextStep (int newStep, string completeText)
		{

//			float elaspedTime = timer - stepStartTime;
//			GameObject.Find ("GameText").GetComponent<Text> ().text = completeText + "\nTime: +" +
//			Mathf.Floor (elaspedTime / 60).ToString ("00") + ":" +
//			Mathf.Floor (elaspedTime % 60).ToString ("00");
			gameStep = 0; // temporarily pause game
			yield return new WaitForSeconds (3);
			gameStep = newStep;
			stepStartTime = timer;

//			gameText = steps [gameStep - 1];
//			GameObject.Find ("GameText").GetComponent<Text> ().text = gameText;
			transitionFlag = true;
		}

		public void Annotate()
		{
			annotateFlag = !annotateFlag;
		}

		public void Tracked()
		{
			tracked.Play ();
		}
			
	}
}
