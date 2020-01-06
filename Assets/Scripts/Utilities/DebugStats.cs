using System.Collections;
using UnityEngine;
using TMPro;

public class DebugStats : MonoBehaviour
{
    public TextMeshProUGUI debugStats;

    public float frequency = 1f;
    public int FramesPerSec { get; protected set; }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FramesPerSecond());
    }

    private IEnumerator FramesPerSecond()
    {
        while(true)
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

    // Update is called once per frame
    void Update()
    {
        string targetFPS = "N/A";
        if(Application.targetFrameRate != -1)
        {
            targetFPS = Application.targetFrameRate.ToString();
        }
        debugStats.text = $"FPS: {FramesPerSec} - Target: {targetFPS}";
    }
}
