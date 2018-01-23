using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour {

	private MapHandler mh;
	private UIHandler uh;
	private GameState lastState;
	public bool isRunning;
	private bool cinematicSet;

	//public delegate void RunningDelegate (GameState gState);
	//public event RunningDelegate RunningEvent;

	// Use this for initialization
	void Start () {
		isRunning = false;
		mh = FindObjectOfType<MapHandler> ();
		uh = FindObjectOfType<UIHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isRunning && !cinematicSet) {
            SetCinematic();
		} else if (!isRunning && cinematicSet) {
            ResetCinematic();
		}
	}

	void SetCinematic(){
		cinematicSet = true;
		uh.gameObject.SetActive (false);
		lastState = mh.gs;
		mh.ChangeState (GameState.Cinematic);
	}

	void ResetCinematic(){
		cinematicSet = false;
		uh.gameObject.SetActive (true);
		mh.ChangeState (lastState);
	}
}
