using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid;
/* classe che gestisce la partita e la griglia di gioco*/
public class MapHandler : MonoBehaviour {

	// array 2D di nodi
	public	GameObject[,] grid;
	//dimensione array2D
	public int ColumnH = 100;
	public int RowH = 100;
	//prefab players
	public List<GameObject> players;
	public List<GameObject> targetList;
	//Stato di gioco
	public GameState gs;
	public InputState inputS;
	public GameObject selectedPlayer;

	public GameObject PlayerPin;

	public delegate void ChangeStateDelegate (GameState gState);
	public event ChangeStateDelegate changeStateEvent;

	public delegate void ChangeInputStateDelegate (InputState inpState);
	public event ChangeInputStateDelegate changeInputStateEvent;

	public delegate void SelectedPlayerDelegate (GameObject player);
	public event SelectedPlayerDelegate selectPlayerEvent;

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
		GridFunc.ShowSpawnPoints (grid);
		ChangeState(GameState.Spawn);
		ChangeInputState (InputState.Nothing);
	}

	public IEnumerator EndAiTurn(){
		yield return 2f;
		players = GridFunc.ResetPlayersActions();
		if (players.Count > 0) {
			SelectPlayer (players [0]);
			ChangeInputState (InputState.Decision);
		}
		ChangeState (GameState.AllyTurn);
	}

	public void Spawn(GameObject block){
		GridFunc.SpawnFirstPlayer (players, block);
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

	public void ShowCurrentPlayerMovement(GameObject player){
		SelectPlayer (player);
		ChangeInputState (InputState.Movement);
		Grid.GridMath.ChangeBlocksColour(Color.blue,Grid.GridMath.FindWalkPathInRange(Grid.GridMath.GetPlayerBlock(player),Grid.GridMath.GetPlayerMoveRange(player)));
	}

	public void HideCurrentPlayerMovement(){
		Grid.GridMath.RevertBlocksColour(Grid.GridMath.FindWalkPathInRange(Grid.GridMath.GetPlayerBlock(selectedPlayer),Grid.GridMath.GetPlayerMoveRange(selectedPlayer)));
		ChangeInputState (InputState.Nothing);
	}

	public void MoveCurrentPlayerTo(GameObject block){
		Grid.GridMath.RevertBlocksColour(Grid.GridMath.FindWalkPathInRange(Grid.GridMath.GetPlayerBlock(selectedPlayer),Grid.GridMath.GetPlayerMoveRange(selectedPlayer)));
		Grid.GridMath.MovePlayerToBlock (selectedPlayer, block);
		Player plr = selectedPlayer.GetComponent<Player> ();
		plr.moved = true;
		ChangeInputState (InputState.Decision);
		if (plr.IsDone ()) {
			Grid.GridMath.RemovePlayerFromList (selectedPlayer, players);
			SelectPlayer (null);
			ChangeInputState (InputState.Nothing);
		}
		if (CheckAllyEndTurn ()) {
			ChangeState (GameState.EnemyTurn);
			ChangeInputState (InputState.Nothing);
		}
	}

	public void OpenPlayerMenu(GameObject player){
		SelectPlayer (player);
		ChangeInputState (InputState.Decision);
	}

	public void SelectNothing(GameObject block){
		SelectPlayer (null);
		ChangeInputState (InputState.Nothing);
	}

	public void TargetEnemy(GameObject player){
		targetList = GridFunc.FindEnemyOnMap (grid);
		targetList = GridFunc.HittableEnemies (player, targetList);
		Debug.Log (targetList.Count);
		if (targetList.Count > 0)
		ChangeInputState (InputState.Attack);
		/*Debug.Log (enemylist.Count);
		Ray ray;
		RaycastHit hit;
		GameObject hitted;
		foreach (GameObject enemy in enemylist) {
			ray = new Ray (player.transform.position, enemy.transform.position);
			Debug.DrawRay(player.transform.position, (enemy.transform.position - player.transform.position).normalized,Color.red,4f);
			if (Physics.Raycast (player.transform.position, (enemy.transform.position - player.transform.position).normalized,out hit)) {
				hitted = hit.transform.gameObject;
				Debug.Log (hitted.name);
			}
		}*/
	}

	public void ProvideDamageToPlayer(GameObject playerTrg, int damage){
		Player plr = playerTrg.GetComponent<Player> ();
		plr.currentHP -= damage;
		if(plr.currentHP <= 0){
			players.Clear ();
			GridMath.ChangeBlockType (GridMath.GetPlayerBlock (playerTrg), BlockType.Walkable);
			Destroy (playerTrg);
		}
	}

	public void PassTurn(){
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
		enem.currentHP -= damage;
		if (enem.currentHP <= 0) {
			targetList.Remove (enemy);
			GridMath.ChangeBlockType (GridMath.GetEnemyBlock(enemy), BlockType.Walkable);
			Destroy (enemy);
		}
	}

	public void HitEnemy(GameObject enemy){
		if (targetList.Contains (enemy)) {
			Debug.Log ("hit");
			Player plr = selectedPlayer.GetComponent<Player> ();
			ProvideDamageToEnemy (enemy, plr.firstWeapon.getDamage());
			plr.attacked = true;
			ChangeInputState (InputState.Decision);
			if (plr.IsDone ()) {
				Grid.GridMath.RemovePlayerFromList (selectedPlayer, players);
				SelectPlayer (null);
				ChangeInputState (InputState.Nothing);
			}
			if (CheckAllyEndTurn ()) {
				ChangeState (GameState.EnemyTurn);
				ChangeInputState (InputState.Nothing);
			}
		}
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
		inputS = inpS;
		if (changeInputStateEvent != null)
			changeInputStateEvent (inpS);
	}

	public void SelectPlayer(GameObject player){
		SpawnPin (player);
		selectedPlayer = player;
		if (selectPlayerEvent != null)
			selectPlayerEvent (player);
	}

	public bool CheckAllyEndTurn(){
		if (players.Count <= 0)
			return true;
		else
			return false;
	}

	public void SpawnPin(GameObject player){
		if (player && player != selectedPlayer)
			Instantiate (PlayerPin);
	}
}