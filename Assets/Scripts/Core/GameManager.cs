using School.Core;
using School.EscapePhase;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Core
{
    public class GameManager : MonoBehaviour, IRegistrable
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] public int currentTimeToEscape = 60;
        [SerializeField] public int currentDay = 1;

        [SerializeField] public float sensitivitySettings;
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            ServiceLocator.Register(this);
        }

        public void OnStartButtonPressed()
        {
            SceneManager.LoadScene("School");
        }

        public void DeductTime()
        {
            currentTimeToEscape -= 15;
        }
        
        public void OnEscaped()
        {
            SceneManager.LoadScene("Scenes/Questions");
        }
        
    }
}
