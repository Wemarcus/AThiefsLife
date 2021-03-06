﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeTrailer : MonoBehaviour
{

    public GameObject smoke_effect;
    public float radius = 5.0F;
    public float power = 10.0F;
    public bool is_grenade;
    public bool is_flash;
    public bool is_C4;
    public bool is_smoke;

    private bool block;
    private Vector3 explosionPos;
    private Collider[] colliders;
    private AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        explosionPos = transform.position;
        colliders = Physics.OverlapSphere(explosionPos, radius);

        if (!block && (other.tag == "Player" || other.tag == "Enemy" || other.tag == "Floor"))
        {
            Debug.Log("La granata collide con: " + other.gameObject);

            block = true;
            smoke_effect.SetActive(true);

            if (sound != null)
            {
                sound.Play();
            }

            if (is_grenade || is_C4)
            {
                foreach (Collider hit in colliders)
                {
                    Debug.Log(hit.name);

                    Rigidbody rb = hit.GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        if (rb.tag == "Door")
                        {
                            rb.useGravity = true;
                            rb.isKinematic = false;
                            rb.AddExplosionForce(power*5, explosionPos, radius, 3.0F);
                            Destroy(rb.gameObject, 2.0f);
                        }
                        else if (rb.tag != "Grenade")
                        {
                            rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                        }
                    }
                }
            }

            if (is_flash)
            {
                Destroy(gameObject, 6.0f);
            }
            else if (is_smoke)
            {
                Destroy(gameObject, 13.0f);
            }
            else if (is_grenade)
            {
                Destroy(gameObject, 7.0f);
            }
            else if (is_C4)
            {
                Destroy(gameObject, 4.0f);
            }
        }

    }
}
