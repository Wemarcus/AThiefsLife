using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for

        public bool floor;
        private bool block;

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
			if (target != null) {
                agent.enabled = true;
                agent.SetDestination(target.position);
                FindObjectOfType<MapHandler>().AnimationPerforming(true);
            }

            if (target != null && agent.remainingDistance > agent.stoppingDistance) {
				character.Move (agent.desiredVelocity, false, false);
			} else {
				character.Move (Vector3.zero, false, false);
			}

			if (target && !agent.pathPending && agent.remainingDistance < 0.5f) { 
				//Debug.Log ("stoppa animazione");
				FindObjectOfType<MapHandler> ().AnimationPerforming (false);
				SetTarget (null);
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
        }
    }
}
