using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisionByClick : MonoBehaviour {

    [SerializeField]
    private Transform objectToCollide;

    private RaycastHit hit;
    private Ray ray;



    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(" you clicked on " + hit.collider.gameObject.name);

            if (hit.collider.gameObject.name == "Your 3D Model Name")
            {
                // Write things you want to do when you click.
            }
        }
    }
}
