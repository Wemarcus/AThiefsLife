using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Grid{
/* classe per la gestione delle operazioni a basso livello per la griglia di gioco*/
public class GridMath : MonoBehaviour {

		public static void ChangeBlockColour(Color color, GameObject block){	
			Renderer rend = block.GetComponent<Renderer> ();
			rend.material.color = color;
		}

		public static void RevertBlockColour(GameObject block){
			Renderer rend = block.GetComponent<Renderer> ();
			Node n = block.GetComponent<Node> ();
			if (n) {
				rend.material = n.thisMaterial;
			}
		}

		public static void ActivateBlockMesh(GameObject block){
			/*MeshRenderer mr = block.GetComponent<MeshRenderer> ();
			mr.enabled = true;*/
			if (block.GetComponent<cakeslice.Outline>() != null) {
				cakeslice.Outline outline = block.GetComponent<cakeslice.Outline> ();
				outline.enabled = true;
			}
		}

		public static void ActivateBlocksMesh(List<GameObject> blockList){
			foreach (GameObject block in blockList){
				ActivateBlockMesh(block);
			}
		}

		public static void DeactivateBlockMesh(GameObject block){
			/*MeshRenderer mr = block.GetComponent<MeshRenderer> ();
			mr.enabled = false;*/
			if (block.GetComponent<cakeslice.Outline> () != null) {
				cakeslice.Outline outline = block.GetComponent<cakeslice.Outline> ();
				outline.enabled = false;
			}
		}

		public static void DeactivateBlocksMesh(List<GameObject> blockList){
			foreach (GameObject block in blockList){
				DeactivateBlockMesh(block);
			}
		}

		public static void ChangeBlocksColour(Color color, List<GameObject> blockList){
			foreach (GameObject block in blockList) {
				ChangeBlockColour (color, block);
			}
		}

		public static void RevertBlocksColour(List<GameObject> blockList){
			foreach(GameObject block in blockList){
				RevertBlockColour (block);
			}
		}

		public static List<GameObject> FindBlockType(GameObject[,] blockList,BlockType type){
			List<GameObject> blocks = new List<GameObject> ();
			foreach (GameObject i in blockList) {
				if(i){
					Node n = i.GetComponent<Node>();
					if (n.blockType == type)
						blocks.Add (i);
				}
			}
			return blocks;
		}

		public static List<GameObject> FindSpawnPoints(GameObject[,] blockList){
			List<GameObject> spawns = new List<GameObject> ();
			foreach (GameObject i in blockList) {
				if(i){
					Node n = i.GetComponent<Node>();
					if (n.AllySpawn)
						spawns.Add (i);
				}
			}
			return spawns;
		}

		public static GameObject[,] FindGrid(int c, int r){
			List<GameObject> gridList;
			GameObject[,] grid = new GameObject[c,r];
			gridList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Walkable"));
			foreach (GameObject i in gridList) {
				int x = (int)i.transform.position.x;
				int z = (int)i.transform.position.z;
				grid [x,z] = i;
		}
			return grid;
	}

		public static void FindBlockNeighbours(GameObject block, GameObject[,] grid){
			if (block) {
				Node n = block.GetComponent<Node> ();
				if (n)
					n.FindNeighbours (grid);
			}
		}

		public static void SetGridNeighbours(GameObject[,] grid){
			foreach (GameObject block in grid) {
				FindBlockNeighbours (block,grid);
			}
		}

		public static void ChangeBlockType(GameObject block, BlockType type){
			if (block) {
				Node n = block.GetComponent<Node> ();
				if (n)
					n.blockType = type;
			}
		}

		public static void RotateCharacter(GameObject character, GameObject destination){
			character.transform.LookAt (destination.transform);
		}

		public static List<GameObject> FindPlayers(){
			List<GameObject> players = new List<GameObject> ();
			players = GameObject.FindGameObjectsWithTag("Player").ToList();
			return players;
		}

		public static List<GameObject> FindEnemies(){
			List<GameObject> enemies = new List<GameObject> ();
			enemies = GameObject.FindGameObjectsWithTag ("Enemy").ToList ();
			return enemies;
		}

		public static bool PlayerIsActive(List<GameObject> players,GameObject player){
			bool flag = false;
			if (players.Contains (player))
				flag = true;
			return flag;
		}

		public static List<GameObject> DepthVisit(GameObject block, int depth, int currentDepth){
			if (currentDepth == 0) {
				Node thisNode = block.GetComponent<Node> ();
				thisNode.visited = true;
			}
			List<GameObject> graph = new List<GameObject> ();
			Node n = block.GetComponentInParent<Node> ();
			foreach (GameObject node in n.Neighbours) {
				Node currentNode = node.GetComponent<Node> ();
				if (currentNode.visited == false && currentDepth<depth) {
					//visit node
					graph.Add(node);
					currentNode.visited = true;
				}
				if(currentDepth<depth)
				graph.AddRange( DepthVisit (node, depth, currentDepth+1));
			}
			return graph;
		}

		public static List<GameObject> DepthVisitWalkable(GameObject block, int depth, int currentDepth){
			if (currentDepth == 0) {
				Node thisNode = block.GetComponent<Node> ();
				thisNode.visited = true;
			}
			List<GameObject> graph = new List<GameObject> ();
			Node n = block.GetComponentInParent<Node> ();
			foreach (GameObject node in n.Neighbours) {
				Node currentNode = node.GetComponent<Node> ();
				if (currentNode.visited == false && currentDepth<depth && currentNode.blockType == BlockType.Walkable) {
					//visit node
					graph.Add(node);
					currentNode.visited = true;
				}
				if(currentDepth<depth && currentNode.blockType == BlockType.Walkable)
					graph.AddRange( DepthVisitWalkable (node, depth, currentDepth+1));
			}
			return graph;
		}

		public static List<GameObject> DepthVisitWalkable2(GameObject block, int depth, int currentDepth){
			if (currentDepth == 0) {
				Node thisNode = block.GetComponent<Node> ();
				thisNode.visited = true;
			}
			List<GameObject> graph = new List<GameObject> ();
			Node n = block.GetComponentInParent<Node> ();
			graph = visitNeighbours (graph, block, depth, currentDepth + 1);
			return graph;
		}

		public static List<GameObject> visitNeighbours(List<GameObject> graph,GameObject block, int depth, int currentDepth){
			Node n = block.GetComponentInParent<Node> ();
			n.visited = true;
			n.depth = currentDepth;
			graph.Add (block);
			foreach (GameObject node in n.Neighbours) {
				Node currentNode = node.GetComponent<Node> ();
				if ((currentNode.visited == false && currentDepth <= depth && currentNode.blockType == BlockType.Walkable)|| currentNode.depth > currentDepth && currentNode.blockType == BlockType.Walkable) {
					visitNeighbours (graph, node, depth, currentDepth + 1);
				}
			}
			return graph;
		}

		public static List<GameObject> FindPathTo(GameObject block, int depth, int currentDepth, GameObject playerblock){//TODO da fare
			//visita nodo, se è player, ritorna path
			List<GameObject> graph = new List<GameObject>();
			Node thisNode = block.GetComponent<Node>();
			if (thisNode.visited == false && currentDepth < depth && thisNode.blockType == BlockType.Walkable) {
				thisNode.visited = true;
				graph.Add (block);
				if (block == playerblock)
					return graph;
				//se non è player, visita gli altri nodi
				Node n = block.GetComponentInParent<Node> ();
				foreach (GameObject node in n.Neighbours) {
					Node currentNode = node.GetComponent<Node> ();
					if (currentDepth < depth && currentNode.blockType == BlockType.Walkable)
						graph.AddRange (DepthVisitWalkable (node, depth, currentDepth + 1));
				}
			}
			return graph;
		}


		public static void SetBlockListUnvisited(List<GameObject> blockList){
			Node n;
			foreach (GameObject node in blockList) {
				n = node.GetComponent<Node> ();
				n.visited = false;
			}
		}

		public static void SetBlockUnvisited(GameObject block){
			Node n = block.GetComponent<Node> ();
			n.visited = false;
		}

		public static List<GameObject> FindBlocksInRange(GameObject block, int depth){
			List<GameObject> blockList = new List<GameObject> ();
			blockList = DepthVisit (block, depth, 0);
			SetBlockListUnvisited (blockList);
			SetBlockUnvisited (block);
			ResetDepth (blockList);
			return blockList;
		}

		public static List<GameObject> FindWalkPathInRange(GameObject block, int depth){
			List<GameObject> blockList = new List<GameObject> ();
			blockList = DepthVisitWalkable2 (block, depth, 0);
			blockList.Remove (block);
			SetBlockListUnvisited (blockList);
			SetBlockUnvisited (block);
			//Debug.Log (blockList.Count ());
			return blockList;
		}

		public static void ResetDepth(List<GameObject> blocklist){
			Node n;
			foreach (GameObject block in blocklist){
				n = block.GetComponent<Node> ();
				n.depth = 0;
			}
		}

		public static GameObject GetPlayerBlock(GameObject player){
			//return (player.transform.parent.gameObject); TODO
			Player plr = player.GetComponent<Player>();
			return (plr.playerBlock);
		}

		public static GameObject GetEnemyBlock(GameObject enemy){
			Enemy enm = enemy.GetComponent<Enemy> ();
			return (enm.block);
			//return (enemy.transform.parent.gameObject);
		}

		public static int GetPlayerMoveRange(GameObject player){
			Player plr = player.GetComponent<Player> ();
			int range = plr.getMoveRange ();
			return range;
		}

		public static void RemovePlayerFromBlock(GameObject player){
			Player plr = player.GetComponent<Player>();
			GameObject block = GetPlayerBlock (player);
			Node n = block.GetComponent<Node> ();
			if(n)
			n.player = null;
		}

		public static void SetBlockToPlayer(GameObject player, GameObject block){
			Player plr = player.GetComponent<Player>();
			Node n = block.GetComponent<Node> ();
			n.player = player;
		}

		public static void MovePlayerToBlock(GameObject player, GameObject block){
			Vector3 localS = player.transform.localScale;
			Grid.GridMath.ChangeBlockType (Grid.GridMath.GetPlayerBlock (player), BlockType.Walkable);
			RemovePlayerFromBlock (player);
			//player.transform.SetParent (block.transform);
			Player plr = player.GetComponent<Player>();
			plr.playerBlock = block;
			SetBlockToPlayer (player, block);
			UnityStandardAssets.Characters.ThirdPerson.AICharacterControl charcontrol = player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl> ();
			charcontrol.target = block.transform;
			// fine nuovo pezzo
			//player.transform.localPosition = new Vector3 (0, 1.5f, 0);
			//player.transform.localScale = localS;
			Grid.GridMath.ChangeBlockType (block, BlockType.Player);
		}

		// TODO TODO TODO
		public static void MoveEnemyToBlock(GameObject enemy, GameObject block){
			//Vector3 localS = enemy.transform.localScale;
			Grid.GridMath.ChangeBlockType (Grid.GridMath.GetEnemyBlock (enemy), BlockType.Walkable);
			//enemy.transform.SetParent (block.transform);
			//enemy.transform.localPosition = new Vector3 (0, 1.5f, 0);
			//enemy.transform.localScale = localS;
			Enemy enm = enemy.GetComponent<Enemy>();
			enm.block = block;
			UnityStandardAssets.Characters.ThirdPerson.AICharacterControl charcontrol = enemy.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl> ();
			charcontrol.target = block.transform;
			//fine nuovo pezzo
			Grid.GridMath.ChangeBlockType (block, BlockType.Enemy);
		}

		public static void RemovePlayerFromList(GameObject player, List<GameObject> playerList){
			playerList.Remove (player);
		}

		public static void ResetPlayers(List<GameObject> players){
			foreach (GameObject plr in players) {
				ResetPlayer (plr);
			}
		}

		public static void ResetPlayer(GameObject player){
			Player plr = player.GetComponent<Player> ();
			plr.ResetTurn ();
		}
}
}
