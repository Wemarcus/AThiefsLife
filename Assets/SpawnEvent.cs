using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEvent : MonoBehaviour {

	public bool eventTriggered;
	public List<GameObject> spawnPoints;
	public List<GameObject> enemiesToSpawn;
	public GameObject spawnpoint; 
	private CaveauEvent ce;

	void Start(){
		ce = FindObjectOfType<CaveauEvent> ();
	}

	// Update is called once per frame
	void Update () {
		if (!eventTriggered && ce.EventTriggered && FindObjectOfType<MapHandler> ().gs == GameState.EnemyTurn) {
			eventTriggered = true;
			//call spawn func
			SpawnFunc();
		}
	}

	void SpawnFunc(){
		List<GameObject> spawnToUse = new List<GameObject>();
		List<GameObject> enemiesTemp = new List<GameObject> ();
		enemiesTemp.AddRange (enemiesToSpawn);
		Node n;
		foreach (GameObject spawn in spawnPoints) {
			n = spawn.GetComponent<Node> ();
			if (n.blockType == BlockType.Walkable){
				spawnToUse.Add (spawn);
			}
		}

		if (spawnToUse.Count >= enemiesToSpawn.Count) {
			foreach (GameObject enemy in enemiesToSpawn) {
				//spawn enemy
				Debug.Log("spawno");
				Grid.GridFunc.SpawnEnemy(enemiesTemp,spawnToUse[0],spawnpoint);
				spawnToUse.RemoveAt(0);
			}
		}
	}
}
