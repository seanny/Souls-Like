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
        public DayNightData saveData = new DayNightData();
        public Light sun;

        public static void SetDayNightCycleStats(DayNightData dayNightData)
        {
            DayNightCycle dayNightCycle = GameObject.FindObjectOfType<DayNightCycle>();
            if(dayNightCycle != null)
            {
                dayNightCycle.saveData = dayNightData;
            }
        }

        private void Update()
        {
            saveData.minute += Time.deltaTime;
            if(Mathf.RoundToInt(saveData.minute) > 59)
            {
                saveData.minute = 0;
                saveData.hour++;
                if(saveData.hour > 23)
                {
                    saveData.hour = 0;
                    saveData.day++;
                    if(saveData.day > 30)
                    {
                        saveData.day = 0;
                        saveData.month++;
                        if(saveData.month > 12)
                        {
                            saveData.month = 0;
                            saveData.year++;
                        }
                    }
                }
            }

            float degrees = ((saveData.hour / 24.0f) * 360f) - 90f;
            sun.transform.rotation = Quaternion.Euler(degrees, 0, 0);
        }
    }
}
