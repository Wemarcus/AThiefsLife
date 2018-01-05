using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHandler : MonoBehaviour {

	public GameState gs;
	public List<GameObject> enemyList;
	public MapHandler mh;

	void OnEnable(){
		FindObjectOfType<MapHandler> ().changeStateEvent += ChangeState;
	}

	public void ChangeState(GameState gameS){
		gs = gameS;
		if (IsEnemyTurn ()) {
			Debug.Log ("turno nemico");
			enemyList.AddRange(mh.enemiesOnMap);//Grid.GridMath.FindEnemies ();
			//Enemy enem;
			/////RunNextAI ();
			StartCoroutine(RunNextAICor());
			/*foreach (GameObject enemy in enemyList) {
				enem = enemy.GetComponent<Enemy> ();
				enem.RunAI ();
			}
			StartCoroutine(mh.EndAiTurn ());*/
		}
	}

	IEnumerator RunNextAICor(){
		yield return new WaitForSeconds(3f);
		if(IsEnemyTurn())
		RunNextAI();
	}

	public void RunNextAI(){
		Enemy enem;
		if (enemyList.Count > 0) {
			enem = enemyList [0].GetComponent<Enemy> ();
			enem.RunAI ();
			enemyList.RemoveAt (0);
		} else {
			StartCoroutine (mh.EndAiTurn ());
		}
	}

	private bool IsEnemyTurn(){
		if (gs == GameState.EnemyTurn) {
			return true;
		}
		else return false;
	}
}
