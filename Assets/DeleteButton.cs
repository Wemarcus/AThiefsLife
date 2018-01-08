using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteButton : MonoBehaviour {

	public GameObject back;
	public GameObject delete_popup;
	public GameObject fakebackground;

	public void OnClickDelete(){
		SaveAndLoad.sal.DeleteGame ();
		fakebackground.SetActive(true);
		back.GetComponent<Button>().interactable = true;
		delete_popup.SetActive(false);
	}
}
