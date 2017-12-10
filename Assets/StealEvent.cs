using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealEvent : MonoBehaviour {

	public List<GameObject> blockListTrigger;
	public int turn;
	public int money;
	private int turnCount;
    // Parte Marco
    public GameObject effect;

    // Use this for initialization
    void Start () {
		turnCount = turn;
		MapHandler mh = FindObjectOfType<MapHandler> ();
		mh.nextRoundEvent += TurnPassed;
	}
	
	void TurnPassed(int n){
		Node nod;
		foreach (GameObject block in blockListTrigger) {
			nod = block.GetComponent<Node> ();
			if (nod.player != null && turn > 0) {
				//start event
				FindObjectOfType<MapHandler> ().StealMoney (money / turnCount);
				turn -=1;
				Debug.Log ("Turni per rubare tutto :" + turn);

                if (turn <= 0)
                {
                    Debug.Log("Hai rubato tutto");

                    effect.SetActive(false);

                    // fare funzione in grid math invece del foreach
                    foreach (GameObject go in blockListTrigger)
                    {
                        go.GetComponent<cakeslice.Outline>().color = 2;
                    }
                }
			}
		}
	}
}
