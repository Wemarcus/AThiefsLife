using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPanelHandler : MonoBehaviour {

	public GameObject Popup;
	public Text Paneltext;

	public delegate void ClickYesDelegate ();
	public event ClickYesDelegate clickYesEvent;

	public delegate void ClickNoDelegate ();
	public event ClickNoDelegate clickNoEvent;

	public void RunYes(){
		if (clickYesEvent != null) {
			clickYesEvent ();
		}
	}

	public void RunNo(){
		if (clickNoEvent != null) {
			clickNoEvent ();
		}
	}

	public void Surrend(){
		// TODO non implementato
	}

	public void SwitchTurn(){
		Popup.SetActive (true);
		Paneltext.text = "Switch turn to enemy?";
		clickYesEvent = SwitchYes;
		clickNoEvent = SwitchNo;
	}

	private void SwitchYes(){
		FindObjectOfType<MapHandler> ().PassAllyTurn ();
		Popup.SetActive (false);
	}

	private void SwitchNo(){
		Paneltext.text = "";
		Popup.SetActive (false);
	}
}
