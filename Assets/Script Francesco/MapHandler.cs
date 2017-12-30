using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid;
using MessagePopUp;

/* classe che gestisce la partita e la griglia di gioco*/
public class MapHandler : MonoBehaviour {

	// array 2D di nodi
	public	GameObject[,] grid;
	//dimensione array2D
	public int ColumnH = 100;
	public int RowH = 100;
	//prefab players
	public List<GameObject> spawnpoints;
	public List<GameObject> players;
	public List<GameObject> targetList;
	//Stato di gioco
	public GameState gs;
	public InputState inputS;
	public GameObject selectedPlayer;
	public Weapon selectedWeapon;
	public Actions selectedAction;

	//public GameObject PlayerPin;
	//public GameObject EnemyPin;

	public GameObject CurrentTarget;

	public bool PerformingAction;

	public int turnCount = 1;
	public int money;

	public UnityEngine.UI.Text popUp;

	public List<GameObject> enemiesOnMap;
	public List<GameObject> playersOnMap;

	public List<GameObject> spawnPointsOnMap;
	public List<GameObject> enemySpawnPointsOnMap;

	public ConfirmPanelHandler cph;

	public int policemanKilled;
	public int EmployedKilled;

	public bool pause;
	public PauseMenu pm;

	public GameObject bossDeathPanel;

	public delegate void ChangeStateDelegate (GameState gState);
	public event ChangeStateDelegate changeStateEvent;

	public delegate void ChangeInputStateDelegate (InputState inpState);
	public event ChangeInputStateDelegate changeInputStateEvent;

	public delegate void SelectedPlayerDelegate (GameObject player);
	public event SelectedPlayerDelegate selectPlayerEvent;

	public delegate void CurrentTargetDelegate (GameObject newTarget);
	public event CurrentTargetDelegate currentTargetEvent;

	public delegate void AnimationDelegate (bool isAction);
	public event AnimationDelegate animationEvent;

	public delegate void NextRound(int n);
	public event NextRound nextRoundEvent;

	public delegate void AddMoney(int n);
	public event AddMoney addMoneyEvent;

