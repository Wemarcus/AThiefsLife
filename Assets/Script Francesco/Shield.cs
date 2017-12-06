using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	public int shieldPercentage;
	public int cooldown;
	MapHandler mh;
	// Use this for initialization
	void Start () {
		mh = FindObjectOfType<MapHandler> ();
		mh.nextRoundEvent += CoolDown;
	}

	public void CoolDown(int n){
		cooldown -= 1;
		if (cooldown <= 0) {
			Destroy (this);
		}
	}

	public void SetShield(int perc, int cd){
		shieldPercentage = perc;
		cooldown = cd;
	}
}
