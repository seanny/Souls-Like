using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SoulsLike
{
    public class LoadScene : MonoBehaviour
    {
        public static LoadScene instance { get; private set; }
        public Scene currentScene;

        private void Start()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }

        public bool initialObjectsLoaded = false;

        public void ReturnToMainMenu()
        {
            UnloadLevel();
            SceneManager.LoadScene("Scenes/MainMenu");
        }

        public void UnloadLevel()
        {
            if (currentScene.isLoaded)
            {
                Debug.Log($"Unloading scene {currentScene.name}");
                AsyncOperation async = SceneManager.UnloadSceneAsync(currentScene.buildIndex);
                async.allowSceneActivation = true;
            }
        }

        public void LoadLevel(string levelName)
        {
            if(Application.CanStreamedLevelBeLoaded($"Scenes/Levels/{levelName}") == true)
            {
                Debug.Log($"Loading {levelName}");
                StartCoroutine(OnLoadScene(levelName, Vector3.zero, Quaternion.identity));
            }
        }

        public void LoadLevel(string levelName, Vector3 position, Quaternion rotation)
        {
            if (Application.CanStreamedLevelBeLoaded($"Scenes/Levels/{levelName}") == true)
            {
                Debug.Log($"Loading {levelName}");
                StartCoroutine(OnLoadScene(levelName, position, rotation));
            }
        }

        private IEnumerator OnLoadScene(string levelName, Vector3 position, Quaternion rotation)
        {
            var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
            AsyncOperation loadingScreenAsync = SceneManager.LoadSceneAsync($"Scenes/LoadingScreen", parameters);
            while(!loadingScreenAsync.isDone)
            {
                yield return null;
            }
            if (!initialObjectsLoaded)
            {
                SceneManager.LoadScene("Scenes/SampleScene", parameters);
                initialObjectsLoaded = true;
            }
            UnloadLevel();
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync($"Scenes/Levels/{levelName}", parameters);
            while(!asyncOperation.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
            currentScene = SceneManager.GetSceneByName($"{levelName}");
            Scene mainMenu = SceneManager.GetSceneByName("MainMenu");
            if (mainMenu.isLoaded)
            {
                SceneManager.UnloadSceneAsync(mainMenu);
            }
            
            PlayerActor.instance.transform.position = position;
            PlayerActor.instance.transform.rotation = rotation;
            Scene loadingScene = SceneManager.GetSceneByName("LoadingScreen");
            if (loadingScene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(loadingScene);
            }
        }
    }
}