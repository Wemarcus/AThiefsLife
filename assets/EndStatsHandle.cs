using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndStatsHandle : MonoBehaviour {

	public Text mapName;
	public GameObject thiefContent;
	public Text policemanKilled;
	public Text employedKilled;
	public Text moneyRobbed;
	MapHandler mh;
	bool setted;
	public List<GameObject> portraitList;
	public GameObject endStats;

	// Use this for initialization
	void Start () {
		//setto nome mappa
		//setto icone ladri
		mh = FindObjectOfType<MapHandler> ();
		policemanKilled.text = mh.policemanKilled.ToString();
		employedKilled.text = mh.EmployedKilled.ToString();
		moneyRobbed.text = mh.money.ToString();
		endStats.gameObject.SetActive (false);
		setted = true;
	}

	public void OnEnableEndStats(){
		if (setted) {
			endStats.gameObject.SetActive (true);
			foreach (GameObject portrait in portraitList) {
				portrait.transform.SetParent (thiefContent.transform);
			}
			mh.ChangeState (GameState.End);
			mh.ChangeInputState (InputState.Nothing);
			//setto nome mappa
			//setto icone ladri
			policemanKilled.text = mh.policemanKilled.ToString();
			employedKilled.text = mh.EmployedKilled.ToString();
			moneyRobbed.text = mh.money.ToString();
		}
	}
}
