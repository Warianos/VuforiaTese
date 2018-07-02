using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFalarVar : MonoBehaviour {

    public GameObject gameController;
    // Use this for initialization

    public void changeFalouVar()
    {
        gameController.GetComponent<RaycastColliderDetection>().speaking = !gameController.GetComponent<RaycastColliderDetection>().speaking;
    }
}

