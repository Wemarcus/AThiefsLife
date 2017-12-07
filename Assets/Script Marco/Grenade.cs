using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    public GameObject smoke_effect;
    public AudioSource sound;
    public float radius = 5.0F;
    public float power = 10.0F;
    public bool is_explosive;

    private bool block;
    private Vector3 explosionPos;
    private Collider[] colliders;

    private void Update()
    {
        if (is_explosive)
        {
            Destroy(transform.parent.gameObject, 10.0f);
        }
        else
        {
            Destroy(transform.parent.gameObject, 15.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        explosionPos = transform.position;
        colliders = Physics.OverlapSphere(explosionPos, radius);

        Debug.Log("La granata collide con: " + other.gameObject);

        if (other.tag == "Player" || other.tag == "Enemy")
        {
            gameObject.SetActive(false);
            smoke_effect.SetActive(true);

            if (sound != null)
            {
                sound.Play();
            }

            if (!block && is_explosive)
            {
                block = true;

                foreach (Collider hit in colliders)
                {
                    Debug.Log(hit.name);

                    Rigidbody rb = hit.GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        if (rb.tag == "Glass")
                        {
                            rb.useGravity = true;
                            rb.isKinematic = false;
                        }
                        rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                    }
                }
            }
        }

    }
}
