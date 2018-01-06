using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePowerUp : MonoBehaviour {

	public int cooldown;
	public int damagePercentage;
	MapHandler mh;
	GameObject effect;

	// Use this for initialization
	void Start () {
		mh = FindObjectOfType<MapHandler> ();
		mh.nextRoundEvent += CoolDown;
		if (damagePercentage == 20) {
			GameObject panel = transform.Find ("Effect_Interface").gameObject;
			effect = Instantiate(Resources.Load("DamageTeam", typeof(GameObject)),panel.transform) as GameObject;
		}
		if (damagePercentage == 30) {
			GameObject panel = transform.Find ("Effect_Interface").gameObject;
			effect = Instantiate(Resources.Load("DamageSelf", typeof(GameObject)),panel.transform) as GameObject;
		}

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
		if(mh)
		mh.nextRoundEvent -= CoolDown;
		Destroy (effect);
	}
}
