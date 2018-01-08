using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Savebutton : MonoBehaviour {

	public GameObject save_menu;
	public GameObject fake_background_2;
	public GameObject back_button;
	public GameObject save_button;

	public void OnClickSave(){
		SaveAndLoad.sal.SaveGame ();
		fake_background_2.SetActive(true);
		save_menu.SetActive(false);
		back_button.GetComponent<Button>().interactable = true;
		save_button.GetComponent<Button>().interactable = false;
	}
}
