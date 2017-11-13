using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid;
using UnityEngine.EventSystems;

/* classe che gestisce i click dell'utente */

public class ClickHandle : MonoBehaviour {

	delegate void clickDelegate(GameObject block);
	clickDelegate click;
	public GameState gs;
	public InputState inpS = InputState.Nothing;
	public GameObject player;
	public MapHandler mh;
	public bool enable = true;

	// Use this for initialization
	void OnEnable () {
		mh.changeStateEvent += ChangeState;
		mh.changeInputStateEvent += ChangeInputState;
		mh.selectPlayerEvent += ChangeSelectedPlayer;
	}
		
	// Update is called once per frame
	void Update () {
		if (EventSystem.current.IsPointerOverGameObject(-1))    // is the touch on the GUI
		{
			// GUI Action
			return;
		}
		if (Input.GetMouseButtonDown (0) && enable) { 
			RaycastHit hit; 
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); 
			GameObject hitted;
			if (Physics.Raycast (ray, out hit, 100.0f)) {
				StartCoroutine(ScaleMe(hit.transform));
				hitted = hit.transform.gameObject;
				BuildClickDelegate (hitted);
				RunDelegate (hitted);
			}
		}
	}

	void OnMouseOver(){ // TODO delete
		Debug.Log (gameObject.name);
	}

	IEnumerator ScaleMe(Transform objTr) {
		if (objTr) 
			objTr.localScale *= 1.2f;
			yield return new WaitForSeconds (0.1f);
		if (objTr) 
			objTr.localScale /= 1.2f;
	}
	public void BuildClickDelegate(GameObject hitted){
		click = null;
		switch (gs) {
		case GameState.Spawn:
			Node nd = hitted.GetComponent<Node> ();
			if (nd && nd.AllySpawn && nd.blockType == BlockType.Walkable)
				click += SpawnFuncBuild;
			break;
		case GameState.AllyTurn:
			if (hitted.tag == "Player" && Grid.GridMath.PlayerIsActive (mh.players, hitted) && (inpS == InputState.Nothing || inpS == InputState.Decision)) {
				click += SelectPlayer;
				break;
			}
			if (hitted.tag == "Walkable" && inpS == InputState.Movement && Grid.GridMath.FindWalkPathInRange (Grid.GridMath.GetPlayerBlock (player), Grid.GridMath.GetPlayerMoveRange (player)).Contains (hitted)) {
				click += MoveTo;
				break;
			}
			if (hitted.tag == "Walkable" && inpS == InputState.Movement && !Grid.GridMath.FindWalkPathInRange (Grid.GridMath.GetPlayerBlock (player), Grid.GridMath.GetPlayerMoveRange (player)).Contains (hitted)) {
				click += RevertMoveTo;
				break;
			}
			if (hitted.tag == "Walkable" && (inpS == InputState.Decision || inpS == InputState.Attack)) {
				click += SelectNothing;
				break;
			}
			if (hitted.tag == "Player" && inpS == InputState.Movement) {
				click += RevertMoveTo;
				click += SelectPlayer;
				break;
			}
			if (hitted.tag == "Enemy" && inpS == InputState.Attack) {
				click += Hit;
				break;
			}
			break;
		}
	}
		
	public void RunDelegate(GameObject block){
		if (click != null) {
			click (block);
			click = null;
		}
	}

	public void Hit(GameObject enemy){
		FindObjectOfType<MapHandler> ().HitEnemy (enemy);
	}

	public void MoveTo(GameObject block){
		FindObjectOfType<MapHandler> ().MoveCurrentPlayerTo (block);
	}

	public void RevertMoveTo(GameObject block){
		FindObjectOfType<MapHandler> ().HideCurrentPlayerMovement ();
	}

	public void StartMovement(GameObject player){
		FindObjectOfType<MapHandler> ().ShowCurrentPlayerMovement (player);
	}

	public void SpawnFuncBuild(GameObject block){
		FindObjectOfType<MapHandler> ().Spawn (block);
	}

	public void SelectPlayer(GameObject player){
		FindObjectOfType<MapHandler> ().OpenPlayerMenu (player);
	}

	public void SelectNothing(GameObject block){
		FindObjectOfType<MapHandler> ().SelectNothing (block);
	}

	public void SetEnable(bool b){
		enable = b;
	}

	public void ChangeState(GameState gameS){
		gs = gameS;
	}

	public void ChangeInputState(InputState inptS){
		inpS = inptS; 
	}

	public void ChangeSelectedPlayer(GameObject player2){
		player = player2;
	}
}
