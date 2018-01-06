using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	public int shieldPercentage;
	public int cooldown;
	MapHandler mh;
	GameObject effect;

	// Use this for initialization
	void Start () {
		mh = FindObjectOfType<MapHandler> ();
		mh.nextRoundEvent += CoolDown;
		GameObject panel = transform.Find ("Effect_Interface").gameObject;
		effect = Instantiate(Resources.Load("Shield", typeof(GameObject)),panel.transform) as GameObject;
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

	public void OnDestroy(){
		mh = FindObjectOfType<MapHandler> ();
		if(mh)
		mh.nextRoundEvent -= CoolDown;
		Destroy (effect);
	}
}
