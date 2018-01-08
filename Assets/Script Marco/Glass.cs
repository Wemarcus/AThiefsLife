using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour {

    private Rigidbody rb;
    private bool block;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Props" && !block)
        {
            block = true;

            rb.useGravity = true;
            rb.isKinematic = false;
            rb.AddForce(transform.right * 100, ForceMode.Acceleration);

            Debug.Log("Collisione tra un Props ed un Glass");
        }

        if (other.tag == "Enemy" && !block)
        {
            block = true;

            rb.useGravity = true;
            rb.isKinematic = false;
            rb.AddForce(transform.right * 100, ForceMode.Acceleration);

            Debug.Log("Collisione tra un agente della SWAT ed un Glass");

            Destroy(gameObject, 4.0f);
        }

        if (other.tag == "Floor")
        {
            Debug.Log("Collisione tra un Floor ed un Glass");

            Destroy(gameObject, 10.0f);
        }

        if (other.tag == "Bullet")
        {
            Debug.Log("Collisione tra un Bullet ed un Glass");

            Destroy(gameObject, 10.0f);
        }

        if (other.tag == "Player")
        {
            Debug.Log("Collisione tra un Player ed un Glass");

            Destroy(gameObject, 4.0f);
        }
    }
}
