using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInvisibility : MonoBehaviour
{
    public bool invisibility;
    Invisibility inv;

    private void Start()
    {
        inv = GetComponent<Invisibility>();
    }

    void Update()
    {
        if (invisibility)
        {
            inv.ActiveInvisibility();
        }
        else
        {
            inv.DeactiveInvisibility();
        }
    }
}
