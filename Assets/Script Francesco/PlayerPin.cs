using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPin : MonoBehaviour {

	MapHandler mh;
	GameObject currentPlayerMarked;

	// Use this for initialization
	void Start () {
		mh = FindObjectOfType<MapHandler> ();
		currentPlayerMarked = mh.selectedPlayer;
		this.transform.position = new Vector3 (mh.selectedPlayer.transform.position.x,mh.selectedPlayer.transform.position.y+2,mh.selectedPlayer.transform.position.z);
		this.gameObject.transform.parent = mh.selectedPlayer.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (mh.selectedPlayer == null || mh.selectedPlayer != currentPlayerMarked)
			Destroy (this.gameObject);
	}
}
