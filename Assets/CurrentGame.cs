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

	bool full;

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
		full = true;

		foreach (Bank bank in banks) {
			bank.securityLevel = 0;
		}
	}

	public SerializableGame SerializeGame(){
		SerializableGame sg = new SerializableGame ();
		sg.banks = new List<int> ();
		sg.bossName = bossName;
		sg.age = age;
		sg.money = money;
		sg.policemanKilled = policemanKilled;
		sg.employedKilled = employedKilled;
		sg.robberies = robberies;
		sg.arrested = arrested;
		sg.full = full;
		int count = banks.Count;
		Debug.Log (count);
		for (int i=0; count > i; i++ ) {
			Debug.Log ("iterazione");
			sg.banks.Add(banks[i].securityLevel);
		}
		return sg;
	}

	public void LoadGame(SerializableGame sg){
		bossName = sg.bossName;
		age = sg.age;
		money = sg.money;
		policemanKilled = sg.policemanKilled;
		employedKilled = sg.employedKilled;
		robberies = sg.robberies;
		arrested = sg.arrested;
		full = sg.full;
		int count = banks.Count;
		for (int i=0; count > i; i++ ) {
			banks [i].securityLevel = sg.banks [i];
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

	public void PayBail(int moneyToPay){
		money -= moneyToPay;
	}

	public void ServeSentence(int yearsToServe){
		age += yearsToServe;
	}
}
