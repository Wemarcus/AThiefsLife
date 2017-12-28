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

	// Use this for initialization
	void Start () {
		//setto nome mappa
		//setto icone ladri
		mh = FindObjectOfType<MapHandler> ();
		policemanKilled.text = mh.policemanKilled.ToString();
		employedKilled.text = mh.EmployedKilled.ToString();
		moneyRobbed.text = mh.money.ToString();
		this.gameObject.SetActive (false);
		setted = true;
	}

	void OnEnable(){
		if (setted) {
			//setto nome mappa
			//setto icone ladri
			policemanKilled.text = mh.policemanKilled.ToString();
			employedKilled.text = mh.EmployedKilled.ToString();
			moneyRobbed.text = mh.money.ToString();
		}
	}
}
