using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircle : MonoBehaviour {

	MapHandler mh;
	GameObject currentPlayerMarked;
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		mh = FindObjectOfType<MapHandler> ();
		currentPlayerMarked = this.gameObject.GetComponentInParent<Player> ().gameObject;
		sr = this.gameObject.GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		if ((mh.selectedPlayer == null || mh.selectedPlayer != currentPlayerMarked) || mh.gs == GameState.Cinematic) {
			sr.enabled = false;
		}
		if ((mh.selectedPlayer != null && mh.selectedPlayer == currentPlayerMarked) && mh.gs == GameState.AllyTurn) {
			sr.enabled = true;
		}
	}
}
