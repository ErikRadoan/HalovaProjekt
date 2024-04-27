using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using School.Core;
using School.Enemys;
using Sound;
using UnityEngine;

namespace School.EscapePhase
{
    public class EscapePhaseManager : MonoBehaviour, IRegistrable
    {
        [SerializeField] private float escapeTime = 60f;
        
        
        private float _currentTime;
        private float _chaseTime;
        private bool _timerIsRunning = true;
        
        private Announcer.Announcer _announcer;
        AudioSource _stopSound;
        AudioSource _ringSound;
        AudioSource _chaseSound;
        
        DoorManager _doorManager;
        EnemyManager _enemyManager;
        
        private static readonly int SkyTint = Shader.PropertyToID("_SkyTint");
        private static readonly int Exposure = Shader.PropertyToID("_Exposure");

        Light directionalLight;
        public void Start()
        {
            escapeTime = ServiceLocator.Get<GameManager>().currentTimeToEscape;
            
            //set references
            directionalLight = GameObject.Find("Directional Light").GetComponent<Light>();
            _doorManager = ServiceLocator.Get<DoorManager>();
            _announcer = ServiceLocator.Get<Announcer.Announcer>();
            _enemyManager = ServiceLocator.Get<EnemyManager>();
            
            //trigger other managers
            ServiceLocator.Register(this);
            ServiceLocator.Get<DoorManager>().StartGame();
            ServiceLocator.Get<DoorManager>().OnDoorOpenedEvent += ChasePhaseEnd;
            
            //start the timer
            _currentTime = escapeTime;
            _timerIsRunning = true;
            
            //set up the ambience
            _stopSound = ServiceLocator.Get<SoundPlayer>().PlayUntilStopped("Chill", true);
            AmbienceChange(true);
            
        }
        
        
        void Update()
        {
            if (_doorManager == null || _announcer == null || _enemyManager == null) { return; }
            if (_timerIsRunning && _doorManager.GetActiveDoor() != null)
            {
                double colorSaturation = 1;
                if (_currentTime < escapeTime / 3)
                {
                    colorSaturation = 1 - Math.Cos(ConvertToRadians(_currentTime / (escapeTime/3) * 90));
                }
                if (_currentTime > 0)
                {
                    _currentTime -= Time.deltaTime;
                } 
                else
                {
                    _timerIsRunning = false;
                    _currentTime = 0;
                    _stopSound.Stop();
                    ChasePhase();
                }
                
                _announcer.UpdateTimer(_currentTime, colorSaturation);
            }
            else if(_doorManager.GetActiveDoor() != null && !_timerIsRunning)
            {
                GameObject skolnikObject = _enemyManager.GetEnemy("Skolnik");
                if (skolnikObject != null)
                {
                    Skolnik skolnik = skolnikObject.GetComponent<Skolnik>();
                    if (skolnik != null)
                    {
                        skolnik.SetSpeed(skolnik.GetSpeed() + (Time.deltaTime / 2));
                        skolnik.SetRotationSpeed(skolnik.GetRotationSpeed() + Time.deltaTime);
                        skolnik.SetAcceleration(skolnik.GetAcceleration() + (Time.deltaTime / 4));
                    }
                }
            }
            else
            {
                _stopSound.Stop();
                ServiceLocator.Get<GameManager>().OnEscaped();
            }
        }
        
        double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        private void ChasePhase()
        {
            AmbienceChange();
            ServiceLocator.Get<SoundPlayer>().PlaySound("Ring");
            _enemyManager.EnemyFollowPlayer("Skolnik");
            _chaseSound = ServiceLocator.Get<SoundPlayer>().PlayUntilStopped(_enemyManager.GetEnemy("Skolnik"), "Footsteps", true);
        }

        void ChasePhaseEnd()
        {
            AmbienceChange(true);
            if (_chaseSound != null)
            {
                _chaseSound.Stop();
                _enemyManager.EnemyStopFollowing("Skolnik");
            }
            
        }
        
        void AmbienceChange(bool revert = false)
        {
            if (revert)
            {
                directionalLight.intensity = 0.5f;
                RenderSettings.fog = false;
            }
            else
            {
                directionalLight.intensity = 0.1f;
                RenderSettings.fog = true;
            }
        }
        
        
    }
}