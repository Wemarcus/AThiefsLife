using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionsType{
	Heal,
	Shield
}

public class Actions : MonoBehaviour {

	public ActionsType type;
	public int cooldown;

	public void PerformAction(){
		Debug.Log ("azione :" + type.ToString ());
	}
}
