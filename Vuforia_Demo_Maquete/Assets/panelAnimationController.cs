using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class panelAnimationController : MonoBehaviour 
    , IPointerClickHandler
{
    Animator animator;
    bool clickedPanel;
    AnimatorStateInfo stateInfo;
    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        stateInfo = animator.GetNextAnimatorStateInfo(0);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                //comunicar com o gameController
        }
		*/
    }
  

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Nome do gameobject: " + gameObject.name);
        if (gameObject.tag == "Objectives")
        {
           // Debug.Log("Nome do gameobject: " + gameObject.name);
            if (animator.GetBool("clickedLeftPanel"))
            {
                animator.SetBool("clickedLeftPanel", false);
            }
            else
            {
                animator.SetBool("clickedLeftPanel", true);
            }
        }
        else if(gameObject.tag == "Tools")
        {
            if (animator.GetBool("clickedRightPanel"))
            {
                animator.SetBool("clickedRightPanel", false);
            }
            else
            {
                animator.SetBool("clickedRightPanel", true);
            }
        }

        else if (gameObject.tag == "InfoPanel")
        {
            //gameObject.SetActive(false);
            animator.SetBool("clickedOrTimeOut", true);
            
        }


    }
    public void Destroy()
    {

        Destroy(this.gameObject);
        Destroy();
    }

}
