using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneSingleton : MonoBehaviour
{

    private string levelName;
    AsyncOperation async;
    //public GameObject loadingSceneManager;
    public static LoadSceneSingleton instance;

    private string lastScene;


    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Debug.Log("o loadingSceneSingleton acordou");
    }

  

    public void StartLoading(string name)
    {
        levelName = name;
        StartCoroutine("Load");
    }

    IEnumerator Load()
    {
        gameObject.GetComponent<LoadingSceneManager>().changedScene = true;
        gameObject.GetComponent<LoadingSceneManager>().loadingMenu.SetActive(true);

        gameObject.GetComponent<Animator>().SetBool("canFade", true);
        yield return new WaitForSeconds(1f);
        async = SceneManager.LoadSceneAsync(levelName);
        //Debug.Log(levelName);
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

