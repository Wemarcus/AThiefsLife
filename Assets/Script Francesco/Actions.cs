using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ActionsType{
	HealSelf,
	HealTeam,
	Shield,
	DoubleAttack,
	PowerUpDamageSelf,
	PowerUpDamageTeam,
	Hide,
	Killer
}

public class Actions : MonoBehaviour {

	public string actionName;
	public string description;
	public ActionsType type;
	public int cooldown;
	public int internalCD;
	MapHandler mh; 
	public Sprite ActionImage;
	public Sprite SelectedActionImage;

	void Start(){
		ResetCD ();
		mh = FindObjectOfType<MapHandler> ();
		mh.nextRoundEvent += CoolDown;
	}

	void ResetCD(){
			internalCD = cooldown + 1;
	}

	public void PerformAction(GameObject player){
		Debug.Log ("azione :" + type.ToString ());
		switch (type) {
		case ActionsType.HealSelf:
			HealSelf (player);
			break;
		case ActionsType.HealTeam:
			HealTeamSetup (player);
			break;
		case ActionsType.Shield:
			ShieldSetup (player);
			break;
		case ActionsType.DoubleAttack:
			DoubleAttack (player);
			break;
		case ActionsType.PowerUpDamageSelf:
			PowerUpSelf (player);
			break;
		case ActionsType.PowerUpDamageTeam:
			PowerUpDamageTeamSetup (player);
			break;
		case ActionsType.Hide:
			HideSelf (player);
			break;
		case ActionsType.Killer:
			KillerSelf (player);
			break;
		}
	}

	public void RunAction(GameObject player){
		switch (type) {
		case ActionsType.HealTeam:
			HealTeam(player);
			break;
		case ActionsType.Shield:
			ShieldTeam(player);
			break;
		case ActionsType.PowerUpDamageTeam:
			PowerUpTeam (player);
			break;
		}
	}	

	void HideSelf(GameObject player){
		Player plr = player.GetComponent<Player> ();
		if (internalCD > cooldown) {
			Hide hd = player.AddComponent (typeof(Hide)) as Hide;
			hd.SetCD (1);
			internalCD = 0;
			plr.actionDone = true;
			mh.ChangeInputState (InputState.Decision);
			CheckIfPlayerIsDone (player);
		}
	}

	void KillerSelf(GameObject player){
		Player plr = player.GetComponent<Player> ();
		if (internalCD > cooldown) {
			Killer kl = player.AddComponent (typeof(Killer)) as Killer;
			kl.SetCD (1);
			internalCD = 0;
			plr.actionDone = true;
			mh.ChangeInputState (InputState.Decision);
			CheckIfPlayerIsDone (player);
		}
	}

	void PowerUpDamageTeamSetup(GameObject player){
		if (internalCD > cooldown) {
			List<GameObject> pathList = Grid.GridMath.FindWalkPathInRangeWithPlayers (Grid.GridMath.GetPlayerBlock (player), Grid.GridMath.GetPlayerMoveRange (player));
			Grid.GridMath.ActivateBlocksMesh (pathList);
			mh.ChangeInputState (InputState.Abilty);
			mh.selectedAction = this;
		}
	}

	void PowerUpSelf(GameObject player){
		Player plr = player.GetComponent<Player> ();
		if (internalCD > cooldown) {
			DamagePowerUp dp = player.AddComponent (typeof(DamagePowerUp)) as DamagePowerUp;
			dp.SetDamageModifier (1, 30);
			internalCD = 0;
			plr.actionDone = true;
			mh.ChangeInputState (InputState.Decision);
			CheckIfPlayerIsDone (player);
		}
	}

	void PowerUpTeam(GameObject player){
		List<GameObject> pathList = Grid.GridMath.FindWalkPathInRangeWithPlayers(Grid.GridMath.GetPlayerBlock(player),Grid.GridMath.GetPlayerMoveRange(player));
		Node n;
		Player p;
		foreach (GameObject block in pathList) {
			n = block.GetComponent<Node> ();
			if (n.player) {
				p = n.player.GetComponent<Player>();
				DamagePowerUp dp = p.gameObject.AddComponent (typeof(DamagePowerUp)) as DamagePowerUp;
				dp.SetDamageModifier (1, 20);
			}
			p = player.GetComponent<Player> ();
			p.actionDone = true;
		}
		Grid.GridMath.DeactivateBlocksMesh (pathList);
		Grid.GridMath.ResetDepth (pathList);
		mh.selectedAction = null;
		internalCD = 0;
		mh.ChangeInputState (InputState.Decision);
		CheckIfPlayerIsDone (player);
	}

