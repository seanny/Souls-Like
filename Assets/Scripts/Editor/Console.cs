using System;
using System.Diagnostics;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace SoulsLike
{
    public static class Console
    {
        [MenuItem("Utility/Open Console")]
        public static void OpenConsole()
        {
            string cmdPrompt = string.Empty;
#if UNITY_EDITOR_WIN
            cmdPrompt = @"powershell.exe";
#endif

            try
            {
                Process process = new Process();
                ProcessStartInfo procStartInfo = new ProcessStartInfo(cmdPrompt);
                process.StartInfo = procStartInfo;
                process.Start();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error opening Powershell: {e.Message}");
            }
        }
    }
}
