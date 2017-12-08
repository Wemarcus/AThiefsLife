using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Caveau_Collision : MonoBehaviour {

    private bool touch;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Caveau")
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<NavMeshObstacle>().enabled = false;

            touch = true;
        }

        if ((other.tag == "Player" || other.tag == "Enemy") && touch)
        {
            Destroy(gameObject, 3.0f);
        }
    }
}
