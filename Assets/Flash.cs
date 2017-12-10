using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour {

	public int range;
	public int cooldown;
	public bool exploded;
	// Use this for initialization
	void Start () {
		exploded = false;
	}

	public void SetBomb(int rng,int cd){
		Debug.Log ("sono settato");
		range = rng;
		cooldown = cd;
	}

	void OnCollisionEnter(Collision collision){
		Debug.Log ("collido");
		if (!exploded) {
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
				Confusion cf;
				if (target.GetComponent<Player> ()) {
					p = target.GetComponent<Player> ();
					cf = target.AddComponent (typeof(Confusion)) as Confusion;
					cf.SetConfusionDuration (cooldown);
				} else if (target.GetComponent<Enemy> ()) {
					e = target.GetComponent<Enemy> ();
					cf = target.AddComponent (typeof(Confusion)) as Confusion;
					cf.SetConfusionDuration (cooldown);
				}
			}
			exploded = true;
		}
        //Modifica Marco
		//Destroy (this.gameObject);
	}
}
