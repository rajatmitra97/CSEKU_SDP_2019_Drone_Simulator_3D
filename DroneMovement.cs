﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;

public class DroneMovement : MonoBehaviour {

	Rigidbody ourDrone;

	// Use this for initialization
	void Awake () {
		ourDrone = GetComponent<Rigidbody> ();
		droneSound = gameObject.transform.Find ("drone_sound").GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		MovementUpDown ();
		MovementForward ();
		Rotation ();
		ClampingSpeedValues ();
		Swerve ();
		DroneSound ();
		ourDrone.AddRelativeForce (Vector3.up * upForce);
		ourDrone.rotation = Quaternion.Euler (new Vector3 (tiltAmountForward, currentYRotation, -tiltAmountSideways));

	}
	public float upForce;
	void MovementUpDown(){
		
		if ((Mathf.Abs (Input.GetAxis ("Vertical")) > 0.2f || Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.2f)) {
			
			if (Input.GetKey (KeyCode.I) || Input.GetKey (KeyCode.K) ) {

				ourDrone.velocity = ourDrone.velocity;

			} 
			if(!Input.GetKey (KeyCode.I) && !Input.GetKey (KeyCode.K) && !Input.GetKey (KeyCode.J) && !Input.GetKey (KeyCode.L)){
				ourDrone.velocity = new Vector3 (ourDrone.velocity.x, Mathf.Lerp (ourDrone.velocity.y, 0, Time.deltaTime * 5), ourDrone.velocity.z);
				upForce = 281;
			}
			if(!Input.GetKey (KeyCode.I) && !Input.GetKey (KeyCode.K) && (Input.GetKey (KeyCode.J) || Input.GetKey (KeyCode.L))){
				ourDrone.velocity = new Vector3 (ourDrone.velocity.x, Mathf.Lerp (ourDrone.velocity.y, 0, Time.deltaTime * 5), ourDrone.velocity.z);
				upForce = 110;
			}
			if(Input.GetKey (KeyCode.J) || Input.GetKey (KeyCode.L)){
				
				upForce = 410;
			}


		}
		if (Mathf.Abs (Input.GetAxis ("Vertical")) < 0.2f && Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.2f) {

			upForce = 135;
		}
		if (Input.GetKey (KeyCode.I)) {
		
			upForce = 450;
			if (Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.2f) {
			
				upForce = 500;
			}
		
		} 
		else if (Input.GetKey (KeyCode.K)) {
		
			upForce = -200;
		} 
		else if(!Input.GetKey (KeyCode.I) && !Input.GetKey (KeyCode.K) && (Mathf.Abs (Input.GetAxis ("Vertical")) < 0.2f && Mathf.Abs (Input.GetAxis ("Horizontal")) < 0.2f)){
			upForce = 98.1f;
	}
	}

	private float movementForwardSpeed = 500.0f;
	private float tiltAmountForward = 0;
	private float tiltVElocityForward;
	void MovementForward(){
	
		if(Input.GetAxis ("Vertical") != 0){

			ourDrone.AddRelativeForce (Vector3.forward * Input.GetAxis ("Vertical") * movementForwardSpeed);
			tiltAmountForward = Mathf.SmoothDamp (tiltAmountForward,20 * Input.GetAxis ("Vertical"),ref tiltVElocityForward,0.1f);

		}
	
	
	}
	private float wantedYRotation;
	public float currentYRotation;
	private float rotateAmountByKeys = 2.5f;
	private float rotationYVelocity;

	void Rotation(){

		if (Input.GetKey (KeyCode.J)) {
			wantedYRotation -= rotateAmountByKeys;	
		}

		if (Input.GetKey (KeyCode.L)) {
			wantedYRotation += rotateAmountByKeys;
		}

		currentYRotation = Mathf.SmoothDamp (currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);
	
	}

	private Vector3 velocityToSmoothDampToZero;
	void ClampingSpeedValues (){
		
		if (Mathf.Abs (Input.GetAxis ("Vertical")) > 0.2f && Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.2f) {
		
			ourDrone.velocity = Vector3.ClampMagnitude (ourDrone.velocity, Mathf.Lerp (ourDrone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
		}
		if (Mathf.Abs (Input.GetAxis ("Vertical")) > 0.2f && Mathf.Abs (Input.GetAxis ("Horizontal")) < 0.2f) {

			ourDrone.velocity = Vector3.ClampMagnitude (ourDrone.velocity, Mathf.Lerp (ourDrone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
		}
		if (Mathf.Abs (Input.GetAxis ("Vertical")) < 0.2f && Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.2f) {

			ourDrone.velocity = Vector3.ClampMagnitude (ourDrone.velocity, Mathf.Lerp (ourDrone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
		}
		if (Mathf.Abs (Input.GetAxis ("Vertical")) < 0.2f && Mathf.Abs (Input.GetAxis ("Horizontal")) < 0.2f) {

			ourDrone.velocity = Vector3.SmoothDamp (ourDrone.velocity, Vector3.zero, ref velocityToSmoothDampToZero, 0.95f);
		}



	
	}

	private float sideMovementAmount = 300.0f;
	private float tiltAmountSideways;
	private float tiltAmountVelocity;
	void Swerve(){

		if (Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.2f) {
		
			ourDrone.AddRelativeForce (Vector3.right * Input.GetAxis ("Horizontal") * sideMovementAmount);
			tiltAmountSideways = Mathf.SmoothDamp (tiltAmountSideways, 20 * Input.GetAxis ("Horizontal"), ref tiltAmountVelocity, 0.1f);
		
		} else {
		
			tiltAmountSideways = Mathf.SmoothDamp (tiltAmountSideways, 0, ref tiltAmountVelocity, 0.1f);
		
		
		}
	
	}
	private AudioSource droneSound;
	void DroneSound(){

		droneSound.pitch = 1 + (ourDrone.velocity.magnitude / 100)*.5f;
	
	
	}




}







