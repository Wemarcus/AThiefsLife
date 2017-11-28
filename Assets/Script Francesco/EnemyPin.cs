using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPin : MonoBehaviour {

	GameObject currentEnemyMarked;
	MapHandler mh;

	// Use this for initialization
	void Start () {

	}

	public void SetupPin(GameObject enemy){
		mh = FindObjectOfType<MapHandler> ();
		currentEnemyMarked = enemy;
		this.transform.position = new Vector3 (enemy.transform.position.x,enemy.transform.position.y+2,enemy.transform.position.z);
		this.gameObject.transform.parent = enemy.transform;
	}

	// Update is called once per frame
	void Update () {
		if (mh.inputS != InputState.Attack)
			Destroy (this.gameObject);
	}
}
