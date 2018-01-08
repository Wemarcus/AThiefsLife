using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyTextHandler : MonoBehaviour {

	Text moneyText;

	// Use this for initialization
	void Start () {
		moneyText = this.gameObject.GetComponent<Text> ();
		moneyText.text = ServiceLibrary.ReturnDotOfInt(FindObjectOfType<MapHandler>().money);
		FindObjectOfType<MapHandler> ().addMoneyEvent += UpdateMoney;

	}

	private void UpdateMoney(int n){
		moneyText.text = ServiceLibrary.ReturnDotOfInt(FindObjectOfType<MapHandler>().money);
	}
}
