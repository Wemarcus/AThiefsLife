using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildStars : MonoBehaviour {

	public GameObject positiveStar;
	public GameObject negativeStar;
	private Bank bank;

	// Use this for initialization
	void OnEnable () {
		ClearStars ();
		bank = GetComponentInParent<Bank_Button> ().bank;
		int positiveToSpawn = bank.securityLevel + 1;
		int negativeToSpawn = 4 - positiveToSpawn;
		SpawnGameObject (positiveToSpawn, positiveStar);
		SpawnGameObject (negativeToSpawn, negativeStar);
	}

	void SpawnGameObject(int count, GameObject go){
		for( int i = count; i>0; i--){
			Instantiate (go, transform);
		}	
	}

	void ClearStars(){
		foreach (Transform child in transform) {
			GameObject.Destroy(child.gameObject);
		}
	}
}
