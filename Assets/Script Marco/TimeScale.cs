using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour {

    public GameObject door;

	public void IncreaseTimeScale()
    {
        Time.timeScale = 6.0F;
        StartCoroutine(NormalTime());
    }

    public void DecreaseTimeScale()
    {
        Time.timeScale = 1.0F;
    }

    private IEnumerator NormalTime()
    {
        yield return new WaitForSeconds(30.0f);
        DecreaseTimeScale();
        door.GetComponent<SlerpRotation>().OpenDoor();
    }
}
