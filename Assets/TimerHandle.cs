using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerHandle : MonoBehaviour {

	private Image yellowBar;
	private Text turnText;
	//private ButtonType bt;

	void Start(){
		yellowBar = this.gameObject.GetComponent<Image> ();
		turnText = this.gameObject.GetComponentInChildren<Text> ();
		//bt = this.gameObject.GetComponentInParent<ButtonDetailsHandler>().bt;
	}

	public void ShowCD(int turns){
		if (turns > 0) {
			yellowBar.enabled = true;
			turnText.text = turns.ToString ();
			turnText.enabled = true;
		} else {
			yellowBar.enabled = false;
			turnText.text = "";
			turnText.enabled = false;
		}
	}
}
