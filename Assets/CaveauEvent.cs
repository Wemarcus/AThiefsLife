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
	public bool SlerpDoor;

    // Parte Marco
    public GameObject drill;
    public GameObject effect;
    public float rotation; //rotazione della porta
    private Vector3 defaultRot;
    private Vector3 openRot;

    // Use this for initialization
    void Start () {
		MapHandler mh = FindObjectOfType<MapHandler> ();
		mh.nextRoundEvent += TurnPassed;
        defaultRot = Door.transform.eulerAngles;
        openRot = new Vector3(defaultRot.x, defaultRot.y + rotation, defaultRot.z);
    }
	
	// Update is called once per frame
	void Update () {
		checkIfSomeoneIsOn ();
		if (SlerpDoor) {
            Door.transform.eulerAngles = Vector3.Slerp(Door.transform.eulerAngles, openRot, Time.deltaTime * speed);
        }
	}

	void checkIfSomeoneIsOn(){
		Node n;
		if (!EventTriggered) {
			foreach (GameObject block in blockListTrigger) {
				n = block.GetComponent<Node> ();
                if (n.player != null)
                {
                    //start event
                    Debug.Log("starto evento");
                    EventTriggered = true;
                    drill.SetActive(true);

                    // fare funzione in grid math invece del foreach
                    foreach (GameObject go in blockListTrigger)
                    {
                        go.GetComponent<cakeslice.Outline>().color = 2;
                    }
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
            effect.SetActive(false);
		}
	}
}
