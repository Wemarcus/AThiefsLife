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
		if (CurrentGame.cg != null) {
			if(age != null)
			age.text = CurrentGame.cg.age.ToString ();
			if(police != null)
			police.text = CurrentGame.cg.policemanKilled.ToString ();
			if(employed != null)
			employed.text = CurrentGame.cg.employedKilled.ToString ();
			if(money != null)
			money.text = ServiceLibrary.ReturnDotOfInt (CurrentGame.cg.money);
			if(robbery != null)
			robbery.text = CurrentGame.cg.robberies.ToString ();
			if(arrested != null)
			arrested.text = CurrentGame.cg.arrested.ToString ();
		}
	}
}
