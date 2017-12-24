using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPin : MonoBehaviour {

	GameObject currentEnemyMarked;
	MapHandler mh;

	// Use this for initialization
	void Start () {

	}

	public void SetupPin(GameObject enemy, Vector3 scale){
		mh = FindObjectOfType<MapHandler> ();
		currentEnemyMarked = enemy;
		this.transform.position = new Vector3 (enemy.transform.position.x,enemy.transform.position.y,enemy.transform.position.z);
		this.gameObject.transform.parent = enemy.transform;
		this.transform.localScale = scale;
	}

	// Update is called once per frame
	void Update () {
		if (mh.CurrentTarget != currentEnemyMarked.gameObject || mh.inputS != InputState.Attack)//mh.inputS != InputState.Attack)
			Destroy (this.gameObject);
	}
}
