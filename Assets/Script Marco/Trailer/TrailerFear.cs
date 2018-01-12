using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerFear : MonoBehaviour {

    public GameObject[] employee;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject man in employee)
            {
                man.GetComponent<Animator>().SetBool("Crouch", true);
            }
        }
    }
}
