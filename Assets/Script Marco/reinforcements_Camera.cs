using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reinforcements_Camera : MonoBehaviour {

    public void CameraActivate()
    {
        if(GetComponent<Cinematic>() != null)
        {
            GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = true;
            GetComponent<Cinematic>().isRunning = true;
            FakeCamera();
        }
    }

    private IEnumerator FakeCamera()
    {
        yield return new WaitForSeconds(5.0f);
        GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = false;
        GetComponent<Cinematic>().isRunning = false;
    }

}
