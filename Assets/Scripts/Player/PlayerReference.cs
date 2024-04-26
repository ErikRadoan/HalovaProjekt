using Core;
using School.Core;
using UnityEngine;

namespace Player
{
    public class PlayerReference : MonoBehaviour, IRegistrable
    {
        private void Awake()
        {
            ServiceLocator.Register(this);
        }
    }
}
