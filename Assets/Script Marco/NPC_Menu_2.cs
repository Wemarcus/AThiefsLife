using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Menu_2 : MonoBehaviour {

    public string animation_name;

    private Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool(animation_name, true);
    }

}
