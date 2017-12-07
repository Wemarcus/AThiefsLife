using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AoEType{
	frag,
	gas,
	flash,
	c4
}

public class Weapon : MonoBehaviour {

	public WeaponType wpnType;
	public int damage;
	public int range;
	public GameObject bulletPrefab;

	[Header("Aoe Setup(only if weapon type is aoe")]

	public AoEType type;
	public int cooldown;
	MapHandler mh;
	public GameObject bombPrefab;
	public int cdDuration;

	void Start(){
		cooldown = 2;
		mh = FindObjectOfType<MapHandler> ();
		mh.nextRoundEvent += CoolDown;
	}

	public void PerformAction(GameObject enemy){
		switch (type) {
		case AoEType.frag:
			Frag (enemy);
			break;
		case AoEType.gas:
			Gas (enemy);
			break;
		case AoEType.flash:
			Flash (enemy);
			break;
		case AoEType.c4:
			break;
		}
	}

	public void c4(){
	}

	public void Flash(GameObject enemy){
		Debug.Log ("sono in flash");
		Enemy enm = enemy.GetComponent<Enemy> ();
		GameObject bomb = (GameObject)Instantiate (bombPrefab, enm.head.transform);
		Flash fh = bomb.AddComponent (typeof(Flash)) as Flash;
		fh.SetBomb (range, cdDuration);
	}

	public void Gas(GameObject enemy){
		Debug.Log ("sono in gas");
		Enemy enm = enemy.GetComponent<Enemy> ();
		GameObject bomb = (GameObject)Instantiate (bombPrefab, enm.head.transform);
		Gas gs = bomb.AddComponent (typeof(Gas)) as Gas;
		gs.SetBomb (damage, range, cdDuration);
	}

	public void Frag(GameObject enemy){
		Debug.Log ("sono in frag");
		Enemy enm = enemy.GetComponent<Enemy> ();
		GameObject bomb = (GameObject)Instantiate (bombPrefab, enm.head.transform);
		Frag frg = bomb.AddComponent (typeof(Frag)) as Frag;
		frg.SetBomb (damage, range);
	}

	public void CoolDown(int n){
		cooldown += 1;
	}

	public int getDamage(){
		return damage;
	}

	public int getRange(){
		return range;
	}
		
	public WeaponType getWeaponType(){
		return wpnType;
	}
}
