using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour {

    //private Rigidbody rb;

    public float radius = 5.0F;
    public float power = 10.0F;
    private bool block;

    
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Glass":
                Debug.Log("VETRO URTATO!");
                if (!block)
                {
                    block = true;

                    Vector3 explosionPos = transform.position;
                    Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
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
                            rb.AddExplosionForce(power, explosionPos, radius, 20.0F);
                            //rb.AddForce(transform.forward * 500, ForceMode.Acceleration);
                            Debug.Log("Collision Detected");
                        }
                    }
                    // in prova..
                    Destroy(this.gameObject);
                }
                break;
        }
    }

    // FUNZIONE DI TEST
    /*void OnMouseDown()
    {
        //rb.AddForce(transform.forward * 1000, ForceMode.Acceleration);
        //rb.useGravity = true;

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                if(rb.tag == "Glass")
                {
                    rb.useGravity = true;
                    rb.isKinematic = false;
                }
                rb.AddExplosionForce(power, explosionPos, radius, 20.0F);
            }
        }
    }*/
}