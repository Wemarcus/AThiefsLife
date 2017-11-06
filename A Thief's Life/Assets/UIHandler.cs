using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

	public GameState gs;
	public InputState inpS = InputState.Nothing;
	public GameObject player;
	public MapHandler mh;
	public bool enable = true;

	public UnityEngine.UI.Text gameStateText;
	public UnityEngine.UI.Text inputStateText;

	public UnityEngine.UI.Button MoveBtn;
	public UnityEngine.UI.Button AttackBtn;

	void OnEnable(){
		mh.changeStateEvent += ChangeState;
		mh.changeInputStateEvent += ChangeInputState;
		mh.selectPlayerEvent += ChangeSelectedPlayer;
	}

	public void ChangeState(GameState gameS){
		gs = gameS;
		gameStateText.text = gameS.ToString ();
		UpdateUI ();
	}

	public void ChangeInputState(InputState inptS){
		inpS = inptS; 
		inputStateText.text = inptS.ToString ();
		UpdateUI ();

	}

	public void ChangeSelectedPlayer(GameObject player2){
		player = player2;
		UpdateUI ();
	}

	public void OnClickMove(){
		mh.ShowCurrentPlayerMovement (player);
	}

	public void OnClickAttack(){
		mh.TargetEnemy (player);
	}
		
	private void UpdateUI(){
		if (inpS == InputState.Decision) {
			Player plr = player.GetComponent<Player> ();
			if (plr && !plr.moved)
				MoveBtn.gameObject.SetActive (true);
			else
				MoveBtn.gameObject.SetActive (false);
			if (plr && !plr.attacked)
				AttackBtn.gameObject.SetActive (true);
			else
				AttackBtn.gameObject.SetActive (false);
			
		}
		if (inpS == InputState.Nothing) {
			MoveBtn.gameObject.SetActive (false);
			AttackBtn.gameObject.SetActive (false);
		}
	}
}
