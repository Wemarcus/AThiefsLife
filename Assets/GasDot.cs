using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasDot : MonoBehaviour {

	public int cooldown;
	public int damage;
	MapHandler mh;
	GameObject effect;

	// Use this for initialization
	void Start () {
		mh = FindObjectOfType<MapHandler> ();
		mh.nextRoundEvent += CoolDown;
		GameObject panel = transform.Find ("Enemy_Interface/Enemy_Interface/DX").gameObject;
		if (panel.transform.childCount > 0) {
			panel = transform.Find ("Enemy_Interface/Enemy_Interface/SX").gameObject;
		}
		effect = Instantiate(Resources.Load("Gas", typeof(GameObject)),panel.transform) as GameObject;
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

		public void OnDestroy(){
			mh = FindObjectOfType<MapHandler> ();
		if(mh)
			mh.nextRoundEvent -= CoolDown;
		Destroy (effect);
		}
}
