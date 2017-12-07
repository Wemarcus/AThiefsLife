using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasDot : MonoBehaviour {

	public int cooldown;
	public int damage;
	MapHandler mh;

	// Use this for initialization
	void Start () {
		mh = FindObjectOfType<MapHandler> ();
		mh.nextRoundEvent += CoolDown;
	}

	public void CoolDown(int n){
		cooldown -= 1;
		Enemy e;
		Player p;
		if (this.gameObject.GetComponent<Player> ()) {
			p = this.gameObject.GetComponent<Player> ();
			p.DealDamage (damage);
		} else if (this.gameObject.GetComponent<Enemy> ()) {
			e = this.gameObject.GetComponent<Enemy> ();
			e.DealDamage (damage);
		}
		if (cooldown <= 0) {
			mh.nextRoundEvent -= CoolDown;
			Destroy (this);
		}
	}

	public void SetDamageOverTime(int cd,int dmg){
		cooldown = cd;
		damage = dmg;
	}
}
