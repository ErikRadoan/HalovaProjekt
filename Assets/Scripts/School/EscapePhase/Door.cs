using System;
using Core;
using School.Core;
using TMPro;
using UnityEngine;

namespace School.EscapePhase
{
    public class Door : MonoBehaviour
    {
        [SerializeField] public string myDefaultName;
        
        private string myName;
        [SerializeField] private TMP_Text doorNameText;
        
        public void SetDoorName(string newName)
        {
            myName = newName;
            doorNameText.text = myName;
        }

        public string GetDoorName()
        {
            return myName;
        }

        private void OnTriggerEnter(Collider other)
        {
            ServiceLocator.Get<DoorManager>().DoorOpened(this);
        }

        private void OnCollisionEnter(Collision other)
        {
            ServiceLocator.Get<DoorManager>().DoorOpened(this);
        }
    }
}
