using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircle : MonoBehaviour {

	public GameObject currentEnemyMarked;
	MapHandler mh;
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		mh = FindObjectOfType<MapHandler> ();
		currentEnemyMarked = this.gameObject.GetComponentInParent<Enemy> ().gameObject;
		sr = this.gameObject.GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		if (mh.CurrentTarget != currentEnemyMarked.gameObject || mh.inputS != InputState.Attack) {//mh.inputS != InputState.Attack)
			sr.enabled = false;
		}
		if (mh.CurrentTarget != null && mh.CurrentTarget == currentEnemyMarked) {
			sr.enabled = true;
		}
	}
}
