using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealUI : MonoBehaviour
{

    private Animator anim;
    private Text heal;

    void Start()
    {
        Animator anim = GetComponent<Animator>();
        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
    }

    void Awake()
    {
        heal = this.gameObject.GetComponent<Text>();

    }

    public void SetText(string text)
    {
        heal.text = text;
    }
}
