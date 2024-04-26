using UnityEngine;

namespace School.Core
{
    public abstract class MonoRegistrable : MonoBehaviour, IRegistrable
    {
        protected void Reset()
        {
            name = GetType().Name;
        }
    }
}
