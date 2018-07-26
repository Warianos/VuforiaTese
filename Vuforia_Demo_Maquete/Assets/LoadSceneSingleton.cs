using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneSingleton : MonoBehaviour
{

    private string levelName;
    AsyncOperation async;

    public static LoadSceneSingleton instance;

    private string lastScene;


    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

  

    public void StartLoading(string name)
    {
        levelName = name;
        StartCoroutine("load");
    }

    IEnumerator load()
    {
        async = SceneManager.LoadSceneAsync(levelName);
        async.allowSceneActivation = false;
        yield return async;
    }

    public void ActivateScene()
    {
        async.allowSceneActivation = true;
    }

    public float LoadProgress()
    {
        return async.progress;
    }

    public bool CheckIfSceneReady()
    {
        return async.allowSceneActivation;
    }
}

