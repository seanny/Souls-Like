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
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public bool initialObjectsLoaded = false;

        public void UnloadLevel()
        {
            if (currentScene.isLoaded)
            {
                AsyncOperation async = SceneManager.UnloadSceneAsync(currentScene);
                async.allowSceneActivation = true;
            }
        }

        public void LoadLevel(string levelName)
        {
            StartCoroutine(OnLoadScene(levelName));
        }

        private IEnumerator OnLoadScene(string levelName)
        {
            var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
            AsyncOperation loadingScreenAsync = SceneManager.LoadSceneAsync($"Scenes/LoadingScreen", parameters);
            yield return new WaitForEndOfFrame();
            while(!loadingScreenAsync.isDone)
            {
                yield return null;
                Debug.Log($"loadingScreenAsync: {loadingScreenAsync.progress}");
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
                Debug.Log($"asyncOperation: {asyncOperation.progress}");
            }
            yield return new WaitForEndOfFrame();
            currentScene = SceneManager.GetSceneByName($"{levelName}");
            yield return new WaitForEndOfFrame();
            Scene loadingScreen = SceneManager.GetSceneByName("LoadingScreen");
            SceneManager.UnloadSceneAsync(loadingScreen);
            yield return new WaitForEndOfFrame();
        }
    }
}