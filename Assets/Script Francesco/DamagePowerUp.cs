using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePowerUp : MonoBehaviour {

	public int cooldown;
	public int damagePercentage;
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

	public void SetDamageModifier(int cd,int damagePerc){
		cooldown = cd;
		damagePercentage = damagePerc;
	}

	public void OnDestroy(){
		mh = FindObjectOfType<MapHandler> ();
		mh.nextRoundEvent -= CoolDown;
	}
}
