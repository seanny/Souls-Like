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
                StartCoroutine(OnLoadScene(levelName));
            }
        }

        private IEnumerator OnLoadScene(string levelName)
        {
            var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
            AsyncOperation loadingScreenAsync = SceneManager.LoadSceneAsync($"Scenes/LoadingScreen", parameters);
            yield return new WaitForEndOfFrame();
            while(!loadingScreenAsync.isDone)
            {
                yield return null;
            }
            yield return new WaitForEndOfFrame();
            if (!initialObjectsLoaded)
            {
                SceneManager.LoadScene("Scenes/SampleScene");
                initialObjectsLoaded = true;
            }
            yield return new WaitForEndOfFrame();
            UnloadLevel();
            yield return new WaitForEndOfFrame();
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync($"Scenes/Levels/{levelName}", parameters);
            while(!asyncOperation.isDone)
            {
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForEndOfFrame();
            currentScene = SceneManager.GetSceneByName($"{levelName}");
            yield return new WaitForEndOfFrame();
            Scene loadingScene = SceneManager.GetSceneByName("LoadingScreen");
            if (loadingScene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(loadingScene);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}