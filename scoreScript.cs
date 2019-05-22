using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class scoreScript : MonoBehaviour {

	public static int scoreValue = 0;
	Text score;
	// Use this for initialization
	//Text gameover;
	//public static int trigger1 = 0;
	void Start () {
		
		score = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		score.text = "SCORE : " + scoreValue;
		/*if (trigger1 == 1) {

			score.text = "GAME OVER";
			score.
		
		}
		if (scoreValue == 120) {
		
			score.text = "YOU WIN";
		}*/
	
	}
}
