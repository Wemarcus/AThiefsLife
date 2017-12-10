using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent_Animation : MonoBehaviour {

    // Variabili da modificare da altri script
    public bool MyTurn; // se True = mio turno, se False = turno avversario
    public bool firing; // utilizzata per sparare con l'arma primaria
    public bool grenade; // utilizzata per lanciare la bomba
    public bool death; // utilizzata per morire
    public bool cover; // utilizzata per il sistema di copertura

    // Variabili da settare nell'inspector
    public GameObject Sphere;
    public GameObject bullet;
    public GameObject sparks;
    public GameObject blood;
    public AudioSource sound;
    public bool is_sniper;
    public bool is_doctor;
    public bool is_tank;

    // Variabili utilizzate solo in questo script
    private Animator agent;
    private float time;
    private MapHandler mh;

    private void Start()
    {
		mh = FindObjectOfType<MapHandler> ();
		mh.changeStateEvent += SetTurnBool;
        agent = GetComponent<Animator>();
        MyTurn = true;
    }

    private void Update()
    {
        if (death)
        {
            death = false;
            agent.SetBool("Death", true);
			FindObjectOfType<MapHandler> ().AnimationPerforming (true);
            time = 3.1f;
            StartCoroutine(Death(time));
        }

        if (firing)
        {
            firing = false;
            agent.SetBool("Firing", true);
			FindObjectOfType<MapHandler> ().AnimationPerforming (true);

            if (is_sniper)
            {
                time = 1.40f;
            }
            else
            {
                time = 3.50f;
            }

            StartCoroutine(Firing(time));
        }

        if (grenade)
        {
            grenade = false;

            if (is_tank)
            {
                Debug.Log("Sono un tank e piazzo un C4");
                time = 5.10f;
                agent.SetBool("C4", true);
                FindObjectOfType<MapHandler>().AnimationPerforming(true);
            }
            else
            {
                Debug.Log("Non sono un tank e lancio una bomba");
                time = 3.20f;
                agent.SetBool("Grenade", true);
                FindObjectOfType<MapHandler>().AnimationPerforming(true);
            }

            StartCoroutine(Grenade(time));
        }

        if (MyTurn)
        {
            Sphere.SetActive(false);
        }
        else if (!MyTurn && cover)
        {
            Sphere.SetActive(true);
        } else if (!MyTurn && !cover)
        {
            Sphere.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Bullet":
                agent.SetTrigger("HitReaction");
                blood.SetActive(true);
                time = 1.00f;
                StartCoroutine(Blood(time));
                break;
        }
    }

    private IEnumerator Death(float t)
    {
        yield return new WaitForSeconds(t);
		mh.changeStateEvent -= SetTurnBool;
		FindObjectOfType<MapHandler> ().AnimationPerforming (false);
		if (FindObjectOfType<MapHandler> ().selectedPlayer == this.gameObject) {
			FindObjectOfType<MapHandler> ().SelectPlayer(mh.players[0]);
		}
		if(FindObjectOfType<AIHandler>().enemyList.Contains(this.gameObject)){
			FindObjectOfType<AIHandler> ().enemyList.Remove (this.gameObject);
		}
        Destroy(this.gameObject);
    }

    private IEnumerator Grenade(float t)
    {
        yield return new WaitForSeconds(t);

        if (is_tank)
        {
            Debug.Log("Sono un tank e finisco animazione");
            agent.SetBool("C4", false);
            FindObjectOfType<MapHandler>().AnimationPerforming(false);
        }
        else
        {
            Debug.Log("Non sono un tank e finisco animazione");
            agent.SetBool("Grenade", false);
            FindObjectOfType<MapHandler>().AnimationPerforming(false);
        }
    }

    private IEnumerator Firing(float t)
    {
        if (bullet != null)
        {
            bullet.SetActive(true);
        }

        if (sparks != null)
        {
            sparks.SetActive(true);
        }

        if (sound != null)
        {
            sound.enabled = true;

            if (is_sniper || is_tank)
            {
                sound.PlayDelayed(1.0f);
            }

            if (is_doctor)
            {
                sound.PlayDelayed(0.5f);
            }
        }

        yield return new WaitForSeconds(t);
        agent.SetBool("Firing", false);
		FindObjectOfType<MapHandler> ().AnimationPerforming (false);

        if (bullet != null)
        {
            bullet.SetActive(false);
        }

        if (sparks != null)
        {
            if (is_sniper)
            {
                yield return new WaitForSeconds(1.0f);
                sparks.SetActive(false);
            } else if (is_tank)
            {
                yield return new WaitForSeconds(0.5f);
                sparks.SetActive(false);
            } else
            {
                sparks.SetActive(false);
            }
        }

        if (sound != null)
        {
            if (is_sniper || is_doctor)
            {
                yield return new WaitForSeconds(1.0f);
                sound.enabled = false;
            } else if (is_tank)
            {
                yield return new WaitForSeconds(0.7f);
                sound.enabled = false;
            } else
            {
                sound.enabled = false;
            }
        }
    }

    private IEnumerator Blood(float t)
    {
        yield return new WaitForSeconds(t);
        blood.SetActive(false);
    }

	private void SetTurnBool(GameState gs){
		if (gs == GameState.AllyTurn && gameObject.GetComponent<Player>()) {
			MyTurn = true;
		}

		if (gs == GameState.EnemyTurn && gameObject.GetComponent<Enemy> ()) {
			MyTurn = true;
		}

		if (gs == GameState.AllyTurn && gameObject.GetComponent<Enemy>()) {
			MyTurn = false;
		}

		if (gs == GameState.EnemyTurn && gameObject.GetComponent<Player> ()) {
			MyTurn = false;
		}
		if(gameObject.GetComponent<Player>()){
			cover = Grid.GridMath.GetPlayerBlock (gameObject).GetComponent<Node>().isCover;
		}
		if(gameObject.GetComponent<Enemy>()){
			cover = Grid.GridMath.GetEnemyBlock (gameObject).GetComponent<Node>().isCover;
		}
	}
}
