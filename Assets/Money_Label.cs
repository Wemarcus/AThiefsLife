using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money_Label : MonoBehaviour {

	Text moneyText;

	// Use this for initialization
	void OnEnable () {
		moneyText = this.gameObject.GetComponent<Text> ();
		moneyText.text = ServiceLibrary.ReturnDotOfInt(CurrentGame.cg.money)+ " ?";
	}
}
