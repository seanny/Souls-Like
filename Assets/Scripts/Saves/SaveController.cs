using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SoulsLike
{

    public static class SaveController
    {
        public static SaveFileData LoadSave(string name)
        {
            SaveFileData saveFileData = null;
            
            if(DoesSaveExist(name))
            {
                saveFileData = DeserializeSave(name, saveFileData);
            }
            return saveFileData;
        }

        private static SaveFileData DeserializeSave(string name, SaveFileData saveFileData)
        {
            FileStream fs = new FileStream(GetSavePath(name), FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                saveFileData = (SaveFileData)formatter.Deserialize(fs);
            }
            catch (Exception exception)
            {
                Debug.LogError($"Error whilst reading save: {exception.Message}");
            }
            finally
            {
                fs.Close();
            }

            return saveFileData;
        }

        public static void CreateSave(string name, bool overwrite = false)
        {
            SaveFileData saveFileData = CreateSaveData();
            SerializeSave(name, overwrite, saveFileData);
        }

        private static void SerializeSave(string name, bool overwrite, SaveFileData saveFileData)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(GetSavePath(name)) && overwrite == false)
            {
                Debug.LogError($"Error whilst saving: Save already exists.");
                return;
            }

            if (File.Exists(GetSavePath(name)) && overwrite == true)
            {
                File.Delete(GetSavePath(name));
            }

            using (FileStream stream = new FileStream(GetSavePath(name), FileMode.Create))
            {
                try
                {
                    formatter.Serialize(stream, saveFileData);
                }
                catch (Exception exception)
                {
                    Debug.LogError($"Error whilst saving: {exception.Message}");
                }
            }
        }

        private static SaveFileData CreateSaveData()
        {
            SaveFileData saveFileData = new SaveFileData();
            saveFileData.gameVersion = GameVersion.GAME_VERSION;
            saveFileData.playerStats = PlayerActor.instance.actorStats;
            saveFileData.playerLevel = LoadScene.instance.currentScene.name;
            foreach (var item in QuestController.instance.quests)
            {
                saveFileData.questData.Add(item.questData);
            }
            foreach (var item in QuestController.instance.completedQuests)
            {
                saveFileData.questData.Add(item.questData);
            }
            if (saveFileData.actorStats == null)
            {
                saveFileData.actorStats = new List<ActorStats>();
            }
            foreach (var item in Actors.instance.GetAllNearbyActors())
            {
                saveFileData.actorStats.Add(item.actorStats);
            }
            return saveFileData;
        }

        public static bool DoesSaveExist(string name)
        {
            string savePath = GetSavePath(name);
            if(File.Exists(savePath))
            {
                return true;
            }
            return false;
        }

        public static string GetSaveDirectory()
        {
            string savePath = Application.persistentDataPath;
#if UNITY_EDITOR
            Debug.Log($"Save Path: {savePath}");
#endif
            return savePath;
        }

        private static string GetSavePath(string name)
        {
            string savePath = Path.Combine(Application.persistentDataPath, name + ".sav");
#if UNITY_EDITOR
            Debug.Log($"Save Path: {savePath}");
#endif
            return savePath;
        }
    }
}