	// Use this for initialization
	void Start () {
		//linko i nodi esistenti all'array
		SetupGrid ();
		//preparo lo spawn
		SetupSpawn ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void SetupSpawn(){
		//GridFunc.ShowSpawnPoints (grid);
		ChangeState(GameState.Spawn);
		ChangeInputState (InputState.Nothing);
		SpawnPlayers ();
		NextTurn (turnCount);
	}

	public IEnumerator EndAiTurn(){
		yield return 2f;
		players = GridFunc.ResetPlayersActions();
		if (players.Count > 0) {
			SelectPlayer (players [0]);
			ChangeInputState (InputState.Decision);
		}
		ChangeState (GameState.AllyTurn);
		NextTurn (turnCount);
	}

	public void Spawn(GameObject block){
		GridFunc.SpawnFirstPlayer (players, block,spawnpoints[0]);
		spawnpoints.Remove (spawnpoints [0]);
		if(players.Count <= 0) {
			players = GridFunc.ResetPlayersActions();
			if (players.Count > 0) {
				SelectPlayer (players [0]);
				ChangeInputState (InputState.Decision);
			}
			GridFunc.HideSpawnPoints (grid);
			ChangeState(GameState.AllyTurn);
		}
	}

	public void SwitchPause(){
		pause = !pause;
		if (pause) {
			pm.pauseMenu.SetActive (true);
			Time.timeScale = 0;
			AudioListener.pause = true;
		} else {
			Time.timeScale = 1;			
			AudioListener.pause = false;
			pm.pauseMenu.SetActive (false);
		}
	}

	public void SpawnPlayers(){
		List<GameObject> spawns = new List<GameObject> ();
		spawns.AddRange (spawnPointsOnMap);
		List<GameObject> playersToSpawn = new List<GameObject>();
		playersToSpawn.AddRange (players);
		foreach (GameObject player in playersToSpawn) {
			GridFunc.SpawnFirstPlayer (players, spawns [0], spawnpoints[0]);
			spawns.Remove (spawns [0]);
			spawnpoints.Remove (spawnpoints [0]);
		}
		players = GridFunc.ResetPlayersActions();
		SelectPlayer (players [0]);
		ChangeInputState (InputState.Decision);
		GridFunc.HideSpawnPoints (grid);
		ChangeState(GameState.AllyTurn);
	}

	public void ShowCurrentPlayerMovement(GameObject player){
		StartCoroutine (MessagePopUp.MessagePopUp.ShowMessage("Choose where to move your character",popUp));
		SelectPlayer (player);
		ChangeInputState (InputState.Movement);
		List<GameObject> pathList = Grid.GridMath.FindWalkPathInRange(Grid.GridMath.GetPlayerBlock(player),Grid.GridMath.GetPlayerMoveRange(player));
		//Grid.GridMath.ChangeBlocksColour (Color.blue,pathList);
		Grid.GridMath.ActivateBlocksMesh (pathList);
		//Grid.GridMath.ChangeBlocksColour(Color.blue,Grid.GridMath.FindWalkPathInRange(Grid.GridMath.GetPlayerBlock(player),Grid.GridMath.GetPlayerMoveRange(player)));
	}

	public void HideCurrentPlayerMovement(){
		List<GameObject> pathList = Grid.GridMath.FindWalkPathInRange(Grid.GridMath.GetPlayerBlock(selectedPlayer),Grid.GridMath.GetPlayerMoveRange(selectedPlayer));
		//Grid.GridMath.RevertBlocksColour (pathList);
		Grid.GridMath.DeactivateBlocksMesh (pathList);
		//Grid.GridMath.RevertBlocksColour(Grid.GridMath.FindWalkPathInRange(Grid.GridMath.GetPlayerBlock(selectedPlayer),Grid.GridMath.GetPlayerMoveRange(selectedPlayer)));
		ChangeInputState (InputState.Decision);
	}

	public void RevertAbility(GameObject player){
		List<GameObject> pathList = Grid.GridMath.FindWalkPathInRangeWithPlayers(Grid.GridMath.GetPlayerBlock(selectedPlayer),Grid.GridMath.GetPlayerMoveRange(selectedPlayer));
		Grid.GridMath.DeactivateBlocksMesh (pathList);
		Grid.GridMath.ResetDepth (pathList);
		selectedAction = null;
		ChangeInputState (InputState.Decision);
	}

	public void MoveCurrentPlayerTo(GameObject block){
		List<GameObject> pathList = Grid.GridMath.FindWalkPathInRange(Grid.GridMath.GetPlayerBlock(selectedPlayer),Grid.GridMath.GetPlayerMoveRange(selectedPlayer));
		//Grid.GridMath.RevertBlocksColour (pathList);
		Grid.GridMath.ResetDepth(pathList); // AGGIUNTO NUOVO PER RESET
		Grid.GridMath.DeactivateBlocksMesh (pathList);
		//Grid.GridMath.RevertBlocksColour(Grid.GridMath.FindWalkPathInRange(Grid.GridMath.GetPlayerBlock(selectedPlayer),Grid.GridMath.GetPlayerMoveRange(selectedPlayer)));
		Grid.GridMath.MovePlayerToBlock (selectedPlayer, block);
		Player plr = selectedPlayer.GetComponent<Player> ();
		plr.moved = true;
		ChangeInputState (InputState.Decision);
		StartCoroutine(CheckDoneAndEndTurn(plr));
		/*if (plr.IsDone ()) {
			Grid.GridMath.RemovePlayerFromList (selectedPlayer, players);
			SelectPlayer (null);
			if(players.Count >0)
				SelectPlayer (players [0]);
			ChangeInputState (InputState.Decision);
		}
		if (CheckAllyEndTurn ()) {
			ChangeState (GameState.EnemyTurn);
			ChangeInputState (InputState.Nothing);
		}*/
	}

	public IEnumerator CheckDoneAndEndTurn(Player plr){
		yield return new WaitForSeconds (1.5f);
		if (plr.IsDone ()) {
			plr.LookNearestEnemy ();
			Grid.GridMath.RemovePlayerFromList (selectedPlayer, players);
			SelectPlayer (null);
			if(players.Count >0)
				SelectPlayer (players [0]);
			ChangeInputState (InputState.Decision);
		}
		if (CheckAllyEndTurn ()) {
			ChangeState (GameState.EnemyTurn);
			ChangeInputState (InputState.Nothing);
		}
	}

	public void PerformAction(GameObject player){
		selectedAction.RunAction (selectedPlayer);
	}

	public void OpenPlayerMenu(GameObject player){
		SelectPlayer (player);
		ChangeInputState (InputState.Decision);
	}

	public void SelectNothing(GameObject block){
		//SelectPlayer (null);
		ChangeTarget (null);
		ChangeInputState (InputState.Decision);
	}

	public void WeaponTarget(GameObject player, Weapon wpn){
		if (wpn.getWeaponType () == WeaponType.Single) {
			selectedWeapon = wpn;
			TargetEnemy (player);
		}
		if (wpn.getWeaponType () == WeaponType.AoE && wpn.type != AoEType.c4 && wpn.internalCD > wpn.cooldown) {
			selectedWeapon = wpn;
			TargetEnemyAoE (player);
		} 
		if (wpn.getWeaponType () == WeaponType.AoE && wpn.type == AoEType.c4 && wpn.internalCD > wpn.cooldown) {
			selectedWeapon = wpn;
			PlaceC4 (player, wpn);
		} 
	}

	private IEnumerator PlaceC4Cor(Weapon wpn, GameObject player){
		yield return new WaitForSeconds (2.0f);
			wpn.internalCD = 0;
			GameObject c4 = (GameObject)Instantiate (wpn.bombPrefab);
			c4.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + 0.1f, player.transform.position.z);
			c4 compc4 = c4.GetComponent<c4> ();
			compc4.Setc4 (wpn.damage, wpn.range);
	}

