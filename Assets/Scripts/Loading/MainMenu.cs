using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SoulsLike
{
    public class MainMenu : MonoBehaviour
    {
        public void OnContinue()
        {
            SaveFileData saveFileData = SaveInput.instance.LoadMostRecentSave();
            Vector3 position = new Vector3(saveFileData.playerStats.actorPosition.x, saveFileData.playerStats.actorPosition.y, saveFileData.playerStats.actorPosition.z);
            Quaternion rotation = new Quaternion(saveFileData.playerStats.actorRX, saveFileData.playerStats.actorRY, saveFileData.playerStats.actorRZ, saveFileData.playerStats.actorRW);
            LoadScene.instance.LoadLevel(saveFileData.playerLevel, position, rotation);
        }
    }
}