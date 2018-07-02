using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class filterON : MonoBehaviour {


    public GameObject gameController;
	// Use this for initialization
	
	
	

   public void changeFilter()
    {
        gameController.GetComponent<EletricityController>().filter = !gameController.GetComponent<EletricityController>().filter;
        //CancelInvoke();
    }
}