	public void PlaceC4(GameObject player,Weapon wpn){
		Player plr = selectedPlayer.GetComponent<Player> ();
		if (wpn.internalCD > wpn.cooldown) {
			/*GameObject c4 = (GameObject)Instantiate (wpn.bombPrefab);
		c4.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y +0.1f, player.transform.position.z);
		c4 compc4 = c4.GetComponent<c4> ();
		compc4.Setc4 (wpn.damage, wpn.range);*/
			Agent_Animation aa = plr.gameObject.GetComponent<Agent_Animation> ();
			aa.grenade = true;
			StartCoroutine (PlaceC4Cor (wpn, player));
			if (selectedPlayer.GetComponent<DoubleAttack> ()) {
				DoubleAttack da = selectedPlayer.GetComponent<DoubleAttack> ();
				Destroy (da);
			} else {
				plr.attacked = true;
			}
			ChangeTarget (null);
			ChangeInputState (InputState.Decision);
			StartCoroutine(CheckDoneAndEndTurn(plr));
			/*if (plr.IsDone ()) {
				Grid.GridMath.RemovePlayerFromList (selectedPlayer, players);
				SelectPlayer (null);
				if(players.Count >0)
				SelectPlayer (players [0]);
				ChangeInputState (InputState.Decision);
			}
			if (CheckAllyEndTurn ()) {
				ChangeState (GameState.EnemyTurn);
				ChangeInputState (InputState.Nothing);
			}*/
		}
	}

