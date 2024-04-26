using System;
using UnityEngine;
using UnityEngine.AI;

namespace School.Enemys
{
    public class Skolnik : Enemy
    {
        public float speed;
        public float rotationSpeed;
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
            return speed;
        }
        
        public void SetRotationSpeed(float newRotationSpeed)
        {
            _agent.angularSpeed = newRotationSpeed;
        }
        
        public float GetRotationSpeed()
        {
            return rotationSpeed;
        }
    }
}
