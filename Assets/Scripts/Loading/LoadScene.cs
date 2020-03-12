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
            else Destroy(this);
        }

        public bool initialObjectsLoaded = false;

        /// <summary>
        /// Return to the main menu.
        /// </summary>
        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene("Scenes/MainMenu");
            UnloadLevel();
        }

        /// <summary>
        /// Unload the current level
        /// </summary>
        public void UnloadLevel()
        {
            if (currentScene.isLoaded)
            {
                Debug.Log($"Unloading scene {currentScene.name}");
                AsyncOperation async = SceneManager.UnloadSceneAsync(currentScene.buildIndex);
                async.allowSceneActivation = true;
            }
        }

        /// <summary>
        /// Load a new level, unloading the old level.
        /// </summary>
        /// <param name="levelName"></param>
        public void LoadLevel(string levelName)
        {
            if(Application.CanStreamedLevelBeLoaded($"Scenes/Levels/{levelName}") == true)
            {
                Debug.Log($"Loading {levelName}");
                StartCoroutine(OnLoadScene(levelName, Vector3.zero, Quaternion.identity));
            }
        }

        /// <summary>
        /// Load a new level, unloading the old level whilst setting the players position and rotation.
        /// </summary>
        /// <param name="levelName"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public void LoadLevel(string levelName, Vector3 position, Quaternion rotation)
        {
            if (Application.CanStreamedLevelBeLoaded($"Scenes/Levels/{levelName}") == true)
            {
                Debug.Log($"Loading {levelName}");
                StartCoroutine(OnLoadScene(levelName, position, rotation));
            }
        }

        /// <summary>
        /// OnLoadScene, called when a new scene is loaded via the level loader.
        /// </summary>
        /// <param name="levelName"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
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
                AsyncOperation sampleSceneAsync = SceneManager.LoadSceneAsync("Scenes/SampleScene", parameters);
                while (!sampleSceneAsync.isDone)
                {
                    yield return null;
                }
                initialObjectsLoaded = true;
            }
            UnloadLevel();
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync($"Scenes/Levels/{levelName}", parameters);
            while(!asyncOperation.isDone)
            {
                yield return null;
            }
            currentScene = SceneManager.GetSceneByName($"{levelName}");
            Scene mainMenu = SceneManager.GetSceneByName("MainMenu");
            if (mainMenu.isLoaded)
            {
                SceneManager.UnloadSceneAsync(mainMenu);
            }

            PlayerActor.instance.transform.position = position;
            PlayerActor.instance.transform.rotation = rotation;
            yield return new WaitForEndOfFrame();
            Scene loadingScene = SceneManager.GetSceneByName("LoadingScreen");
            if (loadingScene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(loadingScene);
            }
        }
    }
}