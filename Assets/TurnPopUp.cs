using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPopUp : MonoBehaviour {

	MapHandler mh;
	Canvas cv;

	// Use this for initialization
	void Start () {
		cv = this.gameObject.GetComponent<Canvas> ();
	}

	void OnEnable(){
		mh = FindObjectOfType<MapHandler> ();
		mh.changeStateEvent += OnGameStateChange;
	}

	public void OnGameStateChange(GameState gs){
		if (gs == GameState.AllyTurn) {
			GameObject popUp = Instantiate(Resources.Load("Turn_Feedback_Allies", typeof(GameObject)),cv.transform) as GameObject;
		}
		if (gs == GameState.EnemyTurn) {
			GameObject popUp = Instantiate(Resources.Load("Turn_Feedback_Enemies", typeof(GameObject)),cv.transform) as GameObject;
		}
	}

	void OnDisable(){
		mh.changeStateEvent -= OnGameStateChange;
	}

	void OnDestroy(){
		mh.changeStateEvent -= OnGameStateChange;
	}
}
