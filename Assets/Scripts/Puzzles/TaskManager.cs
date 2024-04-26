using System;
using UnityEngine;
//using UnityEngine.UI;

namespace Puzzles
{
    public class TaskManager : MonoBehaviour
    {
        [SerializeField] private Transform UITaskParent;
        public static Action<bool> callBackFunction;
        private GameObject _currentTask;
        public void StartTask(PuzzleScriptable puzzleScriptable)
        {
            _currentTask = Instantiate(puzzleScriptable.correspondingUI, UITaskParent);
        }
    
        protected virtual void TaskFinished(bool passed = false)
        {
            callBackFunction?.Invoke(true);
            Destroy(_currentTask);
        }

    }
}
