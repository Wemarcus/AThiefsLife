﻿using System.Collections;
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
    }
}