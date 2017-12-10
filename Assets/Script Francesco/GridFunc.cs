using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid{
	
public class GridFunc : MonoBehaviour {

		public static void SpawnFirstPlayer(List<GameObject> players, GameObject block, GameObject spawnpoint){
			if (players.Count > 0) {
				//Instantiate (players [0], block.transform);
				GameObject newPlayer = Instantiate(players[0]);
				GameObject icon = Instantiate( newPlayer.GetComponent<Player> ().iconPrefab,GameObject.Find("PortraitPanel").transform);
				IconHandle iconH = icon.GetComponent<IconHandle> ();
				iconH.LinkIcon (newPlayer);
				newPlayer.transform.position = spawnpoint.transform.position;
				Player plr = newPlayer.GetComponent<Player> ();
				Grid.GridMath.SetPlayerTarget (newPlayer,block);
				plr.playerBlock = block;
				Grid.GridMath.SetBlockToPlayer (newPlayer, block);
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
				enemyList.Add (enemy);//.transform.GetChild (0).gameObject);
			}
			return enemyList;
		}

		public static List<GameObject> HittableEnemies (GameObject player, List<GameObject> enemies, int range){
			RaycastHit hit;
			GameObject hitted;
			List<GameObject> hittableEnemies = new List<GameObject> ();
			Player plr = player.GetComponent<Player> ();
			Enemy enm;
			int percentage;
			float dist;
			foreach (GameObject enemy in enemies) {
				enm = enemy.GetComponent<Enemy> ();
				Debug.DrawRay (plr.head.transform.position, (enm.head.transform.position - plr.head.transform.position).normalized, Color.red, 4f);
				if (Physics.Raycast (plr.head.transform.position, (enm.head.transform.position - plr.head.transform.position).normalized, out hit)) {
					hitted = hit.transform.gameObject;
					Debug.Log (hitted.name);
					dist = Vector3.Distance(player.transform.position, enemy.transform.position);
					if (hitted.gameObject == enemy && dist < range) {
						hittableEnemies.Add (enemy);
						//percentage = CalculateEnemyHitPercentage (plr, enm);
						//Debug.Log(percentage);
					}
				}
			}
			return hittableEnemies;
		}

		public static int CalculateEnemyHitPercentage(Player player, Enemy enemy){
			//spara dall'arma ai 4 punti del corpo 4 raycast
			int percentage = 0;
			RaycastHit hit;
			GameObject hitted;
			foreach (GameObject hitPoint in enemy.HitZone) {
				Debug.DrawRay (player.ShootPoint.transform.position, (hitPoint.transform.position - player.ShootPoint.transform.position).normalized, Color.red, 4f);
				if (Physics.Raycast (player.ShootPoint.transform.position, (hitPoint.transform.position - player.ShootPoint.transform.position).normalized, out hit)) {
					hitted = hit.transform.gameObject;
					if (hitted.gameObject == enemy.gameObject)
						percentage += 100/enemy.HitZone.Count;
				}
			}
			Debug.Log (percentage);
			return percentage;
		}

		public static List<GameObject> HittableEnemiesPlus (GameObject player, List<GameObject> enemies){
			Debug.Log ("nemici in scena :" + enemies.Count);
			Debug.Log (enemies [0]);
			RaycastHit[] hits;
			Ray ray;
			GameObject hitted;	
			Player plr = player.GetComponent<Player> ();
			Enemy enm;
			List<GameObject> hittableEnemies = new List<GameObject> ();
			foreach (GameObject enemy in enemies) {
				enm = enemy.GetComponent<Enemy> ();
				//Debug.DrawRay (player.transform.position, (enemy.transform.position - player.transform.position).normalized, Color.red, 4f);
				ray = new Ray(plr.head.transform.position,(enm.head.transform.position - plr.head.transform.position).normalized);
				Debug.DrawRay (plr.head.transform.position, (enm.head.transform.position - plr.head.transform.position).normalized, Color.red, 10f);
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

		public static List<GameObject> HittablePlayers (GameObject enemy, List<GameObject> players, int range){
			RaycastHit hit;
			GameObject hitted;
			List<GameObject> hittablePlayers = new List<GameObject> ();
			Enemy enm = enemy.GetComponent<Enemy> ();
			Player plr;
			float dist;
			foreach (GameObject player in players) {
				plr = player.GetComponent<Player> ();
				Debug.DrawRay (enm.head.transform.position, (plr.head.transform.position - enm.head.transform.position).normalized, Color.red, 4f);
				if (Physics.Raycast (enm.head.transform.position, (plr.head.transform.position - enm.head.transform.position).normalized, out hit)) {
					hitted = hit.transform.gameObject;
					dist = Vector3.Distance(enemy.transform.position, player.transform.position);
					Debug.Log (hitted.name);
					if (hitted.gameObject == player && !hitted.GetComponent<Hide>() && dist < range)
						hittablePlayers.Add (player);
				}
			}
			return hittablePlayers;
		}
	}
}
