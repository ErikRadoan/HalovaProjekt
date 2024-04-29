using System;
using Core;
using School.Core;
using UnityEngine;

namespace SavingSystem
{
    public class SavingManager : MonoBehaviour, IRegistrable
    {
        public static SavingManager Instance { get; private set; }
        

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
            PlayerPrefs.SetFloat("Sensitivity", ServiceLocator.Get<GameManager>().sensitivitySettings);
            PlayerPrefs.SetInt("CurrentDay", ServiceLocator.Get<GameManager>().currentDay);
            PlayerPrefs.SetInt("CurrentTimeToEscape", ServiceLocator.Get<GameManager>().currentTimeToEscape);
        }
        
        public void LoadGame()
        {
            ServiceLocator.Get<GameManager>().sensitivitySettings = PlayerPrefs.GetFloat("Sensitivity");
            ServiceLocator.Get<GameManager>().currentDay = PlayerPrefs.GetInt("CurrentDay");
            ServiceLocator.Get<GameManager>().currentTimeToEscape = PlayerPrefs.GetInt("CurrentTimeToEscape");
        }
        
        public void ResetGame()
        {
            PlayerPrefs.SetInt("CurrentDay", 1);
            PlayerPrefs.SetInt("CurrentTimeToEscape", 60);
            LoadGame();
        }
    }
}
