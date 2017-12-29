using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid;

public class Enemy : MonoBehaviour {

	public int moveRange = 3;
	public int maxHP;
	public int currentHP;
	public int damage;
	public int range;
	public AiType ait;
	public List<GameObject> playersTrg;
	public List<GameObject> MoveSpots;
	public GameObject block;
	public GameObject head;
	public List<GameObject> HitZone;
	public GameObject bulletPrefab;
	public GameObject shootPoint;
	public EnemyBarHandler bar;
	public GameObject damagePopUpPrefab;
	public GameObject spawnPointDamagePopUpPrefab;

	public GameObject nearestPlayer;

	[Header("Aoe Setup(only if weapon type is aoe)")]

	public GameObject bombPrefab;
	public int cdDuration;
	public int bombRange;

	public void Start(){
		currentHP = maxHP;
		if(!FindObjectOfType<MapHandler>().enemiesOnMap.Contains(this.gameObject))
			FindObjectOfType<MapHandler> ().enemiesOnMap.Add (this.gameObject);
	}

	void OnEnable(){
		//FindObjectOfType<MapHandler> ().animationEvent += OnAnimationPerform;
		if(!FindObjectOfType<MapHandler>().enemiesOnMap.Contains(this.gameObject))
			FindObjectOfType<MapHandler> ().enemiesOnMap.Add (this.gameObject);
	}

	public void DealDamage(int damage){
		currentHP -= damage;
		GameObject pop = Instantiate (damagePopUpPrefab,spawnPointDamagePopUpPrefab.transform);
		DamageUI popui = pop.GetComponent<DamageUI> ();
		popui.SetText (damage.ToString ());
		//pop.transform.position = spawnPointDamagePopUpPrefab.transform.position;
		if (currentHP < 0) {
			currentHP = 0;
		}
		if (currentHP <= 0) {
			MapHandler.FindObjectOfType<MapHandler>().targetList.Remove (this.gameObject);
			GridMath.ChangeBlockType (GridMath.GetEnemyBlock(this.gameObject), BlockType.Walkable);
			gameObject.GetComponent<Agent_Animation> ().death = true;
			//Destroy (enemy);
		}
	}

