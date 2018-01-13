using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimationTrailer : MonoBehaviour {

    // Variabili da modificare da altri script
    public bool firing; // utilizzata per sparare con l'arma primaria
    public bool grenade; // utilizzata per lanciare la bomba
    public bool death; // utilizzata per morire
    public bool blooding; // utilizzata per dissanguamento
    public bool steal; // utilizzata per derubare

    // Variabili da settare nell'inspector
    public GameObject bullet;
    public GameObject sparks;
    public GameObject blood;
    public AudioSource sound;
    public AudioSource sound_2;
    public bool is_sniper;
    public bool is_doctor;
    public bool is_tank;
    public bool is_swat;

    // Variabili utilizzate solo in questo script
    private Animator agent;
    private float time;

    private bool enable;

    private void Start()
    {
        agent = GetComponent<Animator>();
    }

    private void Update()
    {
        if (death)
        {
            death = false;
            agent.SetBool("Death", true);
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

            if (is_tank)
            {
                Debug.Log("Sono un tank e piazzo un C4");
                time = 5.10f;
                agent.SetBool("C4", true);
            }
            else
            {
                Debug.Log("Non sono un tank e lancio una bomba");
                time = 3.20f;
                agent.SetBool("Grenade", true);
            }

            StartCoroutine(Grenade(time));
        }

        if (blooding)
        {
            blooding = false;
            time = 1.0f;
            StartCoroutine(Blood(time));
        }

        if (steal)
        {
            steal = false;
            if (is_sniper)
            {
                time = 6.0f;
            }
            else
            {
                time = 8.0f;
            }
            StartCoroutine(Stealing(time));
        }
    }

    public IEnumerator Stealing(float t)
    {
        agent.SetBool("Steal", true);
        yield return new WaitForSeconds(t);
        agent.SetBool("Steal", false);
    }

    public IEnumerator Blood(float t)
    {
        blood.SetActive(true);
        yield return new WaitForSeconds(t);
        blood.SetActive(false);
    }

    private IEnumerator Grenade(float t)
    {
        if (!is_tank && sound_2 != null)
        {
            sound_2.PlayDelayed(1.50f);
        }

        yield return new WaitForSeconds(t);

        if (is_tank)
        {
            Debug.Log("Sono un tank e finisco animazione");
            agent.SetBool("C4", false);
        }
        else
        {
            Debug.Log("Non sono un tank e finisco animazione");
            agent.SetBool("Grenade", false);
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

            if (is_sniper || is_tank || is_swat)
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
            }
            else if (is_tank || is_swat)
            {
                yield return new WaitForSeconds(0.5f);
                sparks.SetActive(false);
            }
            else
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
            }
            else if (is_tank || is_swat)
            {
                yield return new WaitForSeconds(0.7f);
                sound.enabled = false;
            }
            else
            {
                sound.enabled = false;
            }
        }
    }
}
