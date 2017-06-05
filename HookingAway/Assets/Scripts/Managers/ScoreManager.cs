using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText;

	public float scoreCount;
	public float hiScoreCount;

	public int credits;

	public float pointsPerSecond;

	public bool scoreIncreasing;

	void Start () {
		scoreText = GameObject.Find ("ScoreText").GetComponent<Text> ();

		if (PlayerPrefs.GetFloat ("HiScoreValue") != null) {
			hiScoreCount = PlayerPrefs.GetFloat ("HiScoreValue");
		}
	}

	void Update () {

		if (scoreIncreasing) 
		{
			
			scoreCount += pointsPerSecond * Time.deltaTime;

			if (scoreCount > hiScoreCount) {
				scoreText.text = "New HighScore: " + Mathf.Round (scoreCount);
			}

			else {
				scoreText.text = "Score: " + Mathf.Round (scoreCount);
			}

		}

		if (scoreCount > hiScoreCount) 
		{
			hiScoreCount = scoreCount;
			PlayerPrefs.SetFloat ("HiScoreValue", hiScoreCount);
		}
	}

	public void AddScore (int pointsToAdd)
	{
		scoreCount += pointsToAdd;
		credits += 1;
	}
}
