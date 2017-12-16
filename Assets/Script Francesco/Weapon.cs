using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AoEType{
	frag,
	gas,
	flash,
	c4
}

public class Weapon : MonoBehaviour {

	public string wpnName;
	public string wpnDescription;
	public WeaponType wpnType;
	public int damage;
	public int range;
	public GameObject bulletPrefab;
	public Sprite wpnImage;
	public Sprite selectedWpnImage;

	[Header("Aoe Setup(only if weapon type is aoe)")]

	public AoEType type;
	public int cooldown;
	public int internalCD;
	MapHandler mh;
	public GameObject bombPrefab;
	public int cdDuration;

	void Start(){
		if(wpnType == WeaponType.AoE)
			internalCD = cooldown +1;
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

	public bool IsOnCooldown(){
		bool b;
		if (internalCD <= cooldown && wpnType == WeaponType.AoE) {
			b = true;
		} else {
			b = false;
		}
		return b;
	}

	public void c4(){
	}

	public void Flash(GameObject enemy){
			internalCD = 0;
			Debug.Log ("sono in flash");
			Enemy enm = enemy.GetComponent<Enemy> ();
			Player plr = FindObjectOfType<MapHandler> ().selectedPlayer.GetComponent<Player> ();
			Agent_Animation aa = plr.gameObject.GetComponent<Agent_Animation> ();
			aa.grenade = true;
			StartCoroutine (SpawnFlash (enemy));
			/*GameObject bomb = (GameObject)Instantiate (bombPrefab, enm.head.transform);
		Flash fh = bomb.AddComponent (typeof(Flash)) as Flash;
		fh.SetBomb (range, cdDuration);*/
	}

	private IEnumerator SpawnFlash(GameObject enemy){
		yield return new WaitForSeconds (2f);
		Enemy enm = enemy.GetComponent<Enemy> ();
		GameObject bomb = (GameObject)Instantiate (bombPrefab);
		bomb.transform.position = new Vector3 (enm.head.transform.position.x, enm.head.transform.position.y + 0.5f, enm.head.transform.position.z);
		Flash fh = bomb.AddComponent (typeof(Flash)) as Flash;
		fh.SetBomb (range, cdDuration);
	}

	public void Gas(GameObject enemy){
			internalCD = 0;
			Debug.Log ("sono in gas");
			Enemy enm = enemy.GetComponent<Enemy> ();
			Player plr = FindObjectOfType<MapHandler> ().selectedPlayer.GetComponent<Player> ();
			Agent_Animation aa = plr.gameObject.GetComponent<Agent_Animation> ();
			aa.grenade = true;
			StartCoroutine (SpawnGas (enemy));
			/*GameObject bomb = (GameObject)Instantiate (bombPrefab, enm.head.transform);
		Gas gs = bomb.AddComponent (typeof(Gas)) as Gas;
		gs.SetBomb (damage, range, cdDuration);*/
	}

	private IEnumerator SpawnGas(GameObject enemy){
		yield return new WaitForSeconds (2f);
		Enemy enm = enemy.GetComponent<Enemy> ();
		GameObject bomb = (GameObject)Instantiate (bombPrefab);
		bomb.transform.position = new Vector3 (enm.head.transform.position.x, enm.head.transform.position.y + 0.5f, enm.head.transform.position.z);
		Gas gs = bomb.AddComponent (typeof(Gas)) as Gas;
		gs.SetBomb (damage, range, cdDuration);
	}

	public void Frag(GameObject enemy){
			internalCD = 0;
			Debug.Log ("sono in frag");
			Enemy enm = enemy.GetComponent<Enemy> ();
			Player plr = FindObjectOfType<MapHandler> ().selectedPlayer.GetComponent<Player> ();
			Agent_Animation aa = plr.gameObject.GetComponent<Agent_Animation> ();
			aa.grenade = true;
			StartCoroutine (SpawnFrag (enemy));
			/*GameObject bomb = (GameObject)Instantiate (bombPrefab, enm.head.transform);
		Frag frg = bomb.AddComponent (typeof(Frag)) as Frag;
		frg.SetBomb (damage, range);*/
	}

	private IEnumerator SpawnFrag(GameObject enemy){
		yield return new WaitForSeconds (2f);
		Enemy enm = enemy.GetComponent<Enemy> ();
		GameObject bomb = (GameObject)Instantiate (bombPrefab);
		bomb.transform.position = new Vector3 (enm.head.transform.position.x, enm.head.transform.position.y + 0.5f, enm.head.transform.position.z);
		Frag frg = bomb.AddComponent (typeof(Frag)) as Frag;
		frg.SetBomb (damage, range);
	}

	public void CoolDown(int n){
		internalCD += 1;
		FindObjectOfType<UIHandler> ().UpdateUI ();
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

	public int GetDamageWithBuff(){
		int newDamage = damage;
		Player plr = this.gameObject.GetComponentInParent<Player> ();
		if (plr.gameObject.GetComponent<DamagePowerUp> ()) {
			float damage2 = damage;
			damage2 = damage2 / 100;
			damage2 = damage2 * (100 + plr.gameObject.GetComponent<DamagePowerUp> ().damagePercentage);
			newDamage = (int)damage2;
		}
		return newDamage;
	}
}
