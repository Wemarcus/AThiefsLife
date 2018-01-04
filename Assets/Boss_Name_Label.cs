using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Name_Label : MonoBehaviour {

	private Text bossNameTxt;

	// Use this for initialization
	void OnEnable () {
		bossNameTxt = this.GetComponent<Text> ();
		bossNameTxt.text = CurrentGame.cg.bossName;
	}
}
