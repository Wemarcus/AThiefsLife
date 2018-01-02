using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team_Management_Animation : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	public void Clicked()
    {
        anim.SetTrigger("SelectedPlayer");
    }
}
