using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStatsHandle : MonoBehaviour {

	public Text age;
	public Text police;
	public Text employed;
	public Text money;
	public Text robbery;
	public Text arrested;

	void OnEnable(){
		age.text = CurrentGame.cg.age.ToString();
		police.text = CurrentGame.cg.policemanKilled.ToString ();
		employed.text = CurrentGame.cg.employedKilled.ToString ();
		money.text = ServiceLibrary.ReturnDotOfInt (CurrentGame.cg.money);
		robbery.text = CurrentGame.cg.robberies.ToString ();
		arrested.text = CurrentGame.cg.arrested.ToString ();
	}
}
