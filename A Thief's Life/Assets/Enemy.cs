using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid;

public class Enemy : MonoBehaviour {

	public int moveRange = 3;
	public int maxHP;
	public int currentHP;
	public int damage;
	public AiType ait;
	public List<GameObject> playersTrg;
	public List<GameObject> MoveSpots;

	public void Start(){
		currentHP = maxHP;
	}

	public void RunAI(){
		switch (ait) {
		case AiType.basic:
			BasicAI ();
			break;
		}
	}

	private void BasicAI(){
		playersTrg = GridMath.FindPlayers ();
		playersTrg = GridFunc.HittablePlayers (this.gameObject, playersTrg);
		MoveSpots = GridMath.FindWalkPathInRange (this.transform.parent.gameObject, moveRange);
		Debug.Log (MoveSpots.Count);
		GameObject target;
		GameObject MoveSpot;
		if (playersTrg.Count > 0) {
			//hit
			target = playersTrg[Random.Range(0,playersTrg.Count)];
			FindObjectOfType<MapHandler> ().ProvideDamageToPlayer (target, damage);
			Debug.Log ("sto colpendo:" + target.name); // calcolo del danno TODO
			//then move
			Debug.Log ("ho colpito:" + target.name + "ora mi muovo");
			MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count)];
			GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
		} else {
			// move
			Debug.Log ("non potevo colpire nessuno, mi muovo");
			MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count)];
			GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
			//try to hit
			playersTrg = GridFunc.HittablePlayers(this.gameObject,playersTrg);
			if (playersTrg.Count > 0) {
				target = playersTrg[Random.Range(0,playersTrg.Count)];
				Debug.Log ("posso colpire:" + target.name);
				//Hit
				target = playersTrg[Random.Range(0,playersTrg.Count)];
				Debug.Log ("sto colpendo:" + target.name); // calcolo del danno TODO
				FindObjectOfType<MapHandler> ().ProvideDamageToPlayer (target, damage);
			}
		}
	}
}
