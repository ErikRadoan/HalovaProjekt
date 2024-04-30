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
        
        void Start()
        {
            _gameManager = ServiceLocator.Get<GameManager>();
            ServiceLocator.Register(this);
            
            sensitivityText.text = _gameManager.sensitivitySettings.ToString("F1");
            sensitivitySlider.value = _gameManager.sensitivitySettings;
            
            volumeText.text = _gameManager.volumeSettings.ToString("F1");
            volumeSlider.value = _gameManager.volumeSettings;
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<Settings>();
            
        }

        public void ChangeSensitivityValue()
        {
            _gameManager.sensitivitySettings =  sensitivitySlider.value;
            sensitivityText.text = _gameManager.sensitivitySettings.ToString("F1");
        }
        
        public void ChangeVolumeValue()
        {
            _gameManager.volumeSettings = volumeSlider.value;
            volumeText.text = _gameManager.volumeSettings.ToString("F1");
        }
        
        public void ChangeDoorNameStatus()
        {
            _gameManager.defaultDoorNames = doorNameSwitch.value == 1;
        }
        
        public void ResetGame()
        {
            ServiceLocator.Get<SavingManager>().ResetGame();
        }
    }
}
