using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour {

	public int damage;
	public int range;
	public int cooldown;
	private bool exploded;

	// Use this for initialization
	void Start () {
		exploded = false;
	}

	public void SetBomb(int dmg,int rng,int cd){
		Debug.Log ("sono settato");
		damage = dmg;
		range = rng;
		cooldown = cd;
	}

	void OnCollisionEnter(Collision collision){
		Debug.Log ("collido");
		if(!exploded){
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
					if (!p.immuneToGas) {
						p.DealDamage (damage);
						gd = target.AddComponent (typeof(GasDot)) as GasDot;
						gd.SetDamageOverTime (cooldown, damage);
					}
				} else if (target.GetComponent<Enemy> ()) {
					e = target.GetComponent<Enemy> ();
					if (!e.immuneToGas) {
						e.DealDamage (damage);
						gd = target.AddComponent (typeof(GasDot)) as GasDot;
						gd.SetDamageOverTime (cooldown, damage);
					}
				}
			}
			exploded = true;
		}
        //modifica MARCO
		//Destroy (this.gameObject);
	}
}
