using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerAI : MonoBehaviour
{

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
    public GameObject drill;
    public GameObject[] bag_empty_lookAt;
    public GameObject[] bag_medium;
    public GameObject[] bag_full;
    public GameObject[] bag_fake;
    public GameObject[] weapon;
    public GameObject[] money;
    public GameObject[] enemies_car;
    public GameObject helicopter;
    public GameObject escape_point;

    private bool first_block;
    private bool second_block;
    private bool third_block;
    private bool fourth_block;

    void Start()
    {
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
        yield return new WaitForSeconds(2.0f);
        male_uzi.GetComponent<AgentAnimationTrailer>().blooding = true;
        female_uzi.GetComponent<AgentAnimationTrailer>().blooding = true;
        yield return new WaitForSeconds(1.0f);
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
                boss.transform.LookAt(drill.transform);
                StartCoroutine(OpenCaveu());
                break;

            case "Sniper":
                sniper.transform.LookAt(caveau_lookAt.transform);
                break;

            case "Doctor":
                doctor.transform.LookAt(caveau_lookAt.transform);
                break;
        }
    }

    private IEnumerator OpenCaveu()
    {
        boss.GetComponent<Animator>().SetTrigger("OpenCaveau");
        yield return new WaitForSeconds(1.0f);
        boss.GetComponent<Animator>().SetTrigger("OpenCaveau");
        yield return new WaitForSeconds(1.0f);
        boss.GetComponent<Animator>().SetTrigger("OpenCaveau");
        yield return new WaitForSeconds(1.0f);
        boss.GetComponent<Animator>().SetTrigger("OpenCaveau");
    }

    public void FourthAction()
    {
        if (!third_block)
        {
            third_block = true;

            //drill.SetActive(false);

            boss.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[12]);
            sniper.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[13]);
            tank.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[14]);
            doctor.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[15]);

            boss.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().fifth = true;
            sniper.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().fifth = true;
            tank.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().fifth = true;
            doctor.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().fifth = true;
        }
    }

    public void FifthAction(string name)
    {
        switch (name)
        {
            case "Boss":
                bag_empty_lookAt[0].SetActive(true);
                bag_fake[0].SetActive(false);
                boss.transform.LookAt(bag_empty_lookAt[0].transform);
                weapon[0].SetActive(false);
                boss.GetComponent<AgentAnimationTrailer>().steal = true;
                break;

            case "Sniper":
                bag_empty_lookAt[1].SetActive(true);
                bag_fake[1].SetActive(false);
                sniper.transform.LookAt(bag_empty_lookAt[1].transform);
                weapon[1].SetActive(false);
                sniper.GetComponent<AgentAnimationTrailer>().steal = true;
                break;

            case "Tank":
                bag_empty_lookAt[2].SetActive(true);
                bag_fake[2].SetActive(false);
                tank.transform.LookAt(bag_empty_lookAt[2].transform);
                weapon[2].SetActive(false);
                tank.GetComponent<AgentAnimationTrailer>().steal = true;
                break;

            case "Doctor":
                bag_empty_lookAt[3].SetActive(true);
                bag_fake[3].SetActive(false);
                doctor.transform.LookAt(bag_empty_lookAt[3].transform);
                weapon[3].SetActive(false);
                doctor.GetComponent<AgentAnimationTrailer>().steal = true;
                break;
        }

        StartCoroutine(MoneyRobbed());
    }

    private IEnumerator MoneyRobbed()
    {
        yield return new WaitForSeconds(3.0f);
        bag_empty_lookAt[0].SetActive(false);
        bag_empty_lookAt[1].SetActive(false);
        bag_empty_lookAt[2].SetActive(false);
        bag_empty_lookAt[3].SetActive(false);
        bag_medium[0].SetActive(true);
        bag_medium[1].SetActive(true);
        bag_medium[2].SetActive(true);
        bag_medium[3].SetActive(true);
        money[0].SetActive(false);
        money[2].SetActive(false);
        money[4].SetActive(false);
        yield return new WaitForSeconds(3.0f);
        money[1].SetActive(false);
        money[3].SetActive(false);
        money[5].SetActive(false);
        yield return new WaitForSeconds(2.0f);
        bag_medium[0].SetActive(false);
        bag_medium[1].SetActive(false);
        bag_medium[2].SetActive(false);
        bag_medium[3].SetActive(false);
        bag_full[0].SetActive(true);
        bag_full[1].SetActive(true);
        bag_full[2].SetActive(true);
        bag_full[3].SetActive(true);
        weapon[0].SetActive(true);
        weapon[1].SetActive(true);
        weapon[2].SetActive(true);
        weapon[3].SetActive(true);
        SixthAction();
    }

    public void SixthAction()
    {
        bag_fake[0].SetActive(true);
        bag_fake[1].SetActive(true);
        bag_fake[2].SetActive(true);
        bag_fake[3].SetActive(true);

        escape_point.SetActive(true);

        boss.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[16]);
        sniper.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[17]);
        tank.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[18]);
        doctor.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[19]);

        boss.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().seventh = true;
        sniper.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().seventh = true;
        tank.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().seventh = true;
        doctor.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().seventh = true;

        enemies_car[0].SetActive(true);
        enemies_car[1].SetActive(true);
        enemies_car[2].SetActive(true);
        enemies_car[3].SetActive(true);
        enemies_car[4].SetActive(true);
        enemies_car[5].SetActive(true);

        bag_full[0].SetActive(false);
        bag_full[1].SetActive(false);
        bag_full[2].SetActive(false);
        bag_full[3].SetActive(false);
    }

    public void SeventhAction()
    {
        helicopter.SetActive(true);
        StartCoroutine(EscapeAlly());
    }

    private IEnumerator EscapeAlly()
    {
        yield return new WaitForSeconds(1.0f);
        boss.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[20]);
        sniper.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[21]);
        tank.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[22]);
        doctor.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[23]);

        boss.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().eighth = true;
        sniper.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().eighth = true;
        tank.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().eighth = true;
        doctor.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().eighth = true;

        if (!fourth_block)
        {
            fourth_block = true;
            escape_point.GetComponent<EscapeTrailer>().SWAT();
        }
    }

    public void EighthAction(string name)
    {
        switch (name)
        {
            case "Boss":
                boss.transform.LookAt(employee.transform);
                break;

            case "Sniper":
                sniper.transform.LookAt(employee.transform);
                break;

            case "Tank":
                tank.transform.LookAt(employee.transform);
                break;

            case "Doctor":
                doctor.transform.LookAt(employee.transform);
                break;
        }

        if(escape_point.GetComponent<EscapeTrailer>().swat_1 != null)
        {
            escape_point.GetComponent<EscapeTrailer>().swat_1.transform.LookAt(boss.transform);
            escape_point.GetComponent<EscapeTrailer>().swat_2.transform.LookAt(doctor.transform);
            escape_point.GetComponent<EscapeTrailer>().swat_3.transform.LookAt(tank.transform);
            escape_point.GetComponent<EscapeTrailer>().swat_4.transform.LookAt(sniper.transform);

        }
    }
}
