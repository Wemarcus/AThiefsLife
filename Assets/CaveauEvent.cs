using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveauEvent : MonoBehaviour {

	public List<GameObject> blockListTrigger;
	public List<GameObject> blocksToBeActivated;
	public List<GameObject> blocksToBeDeactivated;
	public GameObject Door;
	public bool EventTriggered;
	public float speed;
	public Transform doorStartTrasnf;
	public bool SlerpDoor;

	// Use this for initialization
	void Start () {
		MapHandler mh = FindObjectOfType<MapHandler> ();
		mh.nextRoundEvent += TurnPassed;
		doorStartTrasnf = Door.transform;
	}
	
	// Update is called once per frame
	void Update () {
		checkIfSomeoneIsOn ();
		if (SlerpDoor) {
			Door.transform.rotation = Quaternion.Slerp (doorStartTrasnf.rotation, new Quaternion (0, 180, 0, 0), speed * Time.deltaTime);
		}
	}

	void checkIfSomeoneIsOn(){
		Node n;
		if (!EventTriggered) {
			foreach (GameObject block in blockListTrigger) {
				n = block.GetComponent<Node> ();
				if (n.player != null) {
					//start event
					Debug.Log ("starto evento");
					EventTriggered = true;

				}
			}
		}
	}

	void TurnPassed(int n){
		if (EventTriggered) {
			foreach (GameObject block in blocksToBeActivated) {
				Grid.GridMath.ChangeBlockType (block, BlockType.Walkable);
			}
			foreach (GameObject block in blocksToBeDeactivated) {
				Grid.GridMath.ChangeBlockType (block, BlockType.Obstacle);
			}
			SlerpDoor = true;
		}
	}
}
