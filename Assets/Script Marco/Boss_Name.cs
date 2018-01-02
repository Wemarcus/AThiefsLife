using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Name : MonoBehaviour {

    public Text carrier_name;

    private Text boss_name;

    void Start()
    {
        boss_name = GetComponent<Text>();
    }

    public void SetName()
    {
        carrier_name.text = boss_name.text;
    }
}