	void DoubleAttack(GameObject player){
		Player plr = player.GetComponent<Player> ();
		if (internalCD > cooldown) {
			if (plr.attacked == true) {
				plr.attacked = false;
			} else {
				DoubleAttack da = player.AddComponent (typeof(DoubleAttack)) as DoubleAttack;
				da.SetCD (1);
			}
				internalCD = 0;
				plr.actionDone = true;
				mh.ChangeInputState (InputState.Decision);
				CheckIfPlayerIsDone (player);
		}
	}

	void HealSelf(GameObject player){
		Player plr;
		int healAmount;
		if (internalCD > cooldown) {
			plr = player.GetComponent<Player>();
			healAmount = plr.maxHP / 2;
			plr.Heal (healAmount);
			internalCD = 0;
			plr.actionDone = true;
			mh.ChangeInputState (InputState.Decision);
			CheckIfPlayerIsDone (player);
		}
	}

	void HealTeamSetup(GameObject player){
		if (internalCD > cooldown) {
			List<GameObject> pathList = Grid.GridMath.FindWalkPathInRangeWithPlayers (Grid.GridMath.GetPlayerBlock (player), Grid.GridMath.GetPlayerMoveRange (player));
			Grid.GridMath.ActivateBlocksMesh (pathList);
			mh.ChangeInputState (InputState.Abilty);
			mh.selectedAction = this;
		}
	}

	void ShieldSetup(GameObject player){
		if (internalCD > cooldown) {
			List<GameObject> pathList = Grid.GridMath.FindWalkPathInRangeWithPlayers (Grid.GridMath.GetPlayerBlock (player), Grid.GridMath.GetPlayerMoveRange (player));
			Grid.GridMath.ActivateBlocksMesh (pathList);
			mh.ChangeInputState (InputState.Abilty);
			mh.selectedAction = this;
		}
	}

	void HealTeam(GameObject player){
		List<GameObject> pathList = Grid.GridMath.FindWalkPathInRangeWithPlayers(Grid.GridMath.GetPlayerBlock(player),Grid.GridMath.GetPlayerMoveRange(player));
		Node n;
		Player p;
		foreach (GameObject block in pathList) {
			n = block.GetComponent<Node> ();
			if (n.player) {
				p = n.player.GetComponent<Player>();
				p.Heal ((p.maxHP / 100) * 30);
			}
			p = player.GetComponent<Player> ();
			p.actionDone = true;
		}
		Grid.GridMath.DeactivateBlocksMesh (pathList);
		Grid.GridMath.ResetDepth (pathList);
		mh.selectedAction = null;
		internalCD = 0;
		mh.ChangeInputState (InputState.Decision);
		CheckIfPlayerIsDone (player);
	}

	void ShieldTeam(GameObject player){
		List<GameObject> pathList = Grid.GridMath.FindWalkPathInRangeWithPlayers(Grid.GridMath.GetPlayerBlock(player),Grid.GridMath.GetPlayerMoveRange(player));
		Node n;
		Player p;
		Shield sh;
		foreach (GameObject block in pathList) {
			n = block.GetComponent<Node> ();
			if (n.player) {
				p = n.player.GetComponent<Player>();
				sh = p.gameObject.AddComponent(typeof(Shield)) as Shield;
				sh.SetShield (30, 2);
			}
		}
		p = player.GetComponent<Player> ();
		p.actionDone = true;
		Grid.GridMath.DeactivateBlocksMesh (pathList);
		Grid.GridMath.ResetDepth (pathList);
		mh.selectedAction = null;
		internalCD = 0;
		mh.ChangeInputState (InputState.Decision);
		CheckIfPlayerIsDone (player);
	}


	public void CoolDown(int n){
		internalCD += 1;
	}

	public void CheckIfPlayerIsDone(GameObject player){
		StartCoroutine(mh.CheckDoneAndEndTurn(player.GetComponent<Player>()));
		/*Player plr = player.GetComponent<Player> ();
		if (plr.IsDone ()) {
			Grid.GridMath.RemovePlayerFromList (mh.selectedPlayer, mh.players);
			mh.SelectPlayer (null);
			if(mh.players.Count >0)
			mh.SelectPlayer(mh.players[0]);
			mh.ChangeInputState (InputState.Nothing);
		}
		if (mh.CheckAllyEndTurn ()) {
			mh.ChangeState (GameState.EnemyTurn);
			mh.ChangeInputState (InputState.Nothing);
		}*/
	}
}
