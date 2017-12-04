using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveauEvent : MonoBehaviour {

	public List<GameObject> blockList;
	public bool EventTriggered;
	// Use this for initialization
	void Start () {
		//mh.nextRoundEvent += turnopassato;
	}
	
	// Update is called once per frame
	void Update () {
		checkIfSomeoneIsOn ();
	}

	void checkIfSomeoneIsOn(){
		Node n;
		if (!EventTriggered) {
			foreach (GameObject block in blockList) {
				n = block.GetComponent<Node> ();
				if (n.player != null) {
					//start event
					Debug.Log("starto evento");
					EventTriggered = true;
				}
			}
		}
	}
}
