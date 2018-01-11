using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpeed : MonoBehaviour {

	public void FastForward()
    {
        Time.timeScale = 2.0F;
    }

    public void NormalSpeed()
    {
        Time.timeScale = 1.0F;
    }
}
