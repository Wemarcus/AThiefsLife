using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpeed : MonoBehaviour {

	MapHandler mh;
	bool fast;
	public Button fastBtn;
	public Button normalBtn;

	void Start(){
		mh = FindObjectOfType<MapHandler> ();
		mh.changeStateEvent += OnTurnPass;
	}

	void OnEnable(){
		if (mh) {
			mh.changeStateEvent += OnTurnPass;
		}
	}

	public void FastForward()
    {
		fast = true;
        Time.timeScale = 2.0F;
		normalBtn.gameObject.SetActive (true);
		fastBtn.gameObject.SetActive (false);
    }

    public void NormalSpeed()
    {
		fast = false;
        Time.timeScale = 1.0F;
		normalBtn.gameObject.SetActive (false);
		fastBtn.gameObject.SetActive (true);
    }

	void OnTurnPass(GameState gs){
		if (fast)
			NormalSpeed();
		if (gs == GameState.EnemyTurn) {
			normalBtn.gameObject.SetActive (false);
			fastBtn.gameObject.SetActive (true);
		} else {
			normalBtn.gameObject.SetActive (false);
			fastBtn.gameObject.SetActive (false);
		}
	}

	void OnDisable(){
		if (mh) {
			mh.changeStateEvent -= OnTurnPass;
		}
	}
}
