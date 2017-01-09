using UnityEngine;
using System.Collections;

using UnityEngine.UI;

namespace Vuforia
{
	public class UserInterfaceScript : MonoBehaviour
	{

		public float startTime;
		private float elapsedTime;
		public bool pause;
		public int gameStep;
		public string gameText;

		// Use this for initialization
		void Start ()
		{
			startTime = Time.time;
			pause = false;
			gameStep = 1;

		}
		
		// Update is called once per frame
		void Update ()
		{
			// StepText
			if (gameStep > 0 && gameStep < 4) {
				GameObject.Find ("StepText").GetComponent<Text> ().text = "Step: " + gameStep.ToString () + "/3";
			}

			// TimeText
			if (!pause) {
				elapsedTime = Time.time - startTime;
			}
			GameObject.Find ("TimeText").GetComponent<Text> ().text = "Time: " + Mathf.Floor (elapsedTime / 60).ToString ("00") + ":" + 
				Mathf.Floor (elapsedTime % 60).ToString ("00");

			// GameText
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
			GameObject.Find ("GameText").GetComponent<Text>().text = gameText;

		}
	}
}
