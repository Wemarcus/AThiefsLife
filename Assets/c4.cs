using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c4 : MonoBehaviour {

	MapHandler mh;
	int cooldown = 0;
	int damage;
	int range;

	// Use this for initialization
	void Start () {
		mh = FindObjectOfType<MapHandler> ();
		mh.nextRoundEvent += CoolDown;
	}

	public void CoolDown(int n){
		cooldown += 1;
	}

	public void Setc4(int dmg,int rng){
		damage = dmg;
		range = rng;
	}

	void OnTriggerEnter (Collider collider)
	{
		if (cooldown > 1) {
			Debug.Log ("collido");
			List<GameObject> targetList = new List<GameObject> ();
			targetList.AddRange (GameObject.FindGameObjectsWithTag ("Enemy"));
			targetList.AddRange (GameObject.FindGameObjectsWithTag ("Player"));
			List<GameObject> sortedList = new List<GameObject> ();
			Debug.Log ("targets :" + targetList.Count);
			foreach (GameObject go in targetList) {
				float distancesqr = (this.transform.position - go.transform.position).sqrMagnitude;
				if (distancesqr < range) {
					sortedList.Add (go);
				}
			}
			Debug.Log ("targets in range :" + targetList.Count);
			foreach (GameObject target in sortedList) {
				Player p;
				Enemy e;
				if (target.GetComponent<Player> ()) {
					p = target.GetComponent<Player> ();
					p.DealDamage (damage);
				} else if (target.GetComponent<Enemy> ()) {
					e = target.GetComponent<Enemy> ();
					e.DealDamage (damage);
				}
			}
			Destroy (this.gameObject);
		}
	}
}

