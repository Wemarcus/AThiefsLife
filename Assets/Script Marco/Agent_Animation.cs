﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent_Animation : MonoBehaviour {

    // Variabili da modificare da altri script
    public bool MyTurn; // se True = mio turno, se False = turno avversario
    public bool firing; // utilizzata per sparare con l'arma primaria
    public bool grenade; // utilizzata per lanciare la bomba
    public bool death; // utilizzata per morire

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
	/*	Per prendere il blocco del player:  Grid.GridMath.GetPlayerBlock(GameObject player);
	 *
	 *	Per prendere il blocco del nemico:  Grid.GridMath.GetEnemyBlock(GameObject enemy);
	 *
	 *  Prendere il componente Node
	 * 
	 *  .isCover = true o false dentro al blocco (node)
	 */ 
    private void Start()
    {
		MapHandler mh;
		mh = FindObjectOfType<MapHandler> ();
		mh.changeStateEvent += SetTurnBool;
        agent = GetComponent<Animator>();
    }

    private void Update()
    {
        if (death)
        {
            death = false;
            agent.SetBool("Death", true);
            time = 3.1f;
            StartCoroutine(Death(time));
        }

        if (firing)
        {
            firing = false;
            agent.SetBool("Firing", true);

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
            agent.SetTrigger("Grenade");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Cover":
                Sphere.SetActive(true);
                break;

            case "Bullet":
                agent.SetTrigger("HitReaction");
                blood.SetActive(true);
                time = 1.00f;
                StartCoroutine(Blood(time));
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case "Cover":
                if (MyTurn)
                {
                    Sphere.SetActive(false);
                } else
                {
                    Sphere.SetActive(true);
                }
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Cover":
                Sphere.SetActive(false);
                break;
        }
    }

    private IEnumerator Death(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(this.gameObject);
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
	}
}
