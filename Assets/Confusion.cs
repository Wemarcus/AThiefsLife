using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confusion : MonoBehaviour {

	public int cooldown;
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
		effect = Instantiate(Resources.Load("Flash", typeof(GameObject)),panel.transform) as GameObject;
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

	public void OnDestroy(){
		mh = FindObjectOfType<MapHandler> ();
		if(mh)
		mh.nextRoundEvent -= CoolDown;
	}
}
