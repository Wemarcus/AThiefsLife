using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour {

	public int damage;
	public int range;
	public int cooldown;

	// Use this for initialization
	void Start () {
	}

	public void SetBomb(int dmg,int rng,int cd){
		Debug.Log ("sono settato");
		damage = dmg;
		range = rng;
		cooldown = cd;
	}

	void OnCollisionEnter(Collision collision){
		Debug.Log ("collido");
		List<GameObject> targetList = new List<GameObject>();
		targetList.AddRange(GameObject.FindGameObjectsWithTag ("Enemy"));
		targetList.AddRange(GameObject.FindGameObjectsWithTag("Player"));
		List<GameObject> sortedList = new List<GameObject> ();
		Debug.Log ("targets :" + targetList.Count);
		foreach(GameObject go in targetList){
			float distancesqr = (this.transform.position - go.transform.position).sqrMagnitude;
			if(distancesqr < range){
				sortedList.Add(go);
			}
		}
		Debug.Log ("targets in range :" + targetList.Count);
		foreach (GameObject target in sortedList) {
			Player p;
			Enemy e;
			GasDot gd;
			if (target.GetComponent<Player> ()) {
				p = target.GetComponent<Player> ();
				p.DealDamage (damage);
				gd = target.AddComponent (typeof(GasDot)) as GasDot;
				gd.SetDamageOverTime (cooldown, damage);
			} else if (target.GetComponent<Enemy> ()) {
				e = target.GetComponent<Enemy> ();
				e.DealDamage (damage);
				gd = target.AddComponent (typeof(GasDot)) as GasDot;
				gd.SetDamageOverTime (cooldown, damage);
			}
		}
        //modifica MARCO
		//Destroy (this.gameObject);
	}
}
