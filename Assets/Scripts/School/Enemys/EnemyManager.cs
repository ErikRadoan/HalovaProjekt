using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using School.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace School.Enemys
{
    public class EnemyManager : MonoBehaviour, IRegistrable
    {
        [SerializeField] private List<GameObject> _enemyList = new();
        [SerializeField] private Transform spawnLocation;
        public Delegate StopFollowing;

        private void Awake()
        {
            ServiceLocator.Register(this);
        }
        
        private void OnDestroy()
        {
            ServiceLocator.Unregister<EnemyManager>();
        }
        
        public void EnemyFollowPlayer(string enemyName)
        {
            GameObject foundEnemy = _enemyList.Find(e => e.name == enemyName);
            if (foundEnemy != null)
            {
                if (foundEnemy.activeInHierarchy)
                {
                    foundEnemy.GetComponent<Enemy>().FollowPlayer();
                }
                else
                {
                    GameObject newEnemy = Instantiate(foundEnemy, spawnLocation.position, Quaternion.identity);
                    _enemyList.Remove(foundEnemy);
                    newEnemy.name = foundEnemy.name;
                    _enemyList.Add(newEnemy);
                    newEnemy.GetComponent<Enemy>().FollowPlayer();
                }
            }
            else
            {
                Debug.Log("Enemy not found!");
            }
        }
        
        public void EnemyStopFollowing(string enemyName)
        {
            GameObject foundEnemy = _enemyList.Find(e => e.name == enemyName);
            if (foundEnemy != null)
            {
                foundEnemy.GetComponent<Enemy>().StopFollowing();
            }
        }
        
        public GameObject GetEnemy(string enemyName)
        {
            GameObject foundEnemy = _enemyList.Find(e => e.name == enemyName);
            if (foundEnemy != null)
            {
                return foundEnemy;
            }
            else
            {
                Debug.Log("Enemy not found!");
                return null;
            }
        }

    }
}
