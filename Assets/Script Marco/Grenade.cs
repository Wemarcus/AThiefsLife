using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    public GameObject smoke_effect;
    public AudioSource sound;
    public float radius = 5.0F;
    public float power = 10.0F;
    public bool is_grenade;
    public bool is_flash;
    public bool is_C4;
    public bool is_smoke;

    private bool block;
    private Vector3 explosionPos;
    private Collider[] colliders;

    private void OnTriggerEnter(Collider other)
    {
        explosionPos = transform.position;
        colliders = Physics.OverlapSphere(explosionPos, radius);

        Debug.Log("La granata collide con: " + other.gameObject);

        if (!block && (other.tag == "Player" || other.tag == "Enemy"))
        {
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
                        if (rb.tag == "Glass")
                        {
                            rb.useGravity = true;
                            rb.isKinematic = false;
                            rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                        } else if(rb.tag != "Grenade")
                        {
                            rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                        }
                    }
                }
            }

            if (is_flash)
            {
                Destroy(transform.parent.gameObject, 6.0f);
            }
            else if (is_smoke)
            {
                Destroy(transform.parent.gameObject, 13.0f);
            }
            else if (is_grenade)
            {
                Destroy(transform.parent.gameObject, 7.0f);
            }
            else if (is_C4)
            {
                Destroy(transform.parent.gameObject, 4.0f);
            }
        }

    }
}
