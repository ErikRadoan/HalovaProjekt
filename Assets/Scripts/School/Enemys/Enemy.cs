using Core;
using UnityEngine;
using UnityEngine.AI;

namespace School.Enemys
{
    public class Enemy : MonoBehaviour
    {
        private bool _isFollowingPlayer;
        private NavMeshAgent _agent;
        private Transform _player;
        public void FollowPlayer()
        {
            _agent = gameObject.GetComponent<NavMeshAgent>();
            _agent.isStopped = false;
            _isFollowingPlayer = true;
            _player = ServiceLocator.Get<Player.PlayerReference>().transform;
        }

        public void StopFollowing()
        {
            _agent.isStopped = true;
        }
        
        void Update()
        {
            if (_isFollowingPlayer)
            {
                _agent.SetDestination(_player.position);
            }
            
        }
        
    }
}
