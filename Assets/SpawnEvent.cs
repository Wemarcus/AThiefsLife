using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEvent : MonoBehaviour {

	public bool eventTriggered;
	public bool done;
	public List<GameObject> spawnPoints;
	public List<GameObject> enemiesToSpawn;
	public List<GameObject> spawnpointTransf; 
	public List<GameObject> objectsToActivate;
	private CaveauEvent ce;
	public int cooldown;
	public int difficultyLevel;

	void Start(){
		ce = FindObjectOfType<CaveauEvent> ();
		FindObjectOfType<MapHandler> ().nextRoundEvent += TurnPassed;
		if (difficultyLevel > FindObjectOfType<MapHandler> ().bankSettings.securityLevel) {
			this.enabled = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (!eventTriggered && ce.EventTriggered && FindObjectOfType<MapHandler> ().gs == GameState.EnemyTurn) {
			eventTriggered = true;
			ActivateObjects ();
		}
		if (eventTriggered && cooldown <= 0 && !done) {
			SpawnFunc ();
			done = true;
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
				Grid.GridFunc.SpawnEnemy(enemiesTemp,spawnToUse[0],spawnpointTransf[0]);
				spawnToUse.RemoveAt(0);
				spawnpointTransf.RemoveAt (0);
			}
		}
	}

	void TurnPassed(int n){
		if(eventTriggered)
			cooldown -= 1;
	}

	void ActivateObjects(){
		foreach (GameObject element in objectsToActivate) {
			element.SetActive (true);
		}
	}

	void OnDisable(){
		FindObjectOfType<MapHandler> ().nextRoundEvent -= TurnPassed;
	}
}