	public void TargetEnemy(GameObject player){
		targetList = GridFunc.FindEnemyOnMap (grid);
		targetList = GridFunc.HittableEnemiesSortedByRange (player, targetList, selectedWeapon.range);
		//Debug.Log (targetList.Count);
		//EnemyPin enPin;
		/*foreach (GameObject enemy in targetList) {
			enPin = Instantiate (EnemyPin).GetComponent<EnemyPin> ();
			enPin.SetupPin (enemy);
		}*/
		if (targetList.Count > 0) {
			StartCoroutine (MessagePopUp.MessagePopUp.ShowMessage ("Choose an enemy to hit", popUp));
			//enPin = Instantiate (EnemyPin).GetComponent<EnemyPin> ();
			//enPin.SetupPin (targetList[0]);
			//CurrentTarget = (targetList [0]);
			ChangeTarget(targetList[0]);
			Grid.GridMath.RotateCharacter (selectedPlayer, targetList [0]);
			ChangeInputState (InputState.Attack);
		} else {
			StartCoroutine (MessagePopUp.MessagePopUp.ShowMessage ("There is any enemy in your line of sight", popUp));
			selectedWeapon = null;
		}
	}

	public void TargetEnemyAoE(GameObject player){
		targetList = GridFunc.FindEnemyOnMap (grid);
		targetList = GridFunc.HittableEnemiesSortedByRange (player, targetList, selectedWeapon.range);
		//Debug.Log (targetList.Count);
		//EnemyPin enPin;
		/*foreach (GameObject enemy in targetList) {
			enPin = Instantiate (EnemyPin).GetComponent<EnemyPin> ();
			enPin.SetupPin (enemy);
		}*/
		if (targetList.Count > 0) {
			StartCoroutine (MessagePopUp.MessagePopUp.ShowMessage ("Choose an enemy to hit", popUp));
			//enPin = Instantiate (EnemyPin).GetComponent<EnemyPin> ();
			//enPin.SetupPin (targetList[0]);
			//CurrentTarget = (targetList [0]);
			GameObject grenadeRange = Instantiate(Resources.Load("GrenadeRange", typeof(GameObject))) as GameObject;
			ChangeTarget(targetList[0]);
			Grid.GridMath.RotateCharacter (selectedPlayer, targetList [0]);
			ChangeInputState (InputState.Attack);
		} else {
			StartCoroutine (MessagePopUp.MessagePopUp.ShowMessage ("There is any enemy in your line of sight", popUp));
			selectedWeapon = null;
		}
	}

	public void ProvideDamageToPlayer(GameObject playerTrg, int damage){
		Player plr = playerTrg.GetComponent<Player> ();
		plr.DealDamage (damage);
		/*if(plr.currentHP <= 0){
			players.Clear ();
			GridMath.ChangeBlockType (GridMath.GetPlayerBlock (playerTrg), BlockType.Walkable);
			Destroy (playerTrg);
		}*/
	}

	public void ClickSwitchTurn(){
		cph.SwitchTurn ();
	}

	public void PassAllyTurn(){
		/*ChangeInputState (InputState.Nothing);
		ChangeState (GameState.EnemyTurn);
		SelectPlayer (null);*/
		StartCoroutine (Pass ());
	}

	private IEnumerator Pass(){
		selectedPlayer = null;
		ChangeInputState (InputState.Nothing);
		yield return new WaitForSeconds (2f);
		Player plr;
		foreach (GameObject player in playersOnMap) {
			plr = player.GetComponent<Player> ();
			plr.LookNearestEnemy ();
		}
		selectedPlayer = null;
		ChangeInputState (InputState.Nothing);
		ChangeState (GameState.EnemyTurn);
	}

	public void PassPlayerTurn(){
		Player plr = selectedPlayer.GetComponent<Player> ();
		plr.attacked = true;
		plr.moved = true;
		if(players.Contains(selectedPlayer)){
			players.Remove(selectedPlayer);
			ChangeInputState (InputState.Nothing);
		}
		SelectPlayer (null);
		if (CheckAllyEndTurn ()) {
			ChangeState (GameState.EnemyTurn);
			ChangeInputState (InputState.Nothing);
		}
	}

