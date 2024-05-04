using System;
using Core;
using School.Core;
using UnityEngine;

namespace SavingSystem
{
    public class SavingManager : MonoBehaviour, IRegistrable
    {
        public static SavingManager Instance { get; private set; }
        
        GameManager _gameManager;
        

        private void Awake()
        {
            
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                ServiceLocator.Register(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SaveGame()
        {
            _gameManager = ServiceLocator.Get<GameManager>();
            
            PlayerPrefs.SetInt("DefaultDoorNames", _gameManager.defaultDoorNames ? 1 : 0);
            PlayerPrefs.SetFloat("Volume", _gameManager.volumeSettings);
            PlayerPrefs.SetFloat("Sensitivity", _gameManager.sensitivitySettings);
            PlayerPrefs.SetInt("CurrentDay", _gameManager.currentDay);
            PlayerPrefs.SetInt("CurrentTimeToEscape", _gameManager.currentTimeToEscape);
        }
        
        public void LoadGame()
        {
            _gameManager = ServiceLocator.Get<GameManager>();
            _gameManager.defaultDoorNames = PlayerPrefs.GetInt("DefaultDoorNames") == 1;
            _gameManager.volumeSettings = PlayerPrefs.GetFloat("Volume");
            _gameManager.sensitivitySettings = PlayerPrefs.GetFloat("Sensitivity");
            _gameManager.currentDay = PlayerPrefs.GetInt("CurrentDay");
            _gameManager.currentTimeToEscape = PlayerPrefs.GetInt("CurrentTimeToEscape");
        }
        
        public void ResetGame()
        {
            PlayerPrefs.SetInt("CurrentDay", 1);
            PlayerPrefs.SetInt("CurrentTimeToEscape", 60);
            LoadGame();
        }
    }
}
