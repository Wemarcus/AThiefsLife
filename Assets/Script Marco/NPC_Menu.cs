using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Menu : MonoBehaviour
{
    public float time;
    public string animation_name;
    public GameObject phone;

    private Animator anim;
    private bool block = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        if(phone != null)
        {
            phone.SetActive(false);
        }
        StartCoroutine(Delay());
    }

    void Update()
    {
        if (!block)
        {
            block = true;
            anim.SetTrigger(animation_name);
            if (phone != null)
            {
                phone.SetActive(true);
            }
            StartCoroutine(Time(time));
        }
    }

    private IEnumerator Time(float time)
    {
        yield return new WaitForSeconds(time);
        if (phone != null)
        {
            phone.SetActive(false);
        }
        yield return new WaitForSeconds(time*2);
        block = false;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(10.0f);
        block = false;
    }
}
