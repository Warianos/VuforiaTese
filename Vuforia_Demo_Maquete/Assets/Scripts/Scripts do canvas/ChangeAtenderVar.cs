using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAtenderVar : MonoBehaviour {
    public GameObject gameController;
    // Use this for initialization
    
    public void changeAtendeuVar()
    {
        gameController.GetComponent<RaycastColliderDetection>().atendeu = !gameController.GetComponent<RaycastColliderDetection>().atendeu;
    }
}
