using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour {

	public int damage;
	public BulletTag bulletTag;

    // MODIFICA MARCO => necessario passaggio all'OnTriggerEnter per animazioni..

	/*void OnCollisionEnter(Collision collision){
		Debug.Log ("proiettile collide con :" + collision.gameObject);
		if (collision.gameObject.tag == "Enemy" && bulletTag == BulletTag.friendly) {
			DealDamageToEnemy (collision.gameObject);
			Destroy (this.gameObject);
		}
		if (collision.gameObject.tag == "Player" && bulletTag == BulletTag.foe) {
			DealDamageToPlayer (collision.gameObject);
			Destroy (this.gameObject);
		}
	}*/

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("proiettile collide con :" + other.gameObject);
        if (other.gameObject.tag == "Enemy" && bulletTag == BulletTag.friendly)
        {
            DealDamageToEnemy(other.gameObject);
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Player" && bulletTag == BulletTag.foe)
        {
            DealDamageToPlayer(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    void DealDamageToPlayer( GameObject playerTrg){
		Player plr = playerTrg.GetComponent<Player> ();
		MapHandler mh = FindObjectOfType<MapHandler> ();
		plr.DealDamage (damage);
		if (plr.currentHP <= 0) {
			mh.players.Clear ();
			Grid.GridMath.ChangeBlockType (Grid.GridMath.GetPlayerBlock (playerTrg), BlockType.Walkable);
			Destroy (playerTrg);
		}
	}

	void DealDamageToEnemy(GameObject enemy){
		Enemy enm = enemy.GetComponent<Enemy> ();
		enm.DealDamage (damage);
		MapHandler mh = FindObjectOfType<MapHandler> ();
		if (enm.currentHP <= 0) {
			mh.targetList.Remove (enemy);
			Grid.GridMath.ChangeBlockType (Grid.GridMath.GetEnemyBlock (enemy), BlockType.Walkable);
			Destroy (enemy);
		}
	}
}
