using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	MapHandler mh;
	public GameObject pauseMenu;

	// Use this for initialization
	void Start () {
		mh = FindObjectOfType<MapHandler> ();
		if (mh.pause) {
			pauseMenu.SetActive (true);
		} else {
			pauseMenu.SetActive (false);
		}
	}
	
	public void ExitButton(){
		//Application.Quit ();
		SceneManager.LoadScene("Menu",LoadSceneMode.Single);
        // Ritornare alla home del gioco
	}

	public void TutorialButton(){
	}

	public void ResumeButton(){
		mh.SwitchPause ();
	}
}
