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

	public EndRobbery end;

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

	public void UpdateStatsRun(int moneyAmount, int policemanKilledAmount, int employedKilledAmount){
		age += 1;
		money += moneyAmount;
		policemanKilled += policemanKilledAmount;
		employedKilled += employedKilledAmount;
		robberies += 1;
	}

	public void UpdateStatsDied(int policemanKilledAmount, int employedKilledAmount){
		policemanKilled += policemanKilledAmount;
		employedKilled += employedKilledAmount;
	}

	public void UpdateStatsArrested(int policemanKilledAmount, int employedKilledAmount){
		age += 1;
		policemanKilled += policemanKilledAmount;
		employedKilled += employedKilledAmount;
		arrested += 1;
		robberies += 1;
	}
}
