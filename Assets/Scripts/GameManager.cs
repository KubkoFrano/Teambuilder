using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] string firstScene;

    private void Start()
    {
        App.gameManager = this;
        StartSceneLoading("UIScene");
        StartSceneLoading(firstScene);
    }

    IEnumerator LoadSelectedScene(string sceneName)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!loading.isDone)
        {
            yield return null;
        }
    }

    IEnumerator UnloadSelectedScene(string sceneName)
    {
        AsyncOperation loading = SceneManager.UnloadSceneAsync(sceneName);
        while (!loading.isDone)
        {
            yield return null;
        }
    }

    public void StartSceneLoading(string sceneName)
    {
        StartCoroutine(LoadSelectedScene(sceneName));
    }

    public void StartSceneUnloading(string sceneName)
    {
        StartCoroutine(UnloadSelectedScene(sceneName));
    }
}