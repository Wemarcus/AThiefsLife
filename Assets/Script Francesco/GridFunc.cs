using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid{
	
public class GridFunc : MonoBehaviour {

		public static void SpawnFirstPlayer(List<GameObject> players, GameObject block){
			if (players.Count > 0) {
				//Instantiate (players [0], block.transform);
				GameObject newPlayer = Instantiate(players[0]);
				newPlayer.transform.position = block.transform.position;
				Player plr = newPlayer.GetComponent<Player> ();
				plr.playerBlock = block;
				//fine nuovo pezzo
				players.RemoveAt (0);
				Grid.GridMath.ChangeBlockType (block, BlockType.Player);
			}
		}

		public static void HideSpawnPoints(GameObject[,] grid){
			List<GameObject> spawns = Grid.GridMath.FindSpawnPoints (grid);
			Grid.GridMath.RevertBlocksColour (spawns);
			Grid.GridMath.DeactivateBlocksMesh (spawns);
		}

		public static void ShowSpawnPoints(GameObject[,] grid){
			List<GameObject> spawns = Grid.GridMath.FindSpawnPoints (grid);
			Grid.GridMath.ChangeBlocksColour (Color.blue, spawns);
			Grid.GridMath.ActivateBlocksMesh (spawns);
		}

		public static List<GameObject> ResetPlayersActions(){
			List<GameObject> players = Grid.GridMath.FindPlayers ();
			Grid.GridMath.ResetPlayers (players);
			return players;
		}

		public static List<GameObject> FindEnemyOnMap(GameObject[,] grid){
			List<GameObject> list = GridMath.FindEnemies (); //GridMath.FindBlockType (grid, BlockType.Enemy);
			List<GameObject> enemyList = new List<GameObject> ();
			foreach (GameObject enemy in list) {
				enemyList.Add (enemy.transform.GetChild (0).gameObject);
			}
			return enemyList;
		}

		public static List<GameObject> HittableEnemies (GameObject player, List<GameObject> enemies){
			RaycastHit hit;
			GameObject hitted;
			List<GameObject> hittableEnemies = new List<GameObject> ();
			Player plr = player.GetComponent<Player> ();
			foreach (GameObject enemy in enemies) {
				Debug.DrawRay (plr.head.transform.position, (enemy.transform.position - player.transform.position).normalized, Color.red, 4f);
				if (Physics.Raycast (plr.head.transform.position, (enemy.transform.position - player.transform.position).normalized, out hit)) {
					hitted = hit.transform.gameObject;
					Debug.Log (hitted.name);
					if (hitted.gameObject == enemy)
						hittableEnemies.Add (enemy);
				}
			}
			return hittableEnemies;
		}

		public static List<GameObject> HittableEnemiesPlus (GameObject player, List<GameObject> enemies){
			Debug.Log ("nemici in scena :" + enemies.Count);
			RaycastHit[] hits;
			Ray ray;
			GameObject hitted;	
			List<GameObject> hittableEnemies = new List<GameObject> ();
			foreach (GameObject enemy in enemies) {
				//Debug.DrawRay (player.transform.position, (enemy.transform.position - player.transform.position).normalized, Color.red, 4f);
				ray = new Ray(player.transform.position,(enemy.transform.position - player.transform.position).normalized);
				Debug.DrawRay (player.transform.position, (enemy.transform.position - player.transform.position).normalized, Color.red, 10f);
				hits = Physics.RaycastAll (ray, 100f);
					foreach (RaycastHit hit in hits) {
						hitted = hit.transform.gameObject;
						Debug.Log ("sto hittando :" + hitted.name);
					if (hitted.gameObject == enemy)
							hittableEnemies.Add (enemy);
					}
			}
			return hittableEnemies;
		}

		public static List<GameObject> HittablePlayers (GameObject enemy, List<GameObject> players){
			RaycastHit hit;
			GameObject hitted;
			List<GameObject> hittablePlayers = new List<GameObject> ();
			foreach (GameObject player in players) {
				Debug.DrawRay (enemy.transform.position, (player.transform.position - enemy.transform.position).normalized, Color.red, 4f);
				if (Physics.Raycast (enemy.transform.position, (player.transform.position - enemy.transform.position).normalized, out hit)) {
					hitted = hit.transform.gameObject;
					Debug.Log (hitted.name);
					if (hitted.gameObject == player)
						hittablePlayers.Add (player);
				}
			}
			return hittablePlayers;
		}
	}
}
