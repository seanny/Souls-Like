using System.Collections;
using TMPro;
using UnityEngine;

namespace SoulsLike
{
    public class DebugStats : MonoBehaviour
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        public InputManager inputManager;
        public TextMeshProUGUI debugStats;

        public float frequency = 1f;
        public int FramesPerSec { get; protected set; }
        public string[] ActiveControllers { get; protected set; }
        public static bool Active { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            debugStats.gameObject.SetActive(true);
            StartCoroutine(FramesPerSecond());
        }

        private IEnumerator FramesPerSecond()
        {
            while (true)
            {
                int lastFrameCount = Time.frameCount;
                float lastTime = Time.realtimeSinceStartup;
                yield return new WaitForSeconds(frequency);
                float timeSpan = Time.realtimeSinceStartup - lastTime;
                int frameCount = Time.frameCount - lastFrameCount;

                // Display it
                FramesPerSec = Mathf.RoundToInt(frameCount / timeSpan);
            }
        }

        public static void ToggleDebugStats()
        {
            Active = !Active;
        }

        // Update is called once per frame
        void Update()
        {
            debugStats.gameObject.SetActive(Active);
            
            if(Active)
            {
                string controllerNames = string.Empty;

                string targetFPS = "N/A";
                if (Application.targetFrameRate != -1)
                {
                    targetFPS = Application.targetFrameRate.ToString();
                }
                debugStats.text = $"FPS: {FramesPerSec} - Target: {targetFPS}";
            }
        }
#endif
    }
}