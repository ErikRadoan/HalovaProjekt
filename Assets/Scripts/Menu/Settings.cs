using System;
using Core;
using School.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class Settings : MonoBehaviour, IRegistrable
    {
        public float sensitivityValue = 2f;
        
        [SerializeField] private Slider sensitivitySlider;
        [SerializeField] private TMP_Text sensitivityText;
        
        void Start()
        {
            ServiceLocator.Register(this);
            sensitivityText.text = sensitivityValue.ToString("F1");
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<Settings>();
            ServiceLocator.Get<GameManager>().sensitivitySettings = sensitivityValue;
        }

        public void ChangeValue()
        {
            sensitivityValue = sensitivitySlider.value;
            sensitivityText.text = sensitivityValue.ToString("F1");
        }
        
        
    }
}
