using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reinforcements_Camera : MonoBehaviour {

    public void CameraActivate()
    {
        GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = true;
        FakeCamera();
    }

    private IEnumerator FakeCamera()
    {
        yield return new WaitForSeconds(5.0f);
        GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = false;
    }

}
