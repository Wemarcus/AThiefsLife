using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour {
    // Parte Francesco
	public int damage;
	public BulletTag bulletTag;

    // Parte Marco
    public float radius = 5.0F;
    public float power = 10.0F;
    private bool block;
    private Vector3 explosionPos;
    private Collider[] colliders;

    // MODIFICA MARCO => necessario passaggio all'OnTriggerEnter per animazioni.. e fusione con AddForce.cs

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

    /*void OnTriggerEnter(Collider other)
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
    }*/

    private void Update()
    {
        //Destroy(this.gameObject, 3.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        explosionPos = transform.position;
        colliders = Physics.OverlapSphere(explosionPos, radius);

        Debug.Log("Il proiettile collide con :" + other.gameObject);

        switch (other.tag)
        {
            case "Glass":
                if (!block)
                {
                    block = true;

                    foreach (Collider hit in colliders)
                    {
                        Rigidbody rb = hit.GetComponent<Rigidbody>();

                        if (rb != null)
                        {
                            if (rb.tag == "Glass")
                            {
                                rb.useGravity = true;
                                rb.isKinematic = false;
                            }
                            //rb.AddExplosionForce(power, explosionPos, radius, 20.0F);
                            rb.AddForce(transform.right * 100, ForceMode.Acceleration);
                            Debug.Log("Explosive force applied to Glass");
                        }
                    }
                    Destroy(this.gameObject);
                }
                break;

            case "Props":
                if (!block)
                {
                    block = true;

                    foreach (Collider hit in colliders)
                    {
                        Rigidbody rb = hit.GetComponent<Rigidbody>();

                        if (rb != null)
                        {
                            //rb.AddExplosionForce(power, explosionPos, radius, 20.0F);
                            rb.AddForce(transform.right * 500, ForceMode.Acceleration);
                            Debug.Log("Explosive force applied to Props");
                        }
                    }
                    Destroy(this.gameObject);
                }
                break;

            case "Wall":
                Destroy(this.gameObject);
                break;

            case "Cover":
                Destroy(this.gameObject);
                break;

            case "Enemy":
                if (bulletTag == BulletTag.friendly)
                {
                    DealDamageToEnemy(other.gameObject);
                    Destroy(this.gameObject);
                }
                break;

            case "Player":
                if (bulletTag == BulletTag.foe)
                {
                    DealDamageToPlayer(other.gameObject);
                    Destroy(this.gameObject);
                }
                break;
        }
    }

    void DealDamageToPlayer( GameObject playerTrg){
		Player plr = playerTrg.GetComponent<Player> ();
		MapHandler mh = FindObjectOfType<MapHandler> ();
		plr.DealDamage (damage);
		/*if (plr.currentHP <= 0) {
			mh.players.Clear ();
			Grid.GridMath.ChangeBlockType (Grid.GridMath.GetPlayerBlock (playerTrg), BlockType.Walkable);
			Destroy (playerTrg);
		}*/
	}

	void DealDamageToEnemy(GameObject enemy){
		Enemy enm = enemy.GetComponent<Enemy> ();
		enm.DealDamage (damage);
		MapHandler mh = FindObjectOfType<MapHandler> ();
		/*if (enm.currentHP <= 0) {
			mh.targetList.Remove (enemy);
			Grid.GridMath.ChangeBlockType (Grid.GridMath.GetEnemyBlock (enemy), BlockType.Walkable);
			Destroy (enemy);
		}*/
	}
}
