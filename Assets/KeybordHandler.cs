using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybordHandler : MonoBehaviour {

	public bool enable;
	GameState gs;
	InputState inpS;
	GameObject player;
	MapHandler mh;

	// Use this for initialization
	void Start () {
	}

	void OnEnable () {
		mh = FindObjectOfType<MapHandler> ();
		mh.changeStateEvent += ChangeState;
		mh.changeInputStateEvent += ChangeInputState;
		mh.selectPlayerEvent += ChangeSelectedPlayer;
	}

	// Update is called once per frame
	void Update () {
		if (inpS == InputState.Attack && gs == GameState.AllyTurn) {
			ChangeEnemyTarget ();
		}
		if ((inpS == InputState.Decision || inpS == InputState.Nothing || inpS == InputState.Abilty || inpS == InputState.Movement) && gs == GameState.AllyTurn) {
			SwitchSelectedPlayer ();

		}
	}

	void SwitchSelectedPlayer(){
		int index;
		if ((Input.GetKeyDown (KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.A)) && enable) {
			ResetPreviousAction ();
			FindObjectOfType<MapHandler> ().ChangeInputState (InputState.Decision);
			MapHandler mh = FindObjectOfType<MapHandler> ();
			index = mh.players.IndexOf (mh.selectedPlayer);
			if (index - 1 >= 0 && mh.players.Count > 1) {
				mh.SelectPlayer(mh.players[index-1]);
			} else if(mh.players.Count >1) {
				mh.SelectPlayer(mh.players[mh.players.Count-1]);
			}
		}

			if ((Input.GetKeyDown (KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.D)) && enable) {
			ResetPreviousAction ();
			FindObjectOfType<MapHandler> ().ChangeInputState (InputState.Decision);
			MapHandler mh = FindObjectOfType<MapHandler> ();
			index = mh.players.IndexOf (mh.selectedPlayer);
			if (index + 1 < mh.players.Count && mh.players.Count > 1) {
				mh.SelectPlayer (mh.players [index + 1]);
			} else if (mh.players.Count > 1) {
				mh.SelectPlayer (mh.players [0]);
			}
		}
	}

	void ChangeEnemyTarget(){
		int index;
		if ((Input.GetKeyDown (KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.A)) && enable) {
			MapHandler mh = FindObjectOfType<MapHandler> ();
			index = mh.targetList.IndexOf (mh.CurrentTarget);
			if (index - 1 >= 0 && mh.targetList.Count > 1) {
				//mh.CurrentTarget = mh.targetList [index - 1];
				mh.ChangeTarget(mh.targetList[index-1]);
				Grid.GridMath.RotateCharacter (mh.selectedPlayer, mh.CurrentTarget);
			} else if(mh.targetList.Count >1) {
				//mh.CurrentTarget = mh.targetList [mh.targetList.Count - 1];
				mh.ChangeTarget(mh.targetList[mh.targetList.Count-1]);
				Grid.GridMath.RotateCharacter (mh.selectedPlayer, mh.CurrentTarget);
			}
			EnemyPin enPin = Instantiate (mh.EnemyPin).GetComponent<EnemyPin> ();
			enPin.SetupPin (mh.CurrentTarget);
		}

		if ((Input.GetKeyDown (KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.D)) && enable) {
			MapHandler mh = FindObjectOfType<MapHandler> ();
			index = mh.targetList.IndexOf (mh.CurrentTarget);
			if (index + 1 < mh.targetList.Count  && mh.targetList.Count > 1) {
				//mh.CurrentTarget = mh.targetList [index + 1];
				mh.ChangeTarget(mh.targetList[index+1]);
				Grid.GridMath.RotateCharacter (mh.selectedPlayer, mh.CurrentTarget);
			} else if(mh.targetList.Count >1) {
				//mh.CurrentTarget = mh.targetList [0];
				mh.ChangeTarget(mh.targetList[0]);
				Grid.GridMath.RotateCharacter (mh.selectedPlayer, mh.CurrentTarget);
			}
			EnemyPin enPin = Instantiate (mh.EnemyPin).GetComponent<EnemyPin> ();
			enPin.SetupPin (mh.CurrentTarget);
		}
	}

	public void ResetPreviousAction(){
		if (inpS == InputState.Attack) {
			mh.SelectNothing (null);
		}
		if (inpS == InputState.Movement) {
			mh.HideCurrentPlayerMovement ();
		}
		if (inpS == InputState.Abilty) {
			mh.RevertAbility (player.gameObject);
		}
	}

	public void ChangeState(GameState gameS){
		gs = gameS;
	}

	public void ChangeInputState(InputState inptS){
		inpS = inptS; 
	}

	public void ChangeSelectedPlayer(GameObject player2){
		player = player2;
	}

}
