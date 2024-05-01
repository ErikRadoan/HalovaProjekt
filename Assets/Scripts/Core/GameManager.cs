using System;
using Menu;
using SavingSystem;
using School.Core;
using School.EscapePhase;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Core
{
    public class GameManager : MonoBehaviour, IRegistrable
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] public int currentTimeToEscape = 60;
        [SerializeField] public int currentDay = 1;

        [SerializeField] public float sensitivitySettings;
        [SerializeField] public float volumeSettings;
        [SerializeField] public bool defaultDoorNames = true;
        
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            ServiceLocator.Register(this);
            ServiceLocator.Get<SavingManager>().LoadGame();
        }
        
        
        public void OnStartButtonPressed()
        {
            
            SceneManager.LoadScene("School");
        }

        public void DeductTime()
        {
            currentTimeToEscape -= 10;
        }
        
        public void OnEscaped()
        {
            SceneManager.LoadScene("Scenes/Questions");
        }

        private void OnApplicationQuit()
        {
            ServiceLocator.Get<SavingManager>().SaveGame();
        }
    }
}
