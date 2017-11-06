using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int moveRange = 3;
	public int maxHP;
	public int currentHP;
	public bool moved;
	public bool attacked;

	// Use this for initialization

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int getMoveRange (){
		return moveRange;
	}

	void OnCollisioneEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Walkable")
			Debug.Log ("walkable detected");
	}

	public void ResetTurn(){
		moved = false;
		attacked = false;
	}

	public bool IsDone(){
		if (moved && attacked)
			return true;
		else
			return false;
	}
}
