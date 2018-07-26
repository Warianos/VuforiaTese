using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadinSceneManager : MonoBehaviour {

    public GameObject loadingMenu;
    GameObject exitBattle;
    GameObject changeLevel;

   

    LoadSceneSingleton scene;
    /*
    public LoadSceneSingleton Scene
    {
        get
        {
            return scene;
        }
    }
    */
    void Awake()
    {
        DontDestroyOnLoad(this);


        scene = LoadSceneSingleton.instance;

        

        loadingMenu.SetActive(false);
        

    }

    public void EnterInBattle()
    {
        StartCoroutine("LoadScreen");
    }

    IEnumerator LoadScreen()
    {
        loadingMenu.SetActive(true); // activar o canvas
        yield return new WaitUntil(() => LoadSceneSingleton.instance.LoadProgress() >= 0.7f);
        //faz algo
        yield return new WaitForSeconds(0.7f);
        scene.ActivateScene();
        yield return new WaitUntil(() => LoadSceneSingleton.instance.CheckIfSceneReady() == true);
        //faz algo
        yield return new WaitForSeconds(2);
        loadingMenu.SetActive(false);
    }

   
}
