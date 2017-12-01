using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public WeaponType wpnType;
	public int damage;
	public int range;
	public GameObject bulletPrefab;

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
