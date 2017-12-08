using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Caveau_Collision : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Caveau")
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<NavMeshObstacle>().enabled = false;
        }
    }
}
