using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confusion : MonoBehaviour {

	public int cooldown;
	MapHandler mh;

	// Use this for initialization
	void Start () {
		mh = FindObjectOfType<MapHandler> ();
		mh.nextRoundEvent += CoolDown;
	}

	public void CoolDown(int n){
		cooldown -= 1;
		if (this.gameObject.GetComponent<Player> ()) {
			Player p = this.gameObject.GetComponent<Player> ();
			p.attacked = true;
		}
		if (cooldown <= 0) {
			mh.nextRoundEvent -= CoolDown;
			Destroy (this);
		}
	}

	public void SetConfusionDuration(int cd){
		cooldown = cd;
		if (this.gameObject.GetComponent<Player> ()) {
			Player p = this.gameObject.GetComponent<Player> ();
			p.attacked = true;
		}
	}
}
