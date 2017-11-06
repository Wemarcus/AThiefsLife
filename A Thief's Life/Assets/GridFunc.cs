using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid{
	
public class GridFunc : MonoBehaviour {

		public static void SpawnFirstPlayer(List<GameObject> players, GameObject block){
			if (players.Count > 0) {
				Instantiate (players [0], block.transform);
				players.RemoveAt (0);
				Grid.GridMath.ChangeBlockType (block, BlockType.Player);
			}
		}

		public static void HideSpawnPoints(GameObject[,] grid){
			List<GameObject> spawns = Grid.GridMath.FindSpawnPoints (grid);
			Grid.GridMath.RevertBlocksColour (spawns);
		}

		public static void ShowSpawnPoints(GameObject[,] grid){
			List<GameObject> spawns = Grid.GridMath.FindSpawnPoints (grid);
			Grid.GridMath.ChangeBlocksColour (Color.blue, spawns);
		}

		public static List<GameObject> ResetPlayersActions(){
			List<GameObject> players = Grid.GridMath.FindPlayers ();
			Grid.GridMath.ResetPlayers (players);
			return players;
		}

		public static List<GameObject> FindEnemyOnMap(GameObject[,] grid){
			List<GameObject> list = GridMath.FindBlockType (grid, BlockType.Enemy);
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
			foreach (GameObject enemy in enemies) {
				Debug.DrawRay (player.transform.position, (enemy.transform.position - player.transform.position).normalized, Color.red, 4f);
				if (Physics.Raycast (player.transform.position, (enemy.transform.position - player.transform.position).normalized, out hit)) {
					hitted = hit.transform.gameObject;
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
					if (hitted.gameObject == player)
						hittablePlayers.Add (player);
				}
			}
			return hittablePlayers;
		}
	}
}