	public void ProvideDamageToEnemy(GameObject enemy,int damage){
		Enemy enem = enemy.GetComponent<Enemy> ();
		//Player plr = selectedPlayer.GetComponent<Player> ();
		enem.DealDamage(damage);
		/*if (enem.currentHP <= 0) {
			targetList.Remove (enemy);
			GridMath.ChangeBlockType (GridMath.GetEnemyBlock(enemy), BlockType.Walkable);
			Destroy (enemy);
		}*/
	}

	public void FireBulletToEnemy(GameObject target, GameObject starting,int damage){
		Player plr = selectedPlayer.GetComponent<Player> ();
		Enemy enm = target.GetComponent<Enemy> ();
		Agent_Animation aa = plr.gameObject.GetComponent<Agent_Animation> ();
		aa.firing = true;
		int rand = UnityEngine.Random.Range (0, enm.HitZone.Count - 1);
		GameObject bullet = (GameObject)Instantiate(selectedWeapon.bulletPrefab,plr.ShootPoint.transform.position,plr.ShootPoint.transform.rotation);
		if (plr.gameObject.GetComponent<DamagePowerUp> ()) {
			float damage2 = damage;
			damage2 = damage2 / 100;
			damage2 = damage2 * (100 + plr.gameObject.GetComponent<DamagePowerUp> ().damagePercentage);
			damage = (int)damage2;
		}
		if (plr.gameObject.GetComponent<Killer> ()) {
			damage = 999;
		}
		BuildBullet (bullet, damage, BulletTag.friendly);
		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = (enm.HitZone[rand].transform.position - starting.transform.position)* 1f;     
	}

	public void FireBulletToPlayer(GameObject target, GameObject starting, int damage){
		Player plr = target.GetComponent<Player> ();
		Enemy enm = starting.GetComponent<Enemy> ();
		Agent_Animation aa = enm.gameObject.GetComponent<Agent_Animation> ();
		aa.firing = true;
		int rand = UnityEngine.Random.Range (0, plr.HitZone.Count - 1);
		GameObject bullet = (GameObject)Instantiate(enm.bulletPrefab,enm.shootPoint.transform.position,enm.shootPoint.transform.rotation);
		BuildBullet (bullet, damage, BulletTag.foe);
		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = (plr.HitZone[rand].transform.position - enm.shootPoint.transform.position)* 1f;    
	}

	public void ThrowBombToPlayer(GameObject enemy,GameObject player, GameObject bombPrefab, int damage, int range, int cdDuration){
		Agent_Animation aa = enemy.gameObject.GetComponent<Agent_Animation> ();
		aa.grenade = true;
		StartCoroutine(ThrowBombToPlayerCor (player,bombPrefab,damage,range,cdDuration));
	}

	public IEnumerator ThrowBombToPlayerCor(GameObject player, GameObject bombPrefab, int damage, int range, int cdDuration){
		yield return new WaitForSeconds (2f);
		Player plr = player.GetComponent<Player> ();
		GameObject bomb = (GameObject)Instantiate (bombPrefab);
		bomb.transform.position = new Vector3 (plr.head.transform.position.x, plr.head.transform.position.y + 0.5f, plr.head.transform.position.z);
		Gas gs = bomb.AddComponent (typeof(Gas)) as Gas;
		gs.SetBomb (damage, range, cdDuration);
	}

	public void BuildBullet(GameObject bullet, int damage, BulletTag bt){
		BulletDamage bd = bullet.GetComponent<BulletDamage> ();
		bd.damage = damage;
		bd.bulletTag = bt;
	}

	public void HitEnemy(GameObject enemy){
		if (selectedWeapon.wpnType == WeaponType.Single) {
			HitSingleTarget (enemy);
		}else
		if(selectedWeapon.wpnType == WeaponType.AoE){
			Debug.Log("AOE attack");
			HitAoE (enemy);
		}
	}

