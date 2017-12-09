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
		if (currentHP <= 0) {
			MapHandler.FindObjectOfType<MapHandler>().targetList.Remove (this.gameObject);
			GridMath.ChangeBlockType (GridMath.GetEnemyBlock(this.gameObject), BlockType.Walkable);
			gameObject.GetComponent<Agent_Animation> ().death = true;
			//Destroy (enemy);
		}
	}

	public void RunAI(){
		FindObjectOfType<MainCamera> ().target = this.transform;
		switch (ait) {
		case AiType.basic:
			StartCoroutine( BasicAICor ());
			break;
		case AiType.onlymovement:
			StartCoroutine( OnlyMovementAICor ());
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

	private IEnumerator OnlyMovementAICor(){
		while (FindObjectOfType<MapHandler> ().PerformingAction) {
			yield return new WaitForSeconds(1f);
		}
		GameObject MoveSpot;
		MoveSpots = GridMath.FindWalkPathInRange (block, moveRange);
		MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count)];
		if(MoveSpot)
			GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
		
		while (FindObjectOfType<MapHandler> ().PerformingAction) {
			yield return  new WaitForSeconds(1f);
		}
		yield return new WaitForSeconds(1f);
		FindObjectOfType<AIHandler>().RunNextAI();

	}

	private IEnumerator BasicAICor(){
		playersTrg = GridMath.FindPlayers ();
		playersTrg = GridFunc.HittablePlayers (this.gameObject, playersTrg);
		MoveSpots = GridMath.FindWalkPathInRange (block, moveRange);
		//Debug.Log (MoveSpots.Count);
		GameObject target;
		GameObject MoveSpot;

		while (FindObjectOfType<MapHandler> ().PerformingAction) {
			yield return new WaitForSeconds(1f);
		}

		if (playersTrg.Count > 0 && !this.GetComponent<Confusion>()) {
			//hit
			target = playersTrg[Random.Range(0,playersTrg.Count)];
			//FindObjectOfType<MapHandler> ().ProvideDamageToPlayer (target, damage);
			Grid.GridMath.RotateCharacter(this.gameObject,target);
			yield return  new WaitForSeconds(0.5f);
			FindObjectOfType<MapHandler>().FireBulletToPlayer(target,this.gameObject,damage);
			yield return  new WaitForSeconds(1f);
			Debug.Log ("sto colpendo:" + target.name); // calcolo del danno TODO
			//then move
			Debug.Log ("ho colpito:" + target.name + "ora mi muovo");

			while (FindObjectOfType<MapHandler> ().PerformingAction) {
				yield return new WaitForSeconds(1f);
			}

			MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count)];
			if (MoveSpot) {
				GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
				yield return  new WaitForSeconds(1f);
			}
			
			while (FindObjectOfType<MapHandler> ().PerformingAction) {
				yield return  new WaitForSeconds(1f);
			}

		} else {
			// move
			Debug.Log ("non potevo colpire nessuno, mi muovo");
			MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count)];
			if (MoveSpot) {
				GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
				yield return  new WaitForSeconds(1f);
			}
			//try to hit

			while (FindObjectOfType<MapHandler> ().PerformingAction) {
				yield return  new WaitForSeconds(1f);
			}

			playersTrg = GridFunc.HittablePlayers(this.gameObject,playersTrg);
			if (playersTrg.Count > 0 && !this.GetComponent<Confusion>()) {
				target = playersTrg[Random.Range(0,playersTrg.Count)];
				Debug.Log ("posso colpire:" + target.name);
				//Hit
				target = playersTrg[Random.Range(0,playersTrg.Count)];
				Debug.Log ("sto colpendo:" + target.name); // calcolo del danno TODO
				//FindObjectOfType<MapHandler> ().ProvideDamageToPlayer (target, damage);
				Grid.GridMath.RotateCharacter(this.gameObject,target);
				yield return  new WaitForSeconds(0.5f);
				FindObjectOfType<MapHandler>().FireBulletToPlayer(target,this.gameObject,damage);
			}
		}
		//chiama prossimo
		/*while (FindObjectOfType<MapHandler> ().PerformingAction) {
			yield return null;
		}*/
		yield return new WaitForSeconds(1f);
		FindObjectOfType<AIHandler>().RunNextAI();
	}
}
