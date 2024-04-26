using System;
using Core;
using School.Core;
using TMPro;
using UnityEngine;

namespace School.EscapePhase
{
    public class Door : MonoBehaviour
    {
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
        

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log(other.gameObject.name);
            ServiceLocator.Get<DoorManager>().DoorOpened(this);
        }
    }
}
