using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	MapHandler mh;
	public GameObject pauseMenu;

    // Aggiunte Marco
    public GameObject tutorial_menu;
    public GameObject general_info_background;
    public GameObject general_info_button;
    public GameObject character_background;
    public GameObject movement_background;
    public GameObject attack_background;
    public GameObject attack_2_background;
    public GameObject action_background;
    public GameObject action_2_background;
    public GameObject rob_background;
    public GameObject rob_2_background;
    public GameObject rob_3_background;
    public GameObject escape_background;
    public GameObject escape_2_background;
    public GameObject character_button;
    public GameObject movement_button;
    public GameObject attack_button;
    public GameObject action_button;
    public GameObject rob_button;
    public GameObject escape_button;
    public GameObject exit_menu_button;

    // Use this for initialization
    void Start () {
		mh = FindObjectOfType<MapHandler> ();
		if (mh.pause) {
			pauseMenu.SetActive (true);
        } else {
			pauseMenu.SetActive (false);
            tutorial_menu.SetActive(false);
            general_info_background.SetActive(true);
            general_info_button.SetActive(true);
            character_background.SetActive(false);
            movement_background.SetActive(false);
            attack_background.SetActive(false);
            attack_2_background.SetActive(false);
            action_background.SetActive(false);
            action_2_background.SetActive(false);
            rob_background.SetActive(false);
            rob_2_background.SetActive(false);
            rob_3_background.SetActive(false);
            escape_background.SetActive(false);
            escape_2_background.SetActive(false);
            character_button.SetActive(false);
            movement_button.SetActive(false);
            attack_button.SetActive(false);
            action_button.SetActive(false);
            rob_button.SetActive(false);
            escape_button.SetActive(false);
            exit_menu_button.SetActive(false);
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
