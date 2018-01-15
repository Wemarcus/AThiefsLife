using System;
using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class AICharacterControlTrailer : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for

        public bool floor;
        private bool block;
        private bool block_2;
        private bool block_3;

        private bool moving;

        // variabili fittizie per il trailer
        public GameObject trailerAI;
        public GameObject drill;
        public bool first = true;
        public bool second = false;
        public bool third = false;
        public bool fifth = false;
        public bool seventh = false;
        public bool eighth = false;
        public bool is_swat = false;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updateRotation = false;
            agent.updatePosition = true;
        }


        private void Update()
        {
            if (floor)
            {
                if (target != null && !moving)
                {
                    agent.enabled = true;
                    agent.SetDestination(target.position);
                    moving = true;
                }

                if (target != null && agent.remainingDistance > agent.stoppingDistance)
                {
                    character.Move(agent.desiredVelocity, false, false);
                }
                else
                {
                    character.Move(Vector3.zero, false, false);
                }

                if (target && !agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    SetTarget(null);
                    moving = false;

                    if (first)
                    {
                        first = false;
                        trailerAI.GetComponent<TrailerAI>().FirstAction(gameObject.name);
                    }

                    if (second)
                    {
                        second = false;
                        trailerAI.GetComponent<TrailerAI>().SecondAction(gameObject.name);
                    }

                    if (third)
                    {
                        third = false;
                        trailerAI.GetComponent<TrailerAI>().ThirdAction(gameObject.name);
                    }

                    if (fifth)
                    {
                        fifth = false;
                        trailerAI.GetComponent<TrailerAI>().FifthAction(gameObject.name);
                    }

                    if (seventh)
                    {
                        seventh = false;
                        trailerAI.GetComponent<TrailerAI>().SeventhAction();
                    }

                    if (eighth)
                    {
                        eighth = false;
                        trailerAI.GetComponent<TrailerAI>().EighthAction(gameObject.name);
                    }

                    if (is_swat)
                    {
                        is_swat = false;
                        GameObject.Find("Ally").GetComponent<TrailerAI>().NinthAction();
                    }
                }
            }
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!block)
            {
                switch (other.tag)
                {
                    case "Floor":
                        floor = true;
                        block = true;
                        agent.enabled = false;
                        break;
                }
            }

            if (!block_2)
            {
                switch (other.tag)
                {
                    case "Drill":
                        block_2 = true;
                        StartCoroutine(OpenCaveau());
                        break;
                }
            }
        }

        private IEnumerator OpenCaveau()
        {
            if (!block_3)
            {
                block_3 = true;
                yield return new WaitForSeconds(0.5f);
                if(drill != null)
                {
                    drill.SetActive(true);
                }

                if (gameObject.name == "Tank")
                {
                    transform.LookAt(drill.transform);
                }

                yield return new WaitForSeconds(7.0f);
                if(trailerAI != null)
                {
                    trailerAI.GetComponent<TrailerAI>().FourthAction();
                }
            }
        }
    }
}