	public void HitSingleTarget(GameObject enemy){
		if (CurrentTarget == enemy){//targetList.Contains (enemy)) {
			//Debug.Log ("hit");
			Player plr = selectedPlayer.GetComponent<Player> ();
			//ProvideDamageToEnemy (enemy,selectedWeapon.getDamage());
			FireBulletToEnemy(enemy,plr.ShootPoint,selectedWeapon.getDamage());
			if (selectedPlayer.GetComponent<DoubleAttack> ()) {
				DoubleAttack da = selectedPlayer.GetComponent<DoubleAttack> ();
				Destroy (da);
			} else {
				plr.attacked = true;
			}
			ChangeTarget (null);
			ChangeInputState (InputState.Decision);
			StartCoroutine(CheckDoneAndEndTurn(plr));
			/*if (plr.IsDone ()) {
				Grid.GridMath.RemovePlayerFromList (selectedPlayer, players);
				SelectPlayer (null);
				if(players.Count >0)
				SelectPlayer (players [0]);
				ChangeInputState (InputState.Decision);
			}
			if (CheckAllyEndTurn ()) {
				ChangeState (GameState.EnemyTurn);
				ChangeInputState (InputState.Nothing);
			}*/
		}
	}

	public void HitAoE(GameObject enemy){
		if (CurrentTarget == enemy) {
			Player plr = selectedPlayer.GetComponent<Player> ();
			selectedWeapon.PerformAction (enemy);
			if (selectedPlayer.GetComponent<DoubleAttack> ()) {
				DoubleAttack da = selectedPlayer.GetComponent<DoubleAttack> ();
				Destroy (da);
			} else {
				plr.attacked = true;
			}
			ChangeTarget (null);
			ChangeInputState (InputState.Decision);
			StartCoroutine(CheckDoneAndEndTurn(plr));
			/*if (plr.IsDone ()) {
				Grid.GridMath.RemovePlayerFromList (selectedPlayer, players);
				SelectPlayer (null);
				if(players.Count >0)
					SelectPlayer(players[0]);
				ChangeInputState (InputState.Decision);
			}
			if (CheckAllyEndTurn ()) {
				ChangeState (GameState.EnemyTurn);
				ChangeInputState (InputState.Nothing);
			}*/
		}
	}

	public void BossDeath(){
		Time.timeScale = 0;
		AudioListener.pause =false;
		ChangeState (GameState.End);
		ChangeInputState (InputState.Nothing);
		bossDeathPanel.SetActive (true);

	}

	void SetupGrid(){
		ChangeState(GameState.Init);
		grid = new GameObject[ColumnH,RowH];
		grid = Grid.GridMath.FindGrid (ColumnH, RowH);
		Grid.GridMath.SetGridNeighbours(grid);
	}

	public void ChangeState(GameState gameS){
		gs = gameS;
		if (changeStateEvent != null)
			changeStateEvent (gs);
	}

	public void ChangeInputState(InputState inpS){
		if (inpS == InputState.Decision) {
			selectedWeapon = null;
			selectedAction = null;
		}
		inputS = inpS;
		if (changeInputStateEvent != null)
			changeInputStateEvent (inpS);
	}

	public void SelectPlayer(GameObject player){
		//SpawnPin (player);
		selectedPlayer = player;
		if (selectPlayerEvent != null)
			selectPlayerEvent (player);
	}

	public void NextTurn(int n){
		if (nextRoundEvent != null) {
			turnCount += 1;
			nextRoundEvent (n);
		}
	}

	public void ChangeTarget(GameObject newTarget){
		if (currentTargetEvent != null) {
			CurrentTarget = newTarget;
			currentTargetEvent (newTarget);
		}
	}

	public void AnimationPerforming(bool b){
		PerformingAction = b;
		if (animationEvent != null) {
			animationEvent (b);
		}
	}

	public void StealMoney(int n){
		money += n;
		if (addMoneyEvent != null) {
			addMoneyEvent (n);
		}
	}

	public bool CheckAllyEndTurn(){
		if (players.Count <= 0)
			return true;
		else
			return false;
	}
		
	/*public void SpawnPin(GameObject player){
		if (player && player != selectedPlayer)
			Instantiate (PlayerPin);
	}*/
}