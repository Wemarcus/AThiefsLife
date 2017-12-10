using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{

    private float fillAmount;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Image health;

    [SerializeField]
    private Text valueText;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            valueText.text = value.ToString();
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        HandleBar();
    }

    private void HandleBar()
    {
        if (fillAmount != health.fillAmount)
        {
            health.fillAmount = Mathf.Lerp(health.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
        }
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
