using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public ActionsType type;
	public int cooldown;
	MapHandler mh; 

	void Start(){
		ResetCD ();
		mh = FindObjectOfType<MapHandler> ();
		mh.nextRoundEvent += CoolDown;
	}

	void ResetCD(){
		switch (type) {
		case ActionsType.HealSelf:
			cooldown = 2;
			break;
		case ActionsType.HealTeam:
			cooldown = 2;
			break;
		case ActionsType.Shield:
			cooldown = 2;
			break;
		}
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
		}
	}	

	void HealSelf(GameObject player){
		Player plr;
		int healAmount;
		if (cooldown >= 2) {
			plr = player.GetComponent<Player>();
			healAmount = plr.maxHP / 2;
			plr.Heal (healAmount);
			cooldown = 0;
			plr.actionDone = true;
			mh.ChangeInputState (InputState.Decision);
			CheckIfPlayerIsDone (player);
		}
	}

	void HealTeamSetup(GameObject player){
		if (cooldown >= 2) {
			List<GameObject> pathList = Grid.GridMath.FindWalkPathInRangeWithPlayers (Grid.GridMath.GetPlayerBlock (player), Grid.GridMath.GetPlayerMoveRange (player));
			Grid.GridMath.ActivateBlocksMesh (pathList);
			mh.ChangeInputState (InputState.Abilty);
			mh.selectedAction = this;
		}
	}
	void ShieldSetup(GameObject player){
		if (cooldown >= 2) {
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
		cooldown = 0;
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
		cooldown = 0;
		mh.ChangeInputState (InputState.Decision);
		CheckIfPlayerIsDone (player);
	}


	public void CoolDown(int n){
		cooldown += 1;
	}

	public void CheckIfPlayerIsDone(GameObject player){
		Player plr = player.GetComponent<Player> ();
		if (plr.IsDone ()) {
			Grid.GridMath.RemovePlayerFromList (mh.selectedPlayer, mh.players);
			mh.SelectPlayer (null);
			mh.ChangeInputState (InputState.Nothing);
		}
		if (mh.CheckAllyEndTurn ()) {
			mh.ChangeState (GameState.EnemyTurn);
			mh.ChangeInputState (InputState.Nothing);
		}
	}
}
