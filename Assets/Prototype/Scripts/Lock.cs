using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour {

	public GameObject camera;

	// Use this for initialization
	void Start () {
		camera = GameObject.Find ("ARCamera");
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = camera.transform.rotation;
	}
}
