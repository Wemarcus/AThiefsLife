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

	public UnityEngine.UI.Text gameStateText;
	public UnityEngine.UI.Text inputStateText;
	public UnityEngine.UI.Text TurnText;

	public UnityEngine.UI.Button MoveBtn;
	public UnityEngine.UI.Button AttackBtn;
	public UnityEngine.UI.Button AttackBtn2;
	public UnityEngine.UI.Button ActionBtn;
	public UnityEngine.UI.Button ActionBtn2;
	public UnityEngine.UI.Button PassTurnBtn;

	void OnEnable(){
		mh.changeStateEvent += ChangeState;
		mh.changeInputStateEvent += ChangeInputState;
		mh.selectPlayerEvent += ChangeSelectedPlayer;
		mh.nextRoundEvent += ChangeTurnCount;
		mh.animationEvent += AnimationPerform;
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
		mh.ShowCurrentPlayerMovement (player);
	}

	public void OnClickAttack1(){ //TODO aggiustare typeofattack
		Player plr = player.GetComponent<Player>();
		mh.WeaponTarget (player,plr.firstWeapon);
	}

	public void OnClickAttack2(){ //TODO aggiustare typeofattack
		Player plr = player.GetComponent<Player>();
		mh.WeaponTarget (player,plr.secondWeapon);
	}

	public void OnClickAction1(){
		Player plr = player.GetComponent<Player> ();
		plr.firstAction.PerformAction (player);
	}

	public void OnClickAction2(){
		Player plr = player.GetComponent<Player> ();
		plr.secondAction.PerformAction (player);
	}

	public void OnClickPass(){
		mh.PassAllyTurn ();
	}
		
	private void UpdateUI(){
		MoveBtn.gameObject.SetActive (false);
		AttackBtn.gameObject.SetActive (false);
		AttackBtn2.gameObject.SetActive (false);
		ActionBtn.gameObject.SetActive (false);
		ActionBtn2.gameObject.SetActive (false);
		PassTurnBtn.gameObject.SetActive (false);

		if (!animPerform) {
			if (inpS == InputState.Decision && player != null) {
				Sprite img;
				Player plr = player.GetComponent<Player> ();
				if (plr && !plr.moved)
					MoveBtn.gameObject.SetActive (true);
				if (plr && !plr.attacked) {
					img = AttackBtn.gameObject.GetComponent<Sprite> ();
					img = player.GetComponent<Player> ().firstWeapon.wpnImage;
					AttackBtn.GetComponent<Image> ().sprite = img;
					AttackBtn.gameObject.SetActive (true);
					img = AttackBtn2.gameObject.GetComponent<Sprite> ();
					img = player.GetComponent<Player> ().secondWeapon.wpnImage;
					AttackBtn2.GetComponent<Image> ().sprite = img;
					AttackBtn2.gameObject.SetActive (true);
				}
				if (plr && !plr.actionDone) {
					img = ActionBtn.gameObject.GetComponent<Sprite> ();
					img = player.GetComponent<Player> ().firstAction.ActionImage;
					ActionBtn.GetComponent<Image> ().sprite = img;
					ActionBtn.gameObject.SetActive (true);
					img = ActionBtn.gameObject.GetComponent<Sprite> ();
					img = player.GetComponent<Player> ().secondAction.ActionImage;
					ActionBtn2.GetComponent<Image> ().sprite = img;
					ActionBtn2.gameObject.SetActive (true);
				}
				if (plr && (!plr.moved || !plr.attacked || !plr.actionDone))
					PassTurnBtn.gameObject.SetActive (true);
			}
			/*if (inpS == InputState.Nothing) {
				MoveBtn.gameObject.SetActive (false);
				AttackBtn.gameObject.SetActive (false);
				AttackBtn2.gameObject.SetActive (false);
				PassTurnBtn.gameObject.SetActive (false);
			}*/
			if (inpS == InputState.Movement) {
				MoveBtn.gameObject.SetActive (false);
				AttackBtn.gameObject.SetActive (false);
				AttackBtn2.gameObject.SetActive (false);
				PassTurnBtn.gameObject.SetActive (false);
			}
		}
	}

	public void ReloadPlayerList(){
		FindObjectOfType<MapHandler> ().players = Grid.GridMath.FindPlayers ();	
	}
}