	public void RunAI(){
		FindObjectOfType<MainCamera> ().target = this.head.transform;
		switch (ait) {
		case AiType.basic:
			StartCoroutine( BasicAICor ());
			break;
		case AiType.onlymovement:
			StartCoroutine( OnlyMovementAICor ());
			break;
		case AiType.bomb:
			StartCoroutine (BombAICor ());
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
		//playersTrg = GridMath.FindPlayers ();
		playersTrg.Clear();
		playersTrg.AddRange(FindObjectOfType<MapHandler>().playersOnMap);
		playersTrg = GridFunc.HittablePlayers (this.gameObject, playersTrg,range);
		MoveSpots = GridMath.FindWalkPathInRange (block, moveRange);
		//Debug.Log (MoveSpots.Count);
		GameObject target;
		GameObject MoveSpot;
		if (playersTrg.Count > 0 && !this.GetComponent<Confusion>()) {
			//hit
			target = playersTrg[Random.Range(0,playersTrg.Count)];
			//FindObjectOfType<MapHandler> ().ProvideDamageToPlayer (target, damage);
			FindObjectOfType<MapHandler>().FireBulletToPlayer(target,this.gameObject,damage);
			//Debug.Log ("sto colpendo:" + target.name); // calcolo del danno TODO
			//then move
			//Debug.Log ("ho colpito:" + target.name + "ora mi muovo");
			MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count)];
			if(MoveSpot)
				GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
		} else {
			// move
			//Debug.Log ("non potevo colpire nessuno, mi muovo");
			MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count)];
			if(MoveSpot)
				GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
			//try to hit
			playersTrg = GridFunc.HittablePlayers(this.gameObject,playersTrg,range);
			if (playersTrg.Count > 0 && !this.GetComponent<Confusion>()) {
				target = playersTrg[Random.Range(0,playersTrg.Count)];
				//Debug.Log ("posso colpire:" + target.name);
				//Hit
				target = playersTrg[Random.Range(0,playersTrg.Count)];
				//Debug.Log ("sto colpendo:" + target.name); // calcolo del danno TODO
				//FindObjectOfType<MapHandler> ().ProvideDamageToPlayer (target, damage);
				FindObjectOfType<MapHandler>().FireBulletToPlayer(target,this.gameObject,damage);
			}
		}
	}

	private IEnumerator OnlyMovementAICor(){
		while (FindObjectOfType<MapHandler> ().PerformingAction) {
			yield return new WaitForSeconds(1f);
		}
		Move ();
		/*GameObject MoveSpot;
		MoveSpots = GridMath.FindWalkPathInRange (block, moveRange);

		List<GameObject> sortedMoveSpots = new List<GameObject> ();
		Node n;
		foreach (GameObject elem in MoveSpots) {
			n = elem.GetComponent<Node> ();
			if (n.isCover)
				sortedMoveSpots.Add (elem);
		}
		if (sortedMoveSpots.Count > 0) {
			MoveSpot = sortedMoveSpots[Random.Range(0,sortedMoveSpots.Count-1)];
			if(MoveSpot)
				GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
		} else if(MoveSpots.Count >0){
			MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count-1)];
			if(MoveSpot)
				GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
			
		}*/

		/*MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count-1)];
		if(MoveSpot)
			GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);*/

		while (FindObjectOfType<MapHandler> ().PerformingAction) {
			yield return  new WaitForSeconds(1f);
		}
		yield return new WaitForSeconds(2f);
		LookNearestPlayer ();
		FindObjectOfType<AIHandler>().RunNextAI();

	}

	private IEnumerator BombAICor(){
		//playersTrg = GridMath.FindPlayers ();
		playersTrg.Clear();
		playersTrg.AddRange(FindObjectOfType<MapHandler>().playersOnMap);
		playersTrg = GridFunc.HittablePlayers (this.gameObject, playersTrg,range);
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
			FindObjectOfType<MapHandler> ().ThrowBombToPlayer (this.gameObject, target, bombPrefab, damage, bombRange, cdDuration);
			//FindObjectOfType<MapHandler>().FireBulletToPlayer(target,this.gameObject,damage); //TODO
			yield return  new WaitForSeconds(1f);
			//Debug.Log ("sto colpendo:" + target.name); // calcolo del danno TODO
			//then move
			//Debug.Log ("ho colpito:" + target.name + "ora mi muovo");

			while (FindObjectOfType<MapHandler> ().PerformingAction) {
				yield return new WaitForSeconds(1f);
			}

			List<GameObject> sortedMoveSpots = new List<GameObject> ();
			Node n;
			foreach (GameObject elem in MoveSpots) {
				n = elem.GetComponent<Node> ();
				if (n.isCover)
					sortedMoveSpots.Add (elem);
			}
			if (sortedMoveSpots.Count > 0) {
				if (playerIsNear()) {
					sortedMoveSpots = SortSpotsByNearestPlayer (sortedMoveSpots);
					MoveSpot = sortedMoveSpots [0];
				}else{
					MoveSpot = sortedMoveSpots[Random.Range(0,sortedMoveSpots.Count-1)];
				}
				if(MoveSpot)
					GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
			} else if(MoveSpots.Count >0){
				if (playerIsNear()) {
					MoveSpots = SortSpotsByNearestPlayer (MoveSpots);
					MoveSpot = MoveSpots [0];
				}else{
					MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count-1)];
				}
				if(MoveSpot)
					GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);

			}
			/*MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count)];
			if (MoveSpot) {
				GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
				yield return  new WaitForSeconds(1f);
			}*/

			while (FindObjectOfType<MapHandler> ().PerformingAction) {
				yield return  new WaitForSeconds(1f);
			}

		} else {
			// move
			//Debug.Log ("non potevo colpire nessuno, mi muovo");

			List<GameObject> sortedMoveSpots = new List<GameObject> ();
			Node n;
			foreach (GameObject elem in MoveSpots) {
				n = elem.GetComponent<Node> ();
				if (n.isCover)
					sortedMoveSpots.Add (elem);
			}
			if (sortedMoveSpots.Count > 0) {
				if (playerIsNear()) {
					sortedMoveSpots = SortSpotsByNearestPlayer (sortedMoveSpots);
					MoveSpot = sortedMoveSpots [0];
				}else{
					MoveSpot = sortedMoveSpots[Random.Range(0,sortedMoveSpots.Count-1)];
				}
				if(MoveSpot)
					GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
			} else if(MoveSpots.Count >0){
				if (playerIsNear()) {
					MoveSpots = SortSpotsByNearestPlayer (MoveSpots);
					MoveSpot = MoveSpots [0];
				}else{
					MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count-1)];
				}
				if(MoveSpot)
					GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);

			}

			/*MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count)];
			if (MoveSpot) {
				GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
				yield return  new WaitForSeconds(1f);
			}*/
			//try to hit

			while (FindObjectOfType<MapHandler> ().PerformingAction) {
				yield return  new WaitForSeconds(1f);
			}

			playersTrg = GridFunc.HittablePlayers(this.gameObject,playersTrg,range);
			if (playersTrg.Count > 0 && !this.GetComponent<Confusion>()) {
				target = playersTrg[Random.Range(0,playersTrg.Count)];
				//Debug.Log ("posso colpire:" + target.name);
				//Hit
				target = playersTrg[Random.Range(0,playersTrg.Count)];
				//Debug.Log ("sto colpendo:" + target.name); // calcolo del danno TODO
				//FindObjectOfType<MapHandler> ().ProvideDamageToPlayer (target, damage);
				Grid.GridMath.RotateCharacter(this.gameObject,target);
				yield return  new WaitForSeconds(0.5f);
				FindObjectOfType<MapHandler> ().ThrowBombToPlayer (this.gameObject, target, bombPrefab, damage, bombRange, cdDuration);
				//FindObjectOfType<MapHandler>().FireBulletToPlayer(target,this.gameObject,damage); //TODO
			}
		}
		//chiama prossimo
		/*while (FindObjectOfType<MapHandler> ().PerformingAction) {
			yield return null;
		}*/
		yield return new WaitForSeconds(2f);
		LookNearestPlayer ();
		FindObjectOfType<AIHandler>().RunNextAI();
	}

	private IEnumerator BasicAICor(){
		//playersTrg = GridMath.FindPlayers ();
		playersTrg.Clear();
		playersTrg.AddRange(FindObjectOfType<MapHandler>().playersOnMap);
		playersTrg = GridFunc.HittablePlayers (this.gameObject, playersTrg,range);
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
			//Debug.Log ("sto colpendo:" + target.name); // calcolo del danno TODO
			//then move
			//Debug.Log ("ho colpito:" + target.name + "ora mi muovo");

			while (FindObjectOfType<MapHandler> ().PerformingAction) {
				yield return new WaitForSeconds(1f);
			}

			List<GameObject> sortedMoveSpots = new List<GameObject> ();
			Node n;
			foreach (GameObject elem in MoveSpots) {
				n = elem.GetComponent<Node> ();
				if (n.isCover)
					sortedMoveSpots.Add (elem);
			}
			if (sortedMoveSpots.Count > 0) {
				if (playerIsNear()) {
					sortedMoveSpots = SortSpotsByNearestPlayer (sortedMoveSpots);
					MoveSpot = sortedMoveSpots [0];
				}else{
					MoveSpot = sortedMoveSpots[Random.Range(0,sortedMoveSpots.Count-1)];
				}
				if(MoveSpot)
					GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
			} else if(MoveSpots.Count >0){
				if (playerIsNear()) {
					MoveSpots = SortSpotsByNearestPlayer (MoveSpots);
					MoveSpot = MoveSpots [0];
				}else{
					MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count-1)];
				}
				if(MoveSpot)
					GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);

			}
			/*MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count)];
			if (MoveSpot) {
				GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
				yield return  new WaitForSeconds(1f);
			}*/

			while (FindObjectOfType<MapHandler> ().PerformingAction) {
				yield return  new WaitForSeconds(1f);
			}

		} else {
			// move
			//Debug.Log ("non potevo colpire nessuno, mi muovo");

			List<GameObject> sortedMoveSpots = new List<GameObject> ();
			Node n;
			foreach (GameObject elem in MoveSpots) {
				n = elem.GetComponent<Node> ();
				if (n.isCover)
					sortedMoveSpots.Add (elem);
			}
			if (sortedMoveSpots.Count > 0) {
				if (playerIsNear()) {
					sortedMoveSpots = SortSpotsByNearestPlayer (sortedMoveSpots);
					MoveSpot = sortedMoveSpots [0];
				}else{
					MoveSpot = sortedMoveSpots[Random.Range(0,sortedMoveSpots.Count-1)];
				}
				if(MoveSpot)
					GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
			} else if(MoveSpots.Count >0){
				if (playerIsNear()) {
					MoveSpots = SortSpotsByNearestPlayer (MoveSpots);
					MoveSpot = MoveSpots [0];
				}else{
					MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count-1)];
				}
				if(MoveSpot)
					GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);

			}

			/*MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count)];
			if (MoveSpot) {
				GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
				yield return  new WaitForSeconds(1f);
			}*/
			//try to hit

			while (FindObjectOfType<MapHandler> ().PerformingAction) {
				yield return  new WaitForSeconds(1f);
			}

			playersTrg = GridFunc.HittablePlayers(this.gameObject,playersTrg,range);
			if (playersTrg.Count > 0 && !this.GetComponent<Confusion>()) {
				target = playersTrg[Random.Range(0,playersTrg.Count)];
				//Debug.Log ("posso colpire:" + target.name);
				//Hit
				target = playersTrg[Random.Range(0,playersTrg.Count)];
				//Debug.Log ("sto colpendo:" + target.name); // calcolo del danno TODO
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
		yield return new WaitForSeconds(2f);
		LookNearestPlayer ();
		FindObjectOfType<AIHandler>().RunNextAI();
	}



	/*public void OnAnimationPerform(bool b){
		if (!b) {
			//StartCoroutine (LookPlayer ());
		}
	}*/

	private void Move(){
		MoveSpots = GridMath.FindWalkPathInRange (block, moveRange);
		List<GameObject> sortedMoveSpots = new List<GameObject> ();
		Node n;
		GameObject MoveSpot;
		foreach (GameObject elem in MoveSpots) {
			n = elem.GetComponent<Node> ();
			if (n.isCover)
				sortedMoveSpots.Add (elem);
		}
		if (sortedMoveSpots.Count > 0) {
			if (playerIsNear()) {
				sortedMoveSpots = SortSpotsByNearestPlayer (sortedMoveSpots);
				MoveSpot = sortedMoveSpots [0];
			}else{
				MoveSpot = sortedMoveSpots[Random.Range(0,sortedMoveSpots.Count-1)];
			}
			if(MoveSpot)
				GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
		} else if(MoveSpots.Count >0){
			if (playerIsNear()) {
				MoveSpots = SortSpotsByNearestPlayer (MoveSpots);
				MoveSpot = MoveSpots [0];
			}else{
				MoveSpot = MoveSpots[Random.Range(0,MoveSpots.Count-1)];
			}
			if(MoveSpot)
				GridMath.MoveEnemyToBlock (this.gameObject, MoveSpot);
		}
	}

	private void Attack(){
		GameObject target;
		playersTrg = GridFunc.HittablePlayers(this.gameObject,playersTrg,range);
		if (playersTrg.Count > 0 && !this.GetComponent<Confusion> ()) {
			target = playersTrg [Random.Range (0, playersTrg.Count)];
			//Debug.Log ("posso colpire:" + target.name);
			//Hit
			target = playersTrg [Random.Range (0, playersTrg.Count)];
			//Debug.Log ("sto colpendo:" + target.name); // calcolo del danno TODO
			//FindObjectOfType<MapHandler> ().ProvideDamageToPlayer (target, damage);
			Fire (target);
		}
	}

	private IEnumerator Fire(GameObject target){
		Grid.GridMath.RotateCharacter (this.gameObject, target);
		yield return  new WaitForSeconds (0.5f);
		FindObjectOfType<MapHandler> ().FireBulletToPlayer (target, this.gameObject, damage);
	}


	public void LookNearestPlayer(){
		StartCoroutine (LookPlayer ());
	}

	private IEnumerator LookPlayer(){
		yield return new WaitForSeconds (0.8f);
		Grid.GridFunc.LookNearestPlayer (this.gameObject, FindObjectOfType<MapHandler> ().playersOnMap, range);
	}

	public int CompareDistance(GameObject a, GameObject b) {
		Player player_a = a.GetComponent<Player> ();
		Player player_b = b.GetComponent<Player> ();
		Enemy enm = this;
		float distance_a =  Vector3.Distance(enm.transform.position, player_a.transform.position);
		float distance_b =  Vector3.Distance(enm.transform.position, player_b.transform.position);
		if(distance_a >= distance_b) {
			return 1;
		}
		else {
			return -1;
		}
	}

	public int CompareDistanceBetweenTiles(GameObject a, GameObject b) {
		float distance_a =  Vector3.Distance(nearestPlayer.transform.position, a.transform.position);
		float distance_b =  Vector3.Distance(nearestPlayer.transform.position, b.transform.position);
		if(distance_a >= distance_b) {
			return 1;
		}
		else {
			return -1;
		}
	}

	public GameObject GetNearestPlayer(){
		List<GameObject> playerList = FindObjectOfType<MapHandler> ().playersOnMap;
		playerList = GridFunc.HittablePlayers (this.gameObject, playerList, range);
		playerList.Sort (CompareDistance);
		if (playerList.Count > 0) {
			return playerList [0];
		} else {
			return null;
		}
	}

	public bool playerIsNear(){
		List<GameObject> playerList = FindObjectOfType<MapHandler> ().playersOnMap;
		playerList = GridFunc.HittablePlayers (this.gameObject, playerList, range);
		playerList.Sort (CompareDistance);
		if (playerList.Count > 0) {
			return true;
		}else{
			return false;
		}
	}

	public List<GameObject> SortSpotsByNearestPlayer(List<GameObject> spots){
		nearestPlayer = GetNearestPlayer ();
		if (nearestPlayer) {
			spots.Sort (CompareDistanceBetweenTiles);
		}
		return spots;
	}

	void OnDestroy(){
		//FindObjectOfType<MapHandler> ().animationEvent -= OnAnimationPerform;
		if(FindObjectOfType<MapHandler>().enemiesOnMap.Contains(this.gameObject))
			FindObjectOfType<MapHandler> ().enemiesOnMap.Remove (this.gameObject);
		if (FindObjectOfType<AIHandler> ().enemyList.Contains (this.gameObject)) {
			if (FindObjectOfType<AIHandler> ().enemyList [0] == this.gameObject) {
				FindObjectOfType<AIHandler> ().RunNextAI();
			}
			FindObjectOfType<AIHandler> ().enemyList.Remove (this.gameObject);
		}
		if (ait == AiType.basic) {
			FindObjectOfType<MapHandler> ().policemanKilled += 1;
		}
		if (ait == AiType.onlymovement) {
			FindObjectOfType<MapHandler> ().EmployedKilled += 1;
		}
	}

	void OnDisable(){
		//FindObjectOfType<MapHandler> ().animationEvent -= OnAnimationPerform;
		if(FindObjectOfType<MapHandler>().enemiesOnMap.Contains(this.gameObject))
			FindObjectOfType<MapHandler> ().enemiesOnMap.Remove (this.gameObject);
	}
}
