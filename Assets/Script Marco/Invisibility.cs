using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour {

    // Variabili da settare nell'inspector

    // Normal
    public GameObject NormalShirt;
    public GameObject NormalBag;
    public GameObject NormalHair;
    public GameObject NormalMask;
    public GameObject NormaleGrenade_1;
    public GameObject NormaleGrenade_2;
    public GameObject NormaleGrenade_3;
    public GameObject NormalWeapon;

    // Invisible
    public GameObject InvisibleShirt;
    public GameObject InvisibleBag;
    public GameObject InvisibleHair;
    public GameObject InvisibleMask;
    public GameObject InvisibleGrenade_1;
    public GameObject InvisibleGrenade_2;
    public GameObject InvisibleGrenade_3;
    public GameObject InvisibleWeapon;

    private void Start()
    {
        // Normal
        NormalShirt.SetActive(true);
        NormalBag.SetActive(true);
        NormalHair.SetActive(true);
        NormalMask.SetActive(true);
        NormaleGrenade_1.SetActive(true);
        NormaleGrenade_2.SetActive(true);
        NormaleGrenade_3.SetActive(true);
        NormalWeapon.SetActive(true);

        // Invisible
        InvisibleShirt.SetActive(false);
        InvisibleBag.SetActive(false);
        InvisibleHair.SetActive(false);
        InvisibleMask.SetActive(false);
        InvisibleGrenade_1.SetActive(false);
        InvisibleGrenade_2.SetActive(false);
        InvisibleGrenade_3.SetActive(false);
        InvisibleWeapon.SetActive(false);
    }

    public void ActiveInvisibility()
    {
        // Normal
        NormalShirt.SetActive(false);
        NormalBag.SetActive(false);
        NormalHair.SetActive(false);
        NormalMask.SetActive(false);
        NormaleGrenade_1.SetActive(false);
        NormaleGrenade_2.SetActive(false);
        NormaleGrenade_3.SetActive(false);
        NormalWeapon.SetActive(false);

        // Invisible
        InvisibleShirt.SetActive(true);
        InvisibleBag.SetActive(true);
        InvisibleHair.SetActive(true);
        InvisibleMask.SetActive(true);
        InvisibleGrenade_1.SetActive(true);
        InvisibleGrenade_2.SetActive(true);
        InvisibleGrenade_3.SetActive(true);
        InvisibleWeapon.SetActive(true);
    }

    public void DeactiveInvisibility()
    {
        // Normal
        NormalShirt.SetActive(true);
        NormalBag.SetActive(true);
        NormalHair.SetActive(true);
        NormalMask.SetActive(true);
        NormaleGrenade_1.SetActive(true);
        NormaleGrenade_2.SetActive(true);
        NormaleGrenade_3.SetActive(true);
        NormalWeapon.SetActive(true);

        // Invisible
        InvisibleShirt.SetActive(false);
        InvisibleBag.SetActive(false);
        InvisibleHair.SetActive(false);
        InvisibleMask.SetActive(false);
        InvisibleGrenade_1.SetActive(false);
        InvisibleGrenade_2.SetActive(false);
        InvisibleGrenade_3.SetActive(false);
        InvisibleWeapon.SetActive(false);
    }
}
