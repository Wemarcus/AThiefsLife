using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

[Serializable]
public class SerializableGame {

	public string bossName;
	public int age;
	public int money;
	public int policemanKilled;
	public int employedKilled;
	public int robberies;
	public int arrested;
	[SerializeField]
	public List<int> banks;

	public bool full;

	/*public void SetGame(string bossN, int ag, int mon, int pol, int emp, int robb, int arr, List<Bank> ban){
		bossName = bossN;
		age = ag;
		money = mon;
		policemanKilled = pol;
		employedKilled = emp;
		robberies = robb;
		arrested = arr;
		//banks = ban;
	}*/
}
