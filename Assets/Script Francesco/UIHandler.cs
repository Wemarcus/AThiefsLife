using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

	public GameState gs;
	public InputState inpS = InputState.Nothing;
	public int turnCount;
	public GameObject player;
	public bool animPerform;
	public MapHandler mh;
	public bool enable = true;

	//public UnityEngine.UI.Text gameStateText;
	//public UnityEngine.UI.Text inputStateText;
	public UnityEngine.UI.Text TurnText;

	public UnityEngine.UI.Button MoveBtn;
	public UnityEngine.UI.Button AttackBtn;
	public UnityEngine.UI.Button AttackBtn2;
	public UnityEngine.UI.Button ActionBtn;
	public UnityEngine.UI.Button ActionBtn2;
	public UnityEngine.UI.Button PassTurnBtn;

	public GameObject buttonPanel;
	public Sprite selectedMoveBtn;
	public Sprite notselectedMoveBtn;

	void OnEnable(){
		mh.changeStateEvent += ChangeState;
		mh.changeInputStateEvent += ChangeInputState;
		mh.selectPlayerEvent += ChangeSelectedPlayer;
		mh.nextRoundEvent += ChangeTurnCount;
		mh.animationEvent += AnimationPerform;
	}

	public void ChangeState(GameState gameS){
		gs = gameS;
		//gameStateText.text = gameS.ToString ();
		UpdateUI ();
	}

	public void ChangeInputState(InputState inptS){
		inpS = inptS; 
		//inputStateText.text = inptS.ToString ();
		UpdateUI ();

	}

	public void ChangeTurnCount(int n){
		turnCount = n;
		TurnText.text = turnCount.ToString ();
		UpdateUI ();
	}

	public void AnimationPerform(bool b){
		animPerform = b;
		UpdateUI ();
	}

	public void ChangeSelectedPlayer(GameObject player2){
		player = player2;
		UpdateUI ();
	}

	public void OnClickMove(){
		ResetPreviousAction ();
		mh.ShowCurrentPlayerMovement (player);
	}

	public void OnClickAttack1(){ //TODO aggiustare typeofattack
		ResetPreviousAction ();
		Player plr = player.GetComponent<Player>();
		mh.WeaponTarget (player,plr.firstWeapon);
		UpdateUI ();
	}

	public void OnClickAttack2(){ //TODO aggiustare typeofattack
		ResetPreviousAction ();
		Player plr = player.GetComponent<Player>();
		mh.WeaponTarget (player,plr.secondWeapon);
		UpdateUI ();
	}

	public void OnClickAction1(){
		ResetPreviousAction ();
		Player plr = player.GetComponent<Player> ();
		plr.firstAction.PerformAction (player);
		UpdateUI ();
	}

	public void OnClickAction2(){
		ResetPreviousAction ();
		Player plr = player.GetComponent<Player> ();
		plr.secondAction.PerformAction (player);
		UpdateUI ();
	}

	public void OnClickPass(){
		ResetPreviousAction ();
		mh.PassAllyTurn ();
	}

	public void ResetPreviousAction(){
		if (inpS == InputState.Attack) {
			mh.SelectNothing (null);
		}
		if (inpS == InputState.Movement) {
			mh.HideCurrentPlayerMovement ();
		}
		if (inpS == InputState.Abilty) {
			mh.RevertAbility (player);
		}
	}

	public void UpdateUI(){
		MoveBtn.GetComponent<Button> ().interactable = false;
		AttackBtn.GetComponent<Button> ().interactable = false;
		AttackBtn2.GetComponent<Button> ().interactable = false;
		ActionBtn.GetComponent<Button> ().interactable = false;
		ActionBtn2.GetComponent<Button> ().interactable = false;
		PassTurnBtn.GetComponent<Button> ().interactable = false;
		if(gs != GameState.AllyTurn){
			buttonPanel.SetActive (false);
		}else{
			buttonPanel.SetActive (true);
		}
		if (!animPerform) {
			if ((inpS == InputState.Decision || inpS == InputState.Attack || inpS == InputState.Movement || inpS == InputState.Abilty) && player != null) {
				Sprite img;
				Player plr = player.GetComponent<Player> ();

				MoveBtn.GetComponent<Image> ().sprite = notselectedMoveBtn;

				img = AttackBtn.gameObject.GetComponent<Sprite> ();
				img = player.GetComponent<Player> ().firstWeapon.wpnImage;
				AttackBtn.GetComponent<Image> ().sprite = img;

				img = AttackBtn2.gameObject.GetComponent<Sprite> ();
				img = player.GetComponent<Player> ().secondWeapon.wpnImage;
				AttackBtn2.GetComponent<Image> ().sprite = img;

				img = ActionBtn.gameObject.GetComponent<Sprite> ();
				img = player.GetComponent<Player> ().firstAction.ActionImage;
				ActionBtn.GetComponent<Image> ().sprite = img;

				img = ActionBtn.gameObject.GetComponent<Sprite> ();
				img = player.GetComponent<Player> ().secondAction.ActionImage;
				ActionBtn2.GetComponent<Image> ().sprite = img;

				if (plr && !plr.moved)
					MoveBtn.GetComponent<Button> ().interactable = true;
				if (plr && !plr.attacked) {	
					if(!plr.firstWeapon.IsOnCooldown())
					AttackBtn.GetComponent<Button> ().interactable = true;
					if(!plr.secondWeapon.IsOnCooldown())
					AttackBtn2.GetComponent<Button> ().interactable = true;
				}
				if (plr && !plr.actionDone) {
					if(!plr.firstAction.IsOnCoolDown())
					ActionBtn.GetComponent<Button> ().interactable = true;
					if(!plr.secondAction.IsOnCoolDown())
					ActionBtn2.GetComponent<Button> ().interactable = true;
				}
				if (plr && (!plr.moved || !plr.attacked || !plr.actionDone))
					PassTurnBtn.GetComponent<Button> ().interactable = true;
			}
			if (mh && player) {
				Sprite img;
				if (player.GetComponent<Player> ().firstWeapon == mh.selectedWeapon) {
					img = AttackBtn.gameObject.GetComponent<Sprite> ();
					img = player.GetComponent<Player> ().firstWeapon.selectedWpnImage;
					AttackBtn.GetComponent<Image> ().sprite = img;
				}  if (player.GetComponent<Player> ().secondWeapon == mh.selectedWeapon) {
					img = AttackBtn2.gameObject.GetComponent<Sprite> ();
					img = player.GetComponent<Player> ().secondWeapon.selectedWpnImage;
					AttackBtn2.GetComponent<Image> ().sprite = img;
				}  if (player.GetComponent<Player> ().firstAction == mh.selectedAction) {
					img = ActionBtn.gameObject.GetComponent<Sprite> ();
					img = player.GetComponent<Player> ().firstAction.SelectedActionImage;
					ActionBtn.GetComponent<Image> ().sprite = img;
				}  if (player.GetComponent<Player> ().secondAction == mh.selectedAction) {
					img = ActionBtn2.gameObject.GetComponent<Sprite> ();
					img = player.GetComponent<Player> ().secondAction.SelectedActionImage;
					ActionBtn2.GetComponent<Image> ().sprite = img;
				}  if (inpS == InputState.Movement) {
					MoveBtn.GetComponent<Image> ().sprite = selectedMoveBtn;
				}
			}
			/*if (inpS == InputState.Nothing) {
				MoveBtn.gameObject.SetActive (false);
				AttackBtn.gameObject.SetActive (false);
				AttackBtn2.gameObject.SetActive (false);
				PassTurnBtn.gameObject.SetActive (false);
			}*/
			/*if (inpS == InputState.Movement) {
				MoveBtn.gameObject.SetActive (false);
				AttackBtn.gameObject.SetActive (false);
				AttackBtn2.gameObject.SetActive (false);
				PassTurnBtn.gameObject.SetActive (false);
			}*/
		}
	}

	public void ReloadPlayerList(){
		FindObjectOfType<MapHandler> ().players = Grid.GridMath.FindPlayers ();	
	}
}
