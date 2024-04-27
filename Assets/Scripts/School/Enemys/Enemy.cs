using Core;
using UnityEngine;
using UnityEngine.AI;

namespace School.Enemys
{
    public class Enemy : MonoBehaviour
    {
        private bool _isFollowingPlayer;
        private NavMeshAgent _navMeshAgent;
        private Transform _player;
        public void FollowPlayer()
        {
            _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            _navMeshAgent.isStopped = false;
            _isFollowingPlayer = true;
            _player = ServiceLocator.Get<Player.PlayerReference>().transform;
        }

        public void StopFollowing()
        {
            _navMeshAgent.isStopped = true;
        }
        
        void Update()
        {
            if (_isFollowingPlayer)
            {
                _navMeshAgent.SetDestination(_player.position);
            }
            
        }
        
    }
}
