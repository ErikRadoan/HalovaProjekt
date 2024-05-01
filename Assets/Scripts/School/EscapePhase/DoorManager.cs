using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using School.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace School.EscapePhase
{
    public class DoorManager : MonoBehaviour, IRegistrable
    {
        private List<Door> _doors = new List<Door>();
        [SerializeField] private List<string> doorNames;
        
        private Door _activeDoor;

        private Announcer.Announcer _announcer;

        public delegate void OnDoorOpened();
        public event OnDoorOpened OnDoorOpenedEvent;
        private void Awake()
        {
            ServiceLocator.Register(this);
            
            _doors = FindObjectsByType<Door>(FindObjectsSortMode.None).ToList();
        }
        
        private void OnDestroy()
        {
            ServiceLocator.Unregister<DoorManager>();
        }

        public void StartGame()
        {
            if (ServiceLocator.Get<GameManager>().defaultDoorNames)
            {
                Debug.Log("Default names");
                SetDefaultNames();
            }
            else
            {
                ShuffleNames();
            }
            _activeDoor = GenerateRandomDoor();
            _announcer = ServiceLocator.Get<Announcer.Announcer>();
            Debug.Log(_doors.Count);
            _announcer.Announce("Next class is in " + _activeDoor.GetDoorName() + "!");
        }
        

        //When a door is opened, check if it is the active door
        public void DoorOpened(Door door)
        {
            if (door == _activeDoor)
            {
                _activeDoor = null;
                _announcer.ClearAnnouncement();
                OnDoorOpenedEvent?.Invoke();
            }
        }
        
        public Door GetActiveDoor()
        {
            return _activeDoor;
        }
        
        //Generate a random door from the list of doors
        private Door GenerateRandomDoor()
        {
            if (_doors.Count == 0)
            {
                return null;
            }
            Door newDoor = _doors[Random.Range(0, _doors.Count)];
            return newDoor;
        }
        
        //Shuffle the door names
        private void ShuffleNames()
        {
            
            //create a temporary list of door names
            List<string> tempNames = new List<string>(doorNames);
            
            foreach (Door door in _doors)
            {
                
                //assign a random name to each door from the list of door names and remove the name from the list
                string chosenDoorName = tempNames[Random.Range(0, tempNames.Count)];
                door.SetDoorName(chosenDoorName);
                tempNames.Remove(chosenDoorName);
            }
        }
        
        private void SetDefaultNames()
        {
            foreach (Door door in _doors)
            {
                door.SetDoorName(door.myDefaultName);
            }
        }
    }
}
