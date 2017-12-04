using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent_Animation : MonoBehaviour {

    public GameObject Sphere;
    public bool MyTurn; // se True = mio turno, se False = turno avversario
    //private Animator agent;


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Cover":
                if (!MyTurn)
                {
                    Sphere.SetActive(true);
                }
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Cover":
                Sphere.SetActive(false);
                break;
        }
    }
}
