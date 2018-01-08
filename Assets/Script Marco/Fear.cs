using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fear : MonoBehaviour {

    public Transform target;
    public AudioSource audio_fear;
    public GameObject[] innocent;	

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // attivare sound
            audio_fear.Play(); 

            foreach (GameObject man in innocent)
            {
                man.GetComponent<UnityStandardAssets.Characters.ThirdPerson.PeopleFear>().SetTarget(target);
            }

            // distruggere anche sound
            Destroy(gameObject);
        }
    }
}
