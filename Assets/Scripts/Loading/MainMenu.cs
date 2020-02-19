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
            Vector3 position = new Vector3(saveFileData.playerStats.actorX, saveFileData.playerStats.actorY, saveFileData.playerStats.actorZ);
            Quaternion rotation = new Quaternion(saveFileData.playerStats.actorRX, saveFileData.playerStats.actorRY, saveFileData.playerStats.actorRZ, saveFileData.playerStats.actorRW);
            Debug.Log($"Pos: {position}");
            Debug.Log($"Rot: {rotation}");
            LoadScene.instance.LoadLevel(saveFileData.playerLevel, position, rotation);
        }
    }
}