using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconHandle : MonoBehaviour {

	Player player;
	bool death;
	public BarScript bs;
	public Image selectedImg;
	public Image currentHpBar;
	public Image voidHpBar;
	public Text hpTxt;
	GameObject selectedPlayer;

	// Use this for initialization
	void Start () {
		FindObjectOfType<MapHandler> ().selectPlayerEvent += SelectedPlayer;
	}
	
	// Update is called once per frame
	void Update () {
		if (player) {
			bs.setValue((float)player.currentHP);
		}
	}

	public void LinkIcon(GameObject iconPlayer){
		player = iconPlayer.GetComponent<Player>();
		bs.MaxValue = player.maxHP;
	}

	private void SelectedPlayer(GameObject playerS){
		selectedPlayer = playerS;
		try{
			if (player.gameObject && selectedPlayer == player.gameObject) {
				selectedImg.enabled = true;
				currentHpBar.color = new Color(1,1,1,1);
				voidHpBar.color = new Color(1,1,1,1);
				hpTxt.color = new Color(1,1,1,1);
			} else {
				selectedImg.enabled = false;
				currentHpBar.color = new Color(1,1,1,0.3f);
				voidHpBar.color = new Color(1,1,1,0.3f);
				hpTxt.color = new Color(1,1,1,0.3f);
				}
		}catch(Exception e){
			player = null;
		}
	}

	public void SelectPlayer(){
		if (FindObjectOfType<MapHandler> ().players.Contains (player.gameObject) && FindObjectOfType<MapHandler>().gs == GameState.AllyTurn) {
			FindObjectOfType<MapHandler> ().SelectPlayer (player.gameObject);
		}
	}
}
