using System;
using UnityEngine;

namespace SoulsLike
{
    [Serializable]
    public class DayNightData
    {
        public float minute;
        public int hour;
        public int day;
        public int month;
        public int year;
        public float timeScale;
    }

    public class DayNightCycle : MonoBehaviour
    {
        public DayNightData saveData
        {
            get
            {
                return m_SaveData;
            }
            set
            {
                m_SaveData = value;
            }
        }

        [SerializeField] private DayNightData m_SaveData;
        public Light sun;

        public static void SetDayNightCycleStats(DayNightData dayNightData)
        {
            DayNightCycle dayNightCycle = GameObject.FindObjectOfType<DayNightCycle>();
            if(dayNightCycle != null)
            {
                dayNightCycle.saveData = dayNightData;
            }
        }


        private void Start()
        {
            if(m_SaveData == null)
            {
                m_SaveData = new DayNightData();
            }
        }

        private void Update()
        {
            m_SaveData.minute += Time.deltaTime;
            if(Mathf.RoundToInt(m_SaveData.minute) > 59)
            {
                m_SaveData.minute = 0;
                m_SaveData.hour++;
                if(m_SaveData.hour > 23)
                {
                    m_SaveData.hour = 0;
                    m_SaveData.day++;
                    if(m_SaveData.day > 30)
                    {
                        m_SaveData.day = 0;
                        m_SaveData.month++;
                        if(m_SaveData.month > 12)
                        {
                            m_SaveData.month = 0;
                            m_SaveData.year++;
                        }
                    }
                }
            }

            float degrees = ((m_SaveData.hour / 24.0f) * 360f) - 90f;
            sun.transform.rotation = Quaternion.Euler(degrees, 0, 0);
        }
    }
}
