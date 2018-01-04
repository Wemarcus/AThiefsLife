using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentGame : MonoBehaviour {

	public static CurrentGame cg;

	public string bossName;
	public int age;
	public int money;
	public int policemanKilled;
	public int employedKilled;
	public int robberies;
	public int arrested;
	public List<Bank> banks;

	// Use this for initialization
	void Awake () {
		if (cg == null) {
			DontDestroyOnLoad (gameObject);
			cg = this;
		}else if(cg != this){
			Destroy (gameObject);
		}
	}

	public void StartNewGame(){
		bossName = null;
		age = 18;
		money = 0;
		policemanKilled = 0;
		employedKilled = 0;
		robberies = 0;
		arrested = 0;

		foreach (Bank bank in banks) {
			bank.securityLevel = 0;
		}
	}
}
