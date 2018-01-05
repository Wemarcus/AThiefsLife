using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Years_Label : MonoBehaviour {

	Text yearsText;

	// Use this for initialization
	void OnEnable () {
		yearsText = this.gameObject.GetComponent<Text> ();
		yearsText.text = CurrentGame.cg.end.years + " YEARS ?";
	}
}
