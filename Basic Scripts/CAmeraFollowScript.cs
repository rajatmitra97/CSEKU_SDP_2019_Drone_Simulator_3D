using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Utility;

public class CAmeraFollowScript : MonoBehaviour {

	private Transform ourDrone;
	// Use this for initialization
	void Start () {
		ourDrone = GameObject.FindGameObjectWithTag ("Player").transform;
		
	}
	private Vector3 velocityCameraFollow;
	public Vector3 behindPosition = new Vector3 (0, 2, -4);
	public float angle;
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = Vector3.SmoothDamp (transform.position, ourDrone.transform.TransformPoint (behindPosition) , ref velocityCameraFollow, 0.1f);
		transform.rotation = Quaternion.Euler (new Vector3 (angle, ourDrone.GetComponent <DroneMovement> ().currentYRotation, 0));

	}
}





























