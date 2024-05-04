using System;
using Core;
using SavingSystem;
using School.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class Settings : MonoBehaviour, IRegistrable
    {
        
        [SerializeField] private Slider sensitivitySlider;
        [SerializeField] private TMP_Text sensitivityText;
        
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private TMP_Text volumeText;
        
        [SerializeField] private Slider doorNameSwitch;
        
        private GameManager _gameManager;
        
        void Awake()
        {
            _gameManager = ServiceLocator.Get<GameManager>();
            ServiceLocator.Register(this);
        }

        private void OnEnable()
        {
            UpdateSettingsUI();
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<Settings>();
        }
        
        
        public void ResetGame()
        {
            ServiceLocator.Get<SavingManager>().ResetGame();
            UpdateSettingsUI();
        }

        public void DoorNameSwitch()
        {
            _gameManager.defaultDoorNames = doorNameSwitch.value == 1;
            UpdateSettingsUI();
        }
        
        public void VolumeChanged()
        {
            _gameManager.volumeSettings = volumeSlider.value;
            UpdateSettingsUI();
        }
        
        public void SensitivityChanged()
        {
            _gameManager.sensitivitySettings = sensitivitySlider.value;
            UpdateSettingsUI();
        }


        private void UpdateSettingsUI()
        {
            volumeSlider.value = _gameManager.volumeSettings;
            volumeText.text = volumeSlider.value.ToString("F1");
            
            sensitivitySlider.value = _gameManager.sensitivitySettings;
            sensitivityText.text = sensitivitySlider.value.ToString("F1");
            
            doorNameSwitch.value = _gameManager.defaultDoorNames ? 1 : 0;
        }
    }
}
