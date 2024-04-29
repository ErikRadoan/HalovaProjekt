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
        private GameManager _gameManager;
        
        void Start()
        {
            _gameManager = ServiceLocator.Get<GameManager>();
            ServiceLocator.Register(this);
            sensitivityText.text = _gameManager.sensitivitySettings.ToString("F1");
            sensitivitySlider.value = _gameManager.sensitivitySettings;
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<Settings>();
            
        }

        public void ChangeValue()
        {
            _gameManager.sensitivitySettings =  sensitivitySlider.value;
            sensitivityText.text = _gameManager.sensitivitySettings.ToString("F1");
        }
        
        public void ResetGame()
        {
            ServiceLocator.Get<SavingManager>().ResetGame();
        }
    }
}
