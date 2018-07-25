using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {


	
	

    public void SceneChanger(string sceneName)
    {
       if (!SceneManager.GetSceneByName(sceneName).IsValid()){ //isvalid() tras false se for verdade que não seja valido... portanto !
            SceneManager.LoadScene(sceneName);
       }
       else
       {
           Debug.Log("Erro: Não é uma scene válida");
       }
        
    }
}
