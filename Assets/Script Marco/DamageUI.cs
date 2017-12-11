using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour {

    private Animator anim;
    private Text damage;

	// Use this for initialization
	void Start () {
		//damage = this.gameObject.GetComponent<Text> ();
        Animator anim = GetComponent<Animator>();
        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        //damage = anim.GetComponent<Text>();
	}

	void Awake(){
		damage = this.gameObject.GetComponent<Text> ();

	}
	
	public void SetText(string text)
    {
        damage.text = text;
    }
}
