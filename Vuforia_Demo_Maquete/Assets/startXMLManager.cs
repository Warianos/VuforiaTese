using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startXMLManager : MonoBehaviour {
    public GameObject xmlManager;
	// Use this for initialization

	void Awake () {
        if(!GameObject.FindGameObjectWithTag("XMLManager") ) //se nao existe, passa a existir
        {
            Instantiate(xmlManager);
        }
        
	}
	
	
}
