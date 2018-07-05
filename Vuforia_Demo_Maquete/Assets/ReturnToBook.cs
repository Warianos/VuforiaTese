using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToBook : MonoBehaviour {

	// Use this for initialization
    private GameObject XMLManager;
	void Start () {
        XMLManager = GameObject.FindGameObjectWithTag("XMLManager");
        
        Debug.Log("o nome do manager é: " + XMLManager.name);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void returnToBook()
    {
        XMLManager.GetComponent<XMLManager>().LoadScene("MenuAberto");
    }
}
