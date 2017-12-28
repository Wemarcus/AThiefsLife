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
	public GameObject head;
	public GameObject playerBlock;
	public GameObject ShootPoint;
	public GameObject FirstWeaponPhs;
	public List<GameObject> HitZone;
	public bool isBoss;
	public GameObject iconPrefab;
	public GameObject damagePopUpPrefab;
	public GameObject spawnPointDamagePopUpPrefab;
	public GameObject portrait;

	// Use this for initialization

	void Start () {
		currentHP = maxHP;
		if (!FindObjectOfType<MapHandler> ().playersOnMap.Contains (this.gameObject)) {
			FindObjectOfType<MapHandler> ().playersOnMap.Add (this.gameObject);
			//FindObjectOfType<EndStatsHandle> ().portraitList.Add (portrait);
		}
	}

	void OnEnable(){
		//FindObjectOfType<MapHandler> ().animationEvent += OnAnimationPerform;
		if(!FindObjectOfType<MapHandler>().playersOnMap.Contains(this.gameObject))
			FindObjectOfType<MapHandler> ().playersOnMap.Add (this.gameObject);
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
		GameObject pop = Instantiate (damagePopUpPrefab,spawnPointDamagePopUpPrefab.transform);
		DamageUI popui = pop.GetComponent<DamageUI> ();
		popui.SetText (damage.ToString ());
		//pop.transform.position = spawnPointDamagePopUpPrefab.transform.position;
		if (currentHP < 0) {
			currentHP = 0;
		}
		if (currentHP <= 0) {
			FindObjectOfType<MapHandler> ().players.Remove (this.gameObject);
			if (FindObjectOfType<MapHandler> ().players.Count <= 0 && FindObjectOfType<MapHandler>().gs == GameState.AllyTurn) {
				FindObjectOfType<MapHandler> ().ChangeState (GameState.EnemyTurn);
			}
			Grid.GridMath.ChangeBlockType (Grid.GridMath.GetPlayerBlock (this.gameObject), BlockType.Walkable);
			gameObject.GetComponent<Agent_Animation> ().death = true;
			//Destroy (playerTrg);
		}
	}

	public void Heal(int heal){
		currentHP += heal;
		GameObject panel = transform.Find ("Ally_Interface").gameObject;
		GameObject effect = Instantiate(Resources.Load("Heal", typeof(GameObject)),panel.transform) as GameObject;
		if (currentHP > maxHP) {
			currentHP = maxHP;
		}
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

	/*public void OnAnimationPerform(bool b){
		if (!b) {
			//StartCoroutine (LookEnemy ());
		}
	}*/

	public void LookNearestEnemy(){
		StartCoroutine (LookEnemy ());
	}

	private IEnumerator LookEnemy(){
		yield return new WaitForSeconds (0.3f);
		Grid.GridFunc.LookNearestEnemy (this.gameObject, FindObjectOfType<MapHandler> ().enemiesOnMap , this.firstWeapon.range);
	}

	public int CompareDistance(GameObject a, GameObject b) {
		Enemy enemy_a = a.GetComponent<Enemy> ();
		Enemy enemy_b = b.GetComponent<Enemy> ();
		Player plr = this;
		float distance_a =  Vector3.Distance(plr.transform.position, enemy_a.transform.position);
		float distance_b =  Vector3.Distance(plr.transform.position, enemy_b.transform.position);
		if(distance_a >= distance_b) {
			return 1;
		}
		else {
			return -1;
		}
	}

	void OnDestroy(){
		//FindObjectOfType<MapHandler> ().animationEvent -= OnAnimationPerform;
		if (FindObjectOfType<MapHandler> ().playersOnMap.Contains (this.gameObject)) {
			FindObjectOfType<MapHandler> ().playersOnMap.Remove (this.gameObject);
		}
	}

	void OnDisable(){
		//FindObjectOfType<MapHandler> ().animationEvent -= OnAnimationPerform;
		if (FindObjectOfType<MapHandler> ().playersOnMap.Contains (this.gameObject)) {
			FindObjectOfType<MapHandler> ().playersOnMap.Remove (this.gameObject);
		}
	}
}
