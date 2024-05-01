using System;
using Core;
using School.EscapePhase;
using UnityEngine;
using UnityEngine.AI;

namespace School.Enemys
{
    public class Skolnik : Enemy
    {
        NavMeshAgent _agent;

        private void OnEnable()
        {
            _agent = gameObject.GetComponent<NavMeshAgent>();   
        }

        public void SetSpeed(float newSpeed)
        {
            _agent.speed = newSpeed;
        }
        
        public float GetSpeed()
        {
            return _agent.speed;
        }
        
        public void SetAcceleration(float newAcceleration)
        {
            _agent.acceleration = newAcceleration;
        }
        
        public float GetAcceleration()
        {
            return _agent.acceleration;
        }
        
        public void SetRotationSpeed(float newRotationSpeed)
        {
            _agent.angularSpeed = newRotationSpeed;
        }
        
        public float GetRotationSpeed()
        {
            return _agent.angularSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Destroy(GetComponent<BoxCollider>());
                ServiceLocator.Get<EscapePhaseManager>().YouGotCaught();
            }
        }
    }
}
