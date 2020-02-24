using System;
using UnityEngine;

namespace SoulsLike
{
    public class MainMenu : MonoBehaviour
    {
        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void OnContinue()
        {
            SaveFileData saveFileData = SaveInput.instance.LoadMostRecentSave();
            Vector3 position = new Vector3(saveFileData.playerStats.actorPosition.x, saveFileData.playerStats.actorPosition.y, saveFileData.playerStats.actorPosition.z);
            Quaternion rotation = new Quaternion(saveFileData.playerStats.actorRotation.x, saveFileData.playerStats.actorRotation.y, saveFileData.playerStats.actorRotation.z, saveFileData.playerStats.actorRotation.w);
            GlobalController.SetGlobals(saveFileData.savedGlobals);
            LoadScene.instance.LoadLevel(saveFileData.playerLevel, position, rotation);
        }

        public void OnNew()
        {
            throw new NotImplementedException();
        }

        public void OnLoad()
        {
            throw new NotImplementedException();
        }

        public void OnSettings()
        {
            throw new NotImplementedException();
        }

        public void OnQuit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}