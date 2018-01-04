using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBarHandler : MonoBehaviour {

	public Enemy enemy;
	public BarScript bs;
	public Image selectedImg;
	public Text hpTxt;
	InputState inpS;

	// Use this for initialization
	void Start () {
		FindObjectOfType<MapHandler> ().changeInputStateEvent += ChangeInputState;
		bs.MaxValue = enemy.maxHP;
	}

	// Update is called once per frame
	void Update () {
		if (enemy) {
			bs.setValue((float)enemy.currentHP);
		}
	}

	void OnDestroy(){
		if(FindObjectOfType<MapHandler>())
		FindObjectOfType<MapHandler>().changeInputStateEvent -= ChangeInputState;
	}

	public void SetHitPercentage(int perc){
		hpTxt.text = perc.ToString() + " %";
	}

	void ChangeInputState(InputState ins){
		inpS = ins;
		if (inpS == InputState.Attack) {
			hpTxt.enabled = true;
			selectedImg.enabled = true;
		} else {
			hpTxt.enabled = false;
			selectedImg.enabled = false;
			hpTxt.text = "0 %";
		}
	}
}
