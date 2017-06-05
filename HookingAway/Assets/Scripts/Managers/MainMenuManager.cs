using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	private Text progressResetText;
	private Text sfxToggle;
	private Text musicToggle;
	private GameObject optionsScreen;


	void Start () {
		optionsScreen = GameObject.Find ("OptionsScreen");
		progressResetText = GameObject.Find ("ProgressResetText").GetComponent<Text> ();
		sfxToggle = GameObject.Find ("SFXToggleText").GetComponent<Text> ();
		musicToggle = GameObject.Find ("MusicToggleText").GetComponent<Text> ();

		optionsScreen.SetActive (false);

		progressResetText.canvasRenderer.SetAlpha (0f);

		if (PlayerPrefs.GetInt ("SFXEnabled") == 1)
			sfxToggle.text = "SFX:OFF";
		else if (PlayerPrefs.GetInt ("SFXEnabled") == 0 || !PlayerPrefs.HasKey ("SFXEnabled"))
				sfxToggle.text = "SFX:ON";

		if (PlayerPrefs.GetInt ("MusicEnabled") == 1)
			musicToggle.text = "MUSIC:OFF";
		else if (PlayerPrefs.GetInt ("MusicEnabled") == 0 || !PlayerPrefs.HasKey ("SFXEnabled")) 
			musicToggle.text = "MUSIC:ON";
	}
		

	public void ResetProgress () {
		StartCoroutine ("ShowProgressResetText");
		PlayerPrefs.DeleteAll ();
	}


	public void LoadEndlessMode () {
		SceneManager.LoadScene("EndlessModeScene", LoadSceneMode.Single);
	}


	public IEnumerator ShowProgressResetText () {
		progressResetText.canvasRenderer.SetAlpha (255f);
		yield return new WaitForSeconds (3f);
		progressResetText.canvasRenderer.SetAlpha (0f);
	}


	public void MusicToggle() {

		int savedMusicValue = PlayerPrefs.GetInt("MusicEnabled");

		if (savedMusicValue == 0) {
			musicToggle.text = "MUSIC:OFF";
			PlayerPrefs.SetInt ("MusicEnabled", 1);
		} else if (savedMusicValue == 1){
			musicToggle.text = "MUSIC:ON";
			PlayerPrefs.SetInt ("MusicEnabled", 0);
		}
	}

	public void SfxToggle() {

		int savedSFXValue = PlayerPrefs.GetInt("SFXEnabled");

		if (savedSFXValue == 0){
			sfxToggle.text = "SFX:OFF";
			PlayerPrefs.SetInt ("SFXEnabled", 1);
		} else if (savedSFXValue == 1) {
			sfxToggle.text = "SFX:ON";
			PlayerPrefs.SetInt ("SFXEnabled", 0);
		}
	}
}
