using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int moveRange = 3;
	public int maxHP;
	public int currentHP;
	public bool moved;
	public bool attacked;
	public bool actionDone;
	public Weapon firstWeapon;
	public Weapon secondWeapon;
	public Actions firstAction;
	public Actions secondAction;
	public TextMesh visualHP;
	public GameObject head;
	public GameObject playerBlock;
	public GameObject ShootPoint;
	public GameObject FirstWeaponPhs;
	public List<GameObject> HitZone;
	public bool isBoss;

	// Use this for initialization

	void Start () {
		currentHP = maxHP;
		visualHP = this.GetComponentInChildren<TextMesh> ();
		visualHP.text = currentHP.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int getMoveRange (){
		return moveRange;
	}

	public void DealDamage(int damage){
		Shield sh;
		float floatdamage;
		if (this.gameObject.GetComponent<Shield> ()) {
			sh = this.gameObject.GetComponent<Shield> ();
			floatdamage = (float)damage;
			floatdamage = floatdamage / 100;
			floatdamage = floatdamage * (100 - sh.shieldPercentage);
			damage = (int)floatdamage;
		}
		currentHP -= damage;
		visualHP.text = currentHP.ToString();
	}

	public void Heal(int heal){
		currentHP += heal;
		if (currentHP > maxHP) {
			currentHP = maxHP;
		}
		visualHP.text = currentHP.ToString();
	}

	void OnCollisioneEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Walkable")
			Debug.Log ("walkable detected");
	}

	public void ResetTurn(){
		moved = false;
		attacked = false;
		actionDone = false;
	}

	public bool IsDone(){
		if (moved && attacked && actionDone)
			return true;
		else
			return false;
	}
}
