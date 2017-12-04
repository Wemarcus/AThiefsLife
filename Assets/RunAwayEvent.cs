using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayEvent : MonoBehaviour {

	public List<GameObject> blockListTrigger;

	// Use this for initialization

	// Update is called once per frame
	void Update () {
		checkIfSomeoneIsOn ();
	}

	void checkIfSomeoneIsOn(){
		Node n;
		Player p;
			foreach (GameObject block in blockListTrigger) {
			n = block.GetComponent<Node> ();
			if (n.player != null) {
				p = n.player.GetComponent<Player> ();
				if (p.isBoss) {
					//start event
					Debug.Log("sto quittando");
					Application.Quit ();
				}
			}
		}
	}
}
