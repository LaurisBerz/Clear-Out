using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public Transform platformGenerator;
	private Vector3 platformStartPoint;
	private PlayerController thePlayer;
	private Rigidbody2D playerPhysics;
	private Vector3 playerStartPoint;

	private ObjectDestructor[] platformList;

	//Score and UI
	public ScoreManager theScoreManager;
	public GameObject deathScreen;
	public Text deathScreenScoreText;
	public Text pauseScreenScoreText;
	public GameObject pauseBtn;
	public GameObject scoreText;
	public GameObject pauseScreen;

	//Audio
	public AudioSource soundtrack;
	public AudioSource[] sfxSounds;

	public Text sfxToggle;
	public Text musicToggle;

	public float highscore;
	public int collectedCreditsThisRun;
	public int totalCredits;


	void Start ()
	{
		thePlayer = GameObject.Find ("Player").GetComponent<PlayerController> ();
		playerPhysics = GameObject.Find ("Player").GetComponent<Rigidbody2D> ();
		theScoreManager = FindObjectOfType<ScoreManager> ();
		Time.timeScale = 1f;

		if (PlayerPrefs.GetInt ("SFXEnabled") == 1) {
			for (int i = 0; i < sfxSounds.Length; i++) {
				sfxSounds [i].mute = true;
				sfxToggle.text = "SFX:OFF";
			}
		} else if (PlayerPrefs.GetInt ("SFXEnabled") == 0 || !PlayerPrefs.HasKey ("SFXEnabled")){
			for (int i = 0; i < sfxSounds.Length; i++) {
				sfxSounds [i].mute = false;
				sfxToggle.text = "SFX:ON";
			}
		}
			
		if (PlayerPrefs.GetInt ("MusicEnabled") == 1) {
			soundtrack.mute = true;
			musicToggle.text = "MUSIC:OFF";
		} else if (PlayerPrefs.GetInt ("MusicEnabled") == 0 || !PlayerPrefs.HasKey ("SFXEnabled")) {
			soundtrack.mute = false;
			musicToggle.text = "MUSIC:ON";
		}

		platformStartPoint = platformGenerator.position;
		playerStartPoint = thePlayer.transform.position;

		deathScreen.SetActive (false);
		pauseScreen.SetActive (false);
	}


	public void RestartGame ()
	{
		playerPhysics.velocity = new Vector2 (0, 0);
		thePlayer.gameObject.SetActive (false);
		platformList = FindObjectsOfType<ObjectDestructor> ();
		for (int i = 0; i < platformList.Length; i++) {
			platformList [i].gameObject.SetActive (false);
		}

		thePlayer.transform.position = playerStartPoint;
		platformGenerator.position = platformStartPoint;
		thePlayer.gameObject.SetActive (true);
		scoreText.SetActive (true);

		theScoreManager.scoreCount = 0;
		theScoreManager.scoreIncreasing = true;
	}


	public void PauseGame () 
	{
		Time.timeScale = 0f;
		scoreText.SetActive (false);
		pauseBtn.SetActive (false);

		pauseScreen.SetActive (true);
		pauseScreenScoreText.text = "Score: " + Mathf.Round (theScoreManager.scoreCount);
	}


	public void ResumeGame () 
	{
		Time.timeScale = 1f;
		scoreText.SetActive (true);
		pauseBtn.SetActive (true);
		pauseScreen.SetActive (false);
	}
		

	public void ToggleDeathMenu ()
	{
		collectedCreditsThisRun = theScoreManager.credits;
		//totalCredits = PlayerPrefs.GetInt ("TotalCredits") + collectedCreditsThisRun;
		PlayerPrefs.SetInt ("TotalCredits", PlayerPrefs.GetInt ("TotalCredits") + collectedCreditsThisRun); 
		totalCredits = PlayerPrefs.GetInt ("TotalCredits");
		StartCoroutine ("ToggleDeathMenuCo");
	}


	public IEnumerator ToggleDeathMenuCo () 
	{
		theScoreManager.scoreIncreasing = false;
		thePlayer.gameObject.SetActive (false);

		yield return new WaitForSeconds (0.6f);

		scoreText.SetActive (false);
		deathScreen.SetActive (true);

		highscore = Mathf.Round (PlayerPrefs.GetFloat ("HiScoreValue"));

		if (PlayerPrefs.GetFloat ("HiScoreValue") > theScoreManager.scoreCount) 
			deathScreenScoreText.text = "Score: " + Mathf.Round (theScoreManager.scoreCount);
		else if (Mathf.Round (PlayerPrefs.GetFloat ("HiScoreValue")) == Mathf.Round (theScoreManager.scoreCount))	
			deathScreenScoreText.text = "New high score: " + Mathf.Round (theScoreManager.scoreCount);
	}


	public void MusicToggle() {

		int savedMusicValue = PlayerPrefs.GetInt("MusicEnabled");

		if (savedMusicValue == 0) {
			soundtrack.mute = true;
			musicToggle.text = "MUSIC:OFF";
			PlayerPrefs.SetInt ("MusicEnabled", 1);
		} else if (savedMusicValue == 1){
			soundtrack.mute = false;
			musicToggle.text = "MUSIC:ON";
			PlayerPrefs.SetInt ("MusicEnabled", 0);
		}
	}


	public void SfxToggle() {

		int savedSFXValue = PlayerPrefs.GetInt("SFXEnabled");

		if (savedSFXValue == 0){
			for (int i = 0; i < sfxSounds.Length; i++) {
				sfxSounds [i].mute = true;
				sfxToggle.text = "SFX:OFF";
			}
			PlayerPrefs.SetInt ("SFXEnabled", 1);

		} else if (savedSFXValue == 1) {
			for (int i = 0; i < sfxSounds.Length; i++) {
				sfxSounds [i].mute = false;
				sfxToggle.text = "SFX:ON";
			}
			PlayerPrefs.SetInt ("SFXEnabled", 0);
		}
	}


	public void LoadMainMenu () {
		SceneManager.LoadScene("MainMenuScene");
	}
}
