using Core;
using School.Core;
using TMPro;
using UnityEngine;

namespace School.Announcer
{
    public class Announcer : MonoBehaviour, IRegistrable
    {
        
        [SerializeField] private TMP_Text announcerText;
        [SerializeField] private TMP_Text timerText;
        
        private void Awake()
        {
            ServiceLocator.Register(this, true);
        }
        
        public void Announce(string message)
        {
            announcerText.text = message;
        }
        
        public void ClearAnnouncement()
        {
            announcerText.text = "";
        }

        public void UpdateTimer(float time, double colorSaturation)
        {

            timerText.color = new Color(1, (float)colorSaturation, (float)colorSaturation);

            float sec = time % 60;
            float min = (time - sec) / 60;
            if (min == 0 && sec <= 0)
            {
                timerText.text = "8:00:00";
            }
            else
            {
                timerText.text = "7:" + (59 - min) + ":" + (60 - sec).ToString("F2");
            }
            
        }
    }
}
