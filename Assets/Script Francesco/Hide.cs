using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour {

	public int cooldown;
	MapHandler mh;
	Invisibility effect;

	// Use this for initialization
	void Start () {
		mh = FindObjectOfType<MapHandler> ();
		mh.nextRoundEvent += CoolDown;
		effect = GetComponentInChildren<Invisibility> ();
		effect.ActiveInvisibility ();
	}

	public void CoolDown(int n){
		cooldown -= 1;
		if (cooldown <= 0) {
			Destroy (this);
		}
	}

	public void SetCD(int cd){
		cooldown = cd;
	}

	public void OnDestroy(){
		mh = FindObjectOfType<MapHandler> ();
		mh.nextRoundEvent -= CoolDown;
		effect.DeactiveInvisibility ();
	}
}
