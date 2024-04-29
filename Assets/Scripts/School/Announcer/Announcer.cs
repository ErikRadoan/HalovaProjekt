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
        [SerializeField] private TMP_Text dayText;
        
        private void Awake()
        {
            ServiceLocator.Register(this, true);
        }
        
        private void OnDestroy()
        {
            ServiceLocator.Unregister<Announcer>();
        }
        
        public void Announce(string message)
        {
            announcerText.text = message;
        }
        
        public void ClearAnnouncement()
        {
            announcerText.text = "";
        }

        public void SetDay(int day)
        {
            dayText.text = day switch
            {
                1 => "Monday",
                2 => "Tuesday",
                3 => "Wednesday",
                4 => "Thursday",
                5 => "Friday",
                _ => dayText.text
            };
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
