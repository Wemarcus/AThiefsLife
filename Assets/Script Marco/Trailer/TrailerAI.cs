using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerAI : MonoBehaviour {

    public GameObject boss;
    public GameObject sniper;
    public GameObject tank;
    public GameObject doctor;
    public Transform[] target;
    public GameObject male_uzi;
    public GameObject female_uzi;
    public GameObject grenade;
    public GameObject employee;
    public GameObject caveau_lookAt;

    private bool first_block;
    private bool second_block;

	void Start () {
        boss.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[0]);
        sniper.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[1]);
        tank.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[2]);
        doctor.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[3]);
    }

    public void FirstAction(string name)
    {
        switch (name)
        {
            case "Boss":
                boss.transform.LookAt(male_uzi.transform);
                boss.GetComponent<AgentAnimationTrailer>().firing = true;
                male_uzi.transform.LookAt(boss.transform);
                male_uzi.GetComponent<AgentAnimationTrailer>().firing = true;
                break;

            case "Sniper":
                sniper.transform.LookAt(male_uzi.transform);
                sniper.GetComponent<Animator>().SetBool("Cover", true);
                break;

            case "Doctor":
                doctor.transform.LookAt(female_uzi.transform);
                doctor.GetComponent<AgentAnimationTrailer>().firing = true;
                female_uzi.transform.LookAt(doctor.transform);
                female_uzi.GetComponent<AgentAnimationTrailer>().firing = true;
                break;

            case "Tank":
                tank.GetComponent<Animator>().SetBool("Cover", true);
                break;
        }

        if (!first_block)
        {
            first_block = true;
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(3.0f);
        male_uzi.GetComponent<AgentAnimationTrailer>().death = true;
        female_uzi.GetComponent<AgentAnimationTrailer>().death = true;
        male_uzi.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
        female_uzi.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;

        yield return new WaitForSeconds(1.0f);
        if (!second_block)
        {
            second_block = true;
            sniper.GetComponent<Animator>().SetBool("Cover", false);
            tank.GetComponent<Animator>().SetBool("Cover", false);
            boss.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[4]);
            sniper.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[5]);
            tank.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[6]);
            doctor.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[7]);
            boss.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().second = true;
            sniper.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().second = true;
            tank.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().second = true;
            doctor.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().second = true;
        }
    }

    public void SecondAction(string name)
    {
        switch (name)
        {
            case "Boss":
                boss.transform.LookAt(employee.transform);
                boss.GetComponent<AgentAnimationTrailer>().grenade = true;
                StartCoroutine(Grenade());
                break;

            case "Doctor":
                doctor.transform.LookAt(sniper.transform);
                break;
        }
    }

    private IEnumerator Grenade()
    {
        yield return new WaitForSeconds(1.8f);
        grenade.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        employee.GetComponent<Animator>().SetBool("Death", true);

        boss.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[8]);
        sniper.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[9]);
        tank.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[10]);
        doctor.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[11]);

        boss.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().third = true;
        sniper.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().third = true;
        tank.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().third = true;
        doctor.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().third = true;
    }

    public void ThirdAction(string name)
    {
        switch (name)
        {
            case "Boss":
                boss.transform.LookAt(caveau_lookAt.transform);
                break;

            case "Sniper":
                sniper.transform.LookAt(caveau_lookAt.transform);
                break;

            case "Doctor":
                doctor.transform.LookAt(caveau_lookAt.transform);
                break;
        }
    }

    public void FourthAction()
    {
        boss.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[12]);
        sniper.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[13]);
        tank.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[14]);
        doctor.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[15]);
    }
}
