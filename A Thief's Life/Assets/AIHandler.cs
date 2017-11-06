using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHandler : MonoBehaviour {

	public GameState gs;
	List<GameObject> enemyList;
	public MapHandler mh;

	void OnEnable(){
		FindObjectOfType<MapHandler> ().changeStateEvent += ChangeState;
	}

	public void ChangeState(GameState gameS){
		gs = gameS;
		if (IsEnemyTurn ()) {
			Debug.Log ("turno nemico");
			enemyList = Grid.GridMath.FindEnemies ();
			Enemy enem;
			foreach (GameObject enemy in enemyList) {
				enem = enemy.GetComponent<Enemy> ();
				enem.RunAI ();
			}
			StartCoroutine(mh.EndAiTurn ());
		}
	}

	private bool IsEnemyTurn(){
		if (gs == GameState.EnemyTurn) {
			return true;
		}
		else return false;
	}
}
