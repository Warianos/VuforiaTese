using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotationController : MonoBehaviour {

    
    public GameObject cameraGameObject;
    public Animator cameraAnimator;
	// Use this for initialization
	void Start () {
        cameraAnimator = cameraGameObject.GetComponent<Animator>();
    }

   
    // The target marker.
    Transform target;
    private bool canMove = false;

    // Angular speed in radians per sec.
    float speed;
   
    void Update()
    {
       
        
    }
    public void moveCamera(GameObject info)
    {
        Debug.Log("Entrei aqui no move camera");
        Debug.Log(info.name);
        if (info.transform.name == "Info1")
        {
            /*if (cameraAnimator.GetBool("zoomQuestion2") == true)
            {
                cameraAnimator.SetBool("zoomQuestion1",true);
                cameraAnimator.SetBool("zoomQuestion2", false);
                cameraAnimator.SetBool("zoomQuestion3", false);
            }

            else if (cameraAnimator.GetBool("zoomQuestion3") == true)
            {
                cameraAnimator.SetBool("zoomQuestion1", true);
                cameraAnimator.SetBool("zoomQuestion2", false);
                cameraAnimator.SetBool("zoomQuestion3", false);
            }
            else
            {
                cameraAnimator.SetBool("zoomQuestion1", true);
                cameraAnimator.SetBool("zoomQuestion2", false);
                cameraAnimator.SetBool("zoomQuestion3", false);
            }
            */
            if (cameraAnimator.GetBool("zoomQuestion1") == true)
            {
                cameraAnimator.SetBool("zoomQuestion1", false);
            }
            else
            {
                cameraAnimator.SetBool("zoomQuestion1", true);
                cameraAnimator.SetBool("zoomQuestion2", false);
                cameraAnimator.SetBool("zoomQuestion3", false);
            }
            


        }
        else if (info.transform.name == "Info2")
        {
            if (cameraAnimator.GetBool("zoomQuestion2") == true)
            {
                cameraAnimator.SetBool("zoomQuestion2", false);
            }
            else
            {
                cameraAnimator.SetBool("zoomQuestion1", false);
                cameraAnimator.SetBool("zoomQuestion2", true);
                cameraAnimator.SetBool("zoomQuestion3", false);
            }
            
        }
        else if (info.transform.name == "Info3")
        {
            if (cameraAnimator.GetBool("zoomQuestion3") == true)
            {
                cameraAnimator.SetBool("zoomQuestion3", false);
            }
            else
            {
                cameraAnimator.SetBool("zoomQuestion1", false);
                cameraAnimator.SetBool("zoomQuestion2", false);
                cameraAnimator.SetBool("zoomQuestion3", true);
            }
           
        }
        
    }
}
