using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid;

public class Enemy : MonoBehaviour {

	public int moveRange = 3;
	public int maxHP;
	public int currentHP;
	public AiType ait;
	public List<GameObject> players;
	public List<GameObject> MoveSpots;

	public void RunAI(){
		switch (ait) {
		case AiType.basic:
			BasicAI ();
			break;
		}
	}

	private void BasicAI(){
		players = GridMath.FindPlayers ();
		players = GridFunc.HittablePlayers (this.gameObject, players);
		MoveSpots = GridMath.FindWalkPathInRange (this.transform.parent.gameObject, moveRange);
		Debug.Log (MoveSpots.Count);
		GameObject target;
		GameObject MoveSpot;
		if (players.Count > 0) {
			//hit
			target = players[Random.Range(0,players.Count)];
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
			players = GridFunc.HittablePlayers(this.gameObject,players);
			if (players.Count > 0) {
				target = players[Random.Range(0,players.Count)];
				Debug.Log ("posso colpire:" + target.name);
				//Hit
				target = players[Random.Range(0,players.Count)];
				Debug.Log ("sto colpendo:" + target.name); // calcolo del danno TODO
			}
		}
	}
}
