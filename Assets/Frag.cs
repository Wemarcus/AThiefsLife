using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frag : MonoBehaviour {

	public int damage;
	public int range;
	private bool exploded;
	// Use this for initialization
	void Start () {
		exploded = false;
	}

	public void SetBomb(int dmg,int rng){
		Debug.Log ("sono settato");
		damage = dmg;
		range = rng;
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
				if (target.GetComponent<Player> ()) {
					p = target.GetComponent<Player> ();
					p.DealDamage (damage);
				} else if (target.GetComponent<Enemy> ()) {
					e = target.GetComponent<Enemy> ();
					e.DealDamage (damage);
				}
				exploded = true;
			}
		}
        //Modifica Marco
		//Destroy (this.gameObject);
	}
}
