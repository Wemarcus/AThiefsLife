using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Management_Menu : MonoBehaviour {

    private Animator anim;

	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (anim.GetBool("Sitting"))
        {
            transform.localPosition = Vector3.Lerp(new Vector3(17.713f, -0.01974356f, -21.205f), new Vector3(17.991f, -0.01974356f, -21.205f), 4.0f);
            //transform.localPosition = new Vector3(17.991f, -0.01974356f, -21.205f); 
        }
        else
        {
            transform.localPosition = Vector3.Lerp(new Vector3(17.991f, -0.01974356f, -21.205f), new Vector3(17.713f, -0.01974356f, -21.205f), 4.0f);
            //transform.localPosition = new Vector3(17.713f, -0.01974356f, -21.205f);
        }
	}
}
