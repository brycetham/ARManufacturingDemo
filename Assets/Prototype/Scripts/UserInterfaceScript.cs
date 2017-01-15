using UnityEngine;
using System.Collections;

using UnityEngine.UI;

namespace Vuforia
{
	public class UserInterfaceScript : MonoBehaviour
	{
		public float timer;
		private float stepStartTime;

		public bool pause;
		public int gameStep;
		public string gameText;

		private string[] steps;
		public bool transitionFlag;

		// Use this for initialization
		void Start ()
		{
			steps = new string[] {"Step 1: Build the base.\n", 
				"Step 2: Add the mid-frame.\n", 
				"Step 3: Add the ceiling.\n", 
				"Complete: Bus."
			};

		}
		
		// Update is called once per frame
		void Update ()
		{
			// StepText
			if (gameStep > 0 && gameStep < 4) {
				pause = false;
				GameObject.Find ("StepText").GetComponent<Text> ().text = "Step: " + gameStep.ToString () + "/3";
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

			// GameText & ListText
			if (transitionFlag && timer > stepStartTime + 3) {
				GameObject.Find ("GameText").GetComponent<Text> ().text = "";
				GameObject.Find ("ListText").GetComponent<Text> ().text += steps [gameStep - 1];
				transitionFlag = false;
			}
								
		}

		public void BeginGame ()
		{
			StartCoroutine (NextStep (1, "Ready?"));
		}

		public IEnumerator NextStep (int newStep, string completeText)
		{

			float elaspedTime = timer - stepStartTime;
			GameObject.Find ("GameText").GetComponent<Text> ().text = completeText + "\nTime: +" +
			Mathf.Floor (elaspedTime / 60).ToString ("00") + ":" +
			Mathf.Floor (elaspedTime % 60).ToString ("00");
			gameStep = 0; // temporarily pause game
			yield return new WaitForSeconds (5);
			gameStep = newStep;
			stepStartTime = timer;

			gameText = steps [gameStep - 1];
			GameObject.Find ("GameText").GetComponent<Text> ().text = gameText;
			transitionFlag = true;
		}
			
	}
}
