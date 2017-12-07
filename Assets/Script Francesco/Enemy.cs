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
	public GameObject block;
	public TextMesh visualHP;
	public GameObject head;
	public List<GameObject> HitZone;
	public GameObject bulletPrefab;
	public GameObject shootPoint;

	public void Start(){
		currentHP = maxHP;
		visualHP = this.GetComponentInChildren<TextMesh> ();
		visualHP.text = currentHP.ToString ();
	}

	public void DealDamage(int damage){
		currentHP -= damage;
		visualHP.text = currentHP.ToString();
	}

	public void RunAI(){
		switch (ait) {
		case AiType.basic:
			BasicAI ();
			break;
		case AiType.onlymovement:
			OnlyMovementAI ();
			break;
		}
	}

	private void OnlyMovementAI(){
		GameObject MoveSpot;
		MoveSpots = GridMath.FindWalkPathInRange (block, moveRange);
		MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count)];
		if(MoveSpot)
		GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
	}

	private void BasicAI(){
		playersTrg = GridMath.FindPlayers ();
		playersTrg = GridFunc.HittablePlayers (this.gameObject, playersTrg);
		MoveSpots = GridMath.FindWalkPathInRange (block, moveRange);
		//Debug.Log (MoveSpots.Count);
		GameObject target;
		GameObject MoveSpot;
		if (playersTrg.Count > 0 && !this.GetComponent<Confusion>()) {
			//hit
			target = playersTrg[Random.Range(0,playersTrg.Count)];
			//FindObjectOfType<MapHandler> ().ProvideDamageToPlayer (target, damage);
			FindObjectOfType<MapHandler>().FireBulletToPlayer(target,this.gameObject,damage);
			Debug.Log ("sto colpendo:" + target.name); // calcolo del danno TODO
			//then move
			Debug.Log ("ho colpito:" + target.name + "ora mi muovo");
			MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count)];
			if(MoveSpot)
			GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
		} else {
			// move
			Debug.Log ("non potevo colpire nessuno, mi muovo");
			MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count)];
			if(MoveSpot)
			GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
			//try to hit
			playersTrg = GridFunc.HittablePlayers(this.gameObject,playersTrg);
			if (playersTrg.Count > 0 && !this.GetComponent<Confusion>()) {
				target = playersTrg[Random.Range(0,playersTrg.Count)];
				Debug.Log ("posso colpire:" + target.name);
				//Hit
				target = playersTrg[Random.Range(0,playersTrg.Count)];
				Debug.Log ("sto colpendo:" + target.name); // calcolo del danno TODO
				//FindObjectOfType<MapHandler> ().ProvideDamageToPlayer (target, damage);
				FindObjectOfType<MapHandler>().FireBulletToPlayer(target,this.gameObject,damage);
			}
		}
	}
}
