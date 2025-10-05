using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

namespace CS17.Core
{
    /// <summary>
    /// Manages scene loading and transitions
    /// </summary>
    public class SceneController : MonoBehaviour
    {
        private static SceneController _instance;
        public static SceneController Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("SceneController");
                    _instance = go.AddComponent<SceneController>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        public bool IsLoading { get; private set; }
        public float LoadingProgress { get; private set; }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #region Scene Loading

        /// <summary>
        /// Load scene by name
        /// </summary>
        public void LoadScene(string sceneName, Action onComplete = null)
        {
            StartCoroutine(LoadSceneAsync(sceneName, onComplete));
        }

        /// <summary>
        /// Load scene by build index
        /// </summary>
        public void LoadScene(int sceneIndex, Action onComplete = null)
        {
            StartCoroutine(LoadSceneAsync(sceneIndex, onComplete));
        }

        /// <summary>
        /// Reload current scene
        /// </summary>
        public void ReloadCurrentScene(Action onComplete = null)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            LoadScene(currentScene.name, onComplete);
        }

        /// <summary>
        /// Load main menu
        /// </summary>
        public void LoadMainMenu(Action onComplete = null)
        {
            LoadScene("MainMenu", onComplete);
        }

        /// <summary>
        /// Load game scene
        /// </summary>
        public void LoadGameScene(Action onComplete = null)
        {
            LoadScene("Main", onComplete);
        }

        #endregion

        #region Async Loading

        private IEnumerator LoadSceneAsync(string sceneName, Action onComplete)
        {
            IsLoading = true;
            LoadingProgress = 0f;

            // Publish loading started event
            EventSystem.Instance.Publish("SceneLoadingStarted");

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            asyncLoad.allowSceneActivation = false;

            // Wait until scene is almost loaded (0.9 = 90%)
            while (!asyncLoad.isDone)
            {
                LoadingProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

                // When progress reaches 90%, we can activate the scene
                if (asyncLoad.progress >= 0.9f)
                {
                    // Could add minimum loading time here for smooth transition
                    yield return new WaitForSeconds(0.5f);
                    asyncLoad.allowSceneActivation = true;
                }

                yield return null;
            }

            LoadingProgress = 1f;
            IsLoading = false;

            // Publish loading complete event
            EventSystem.Instance.Publish("SceneLoadingComplete");

            onComplete?.Invoke();
        }

        private IEnumerator LoadSceneAsync(int sceneIndex, Action onComplete)
        {
            IsLoading = true;
            LoadingProgress = 0f;

            EventSystem.Instance.Publish("SceneLoadingStarted");

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
            asyncLoad.allowSceneActivation = false;

            while (!asyncLoad.isDone)
            {
                LoadingProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

                if (asyncLoad.progress >= 0.9f)
                {
                    yield return new WaitForSeconds(0.5f);
                    asyncLoad.allowSceneActivation = true;
                }

                yield return null;
            }

            LoadingProgress = 1f;
            IsLoading = false;

            EventSystem.Instance.Publish("SceneLoadingComplete");

            onComplete?.Invoke();
        }

        #endregion

        #region Utility

        /// <summary>
        /// Get current active scene name
        /// </summary>
        public string GetCurrentSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }

        /// <summary>
        /// Get current active scene index
        /// </summary>
        public int GetCurrentSceneIndex()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }

        /// <summary>
        /// Check if a scene is loaded
        /// </summary>
        public bool IsSceneLoaded(string sceneName)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            return scene.isLoaded;
        }

        #endregion
    }
}
