using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SoulsLike
{
    public class SaveInput : MonoBehaviour
    {
        public static SaveInput instance { get; private set; }

        private void Start()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public SaveFileData LoadMostRecentSave()
        {
            SaveFileData saveFileData = null;
            var directory = new DirectoryInfo(SaveController.GetSaveDirectory());
            var file = (from f in directory.GetFiles("*.sav", SearchOption.TopDirectoryOnly)
                        orderby f.CreationTime descending
                        select f).First();

            string saveName = file.Name.Replace(".sav", string.Empty);
            Debug.Log($"Most recent save: {file.FullName} ({saveName})");
            if(file.Exists)
            {
                saveFileData = SaveController.LoadSave(saveName);
            }
            return saveFileData;
        }

        public void LoadGame(string saveName)
        {
            SaveController.LoadSave(saveName);
        }

        public void SaveGame(string saveName, bool quickSave)
        {
            SaveController.CreateSave(saveName, quickSave);
        }

        private void Update()
        {
            // TODO: Convert into the new unity input system.
            if(Input.GetKeyUp(KeyCode.F5) && SceneManager.GetSceneByName("SampleScene").isLoaded)
            {
                SaveGame("QuickSave", true);
            }
        }
    }
}
