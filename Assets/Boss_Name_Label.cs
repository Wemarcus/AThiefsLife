using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Name_Label : MonoBehaviour {

	private Text bossNameTxt;

	// Use this for initialization
	void OnEnable () {
		if (CurrentGame.cg != null) {
			bossNameTxt = this.GetComponent<Text> ();
			if (bossNameTxt != null) {
				bossNameTxt.text = CurrentGame.cg.bossName;
			}
		}
	}
}
