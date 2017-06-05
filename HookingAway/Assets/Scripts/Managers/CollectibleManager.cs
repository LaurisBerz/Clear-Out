using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour {

	public int scoreToGive;

	private ScoreManager theScoreManager;

	void Start ()
	{
		theScoreManager = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();
	}

	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) 
	{
		if (other.gameObject.name == "Player")
		{
		//	theScoreManager.AddScore(scoreToGive);
		//	gameObject.SetActive (false);
		}
	}
}
