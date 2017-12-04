﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealEvent : MonoBehaviour {

	public List<GameObject> blockListTrigger;
	public int turn;

	// Use this for initialization
	void Start () {
		MapHandler mh = FindObjectOfType<MapHandler> ();
		mh.nextRoundEvent += TurnPassed;
	}
	
	void TurnPassed(int n){
		Node nod;
		foreach (GameObject block in blockListTrigger) {
			nod = block.GetComponent<Node> ();
			if (nod.player != null) {
				//start event
				turn -=1;
				Debug.Log ("Turni per rubare tutto :" + turn);
				if (turn <= 0) {
					Debug.Log ("Hai rubato tutto");
				}
			}
		}
	}
}