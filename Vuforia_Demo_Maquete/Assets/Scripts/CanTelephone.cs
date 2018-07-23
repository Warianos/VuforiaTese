using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanTelephone : MonoBehaviour {
    public ParticleSystem smokePS;
    public ParticleSystem sparksPS;
    public ParticleSystem smokePSBottom;
    public ParticleSystem sparksPSBottom;
    private Animator buttonAnim;

    

    //public ParticleSystem s;
    public GameObject gameController;
    private Button button;
    private bool finishedFirstDemo;
    public GameObject infoPanel;
    public bool firstTimeAfterFirstDemo;
    private bool finishSecondDemo;
    // Use this for initialization
    void Start () {
        button = gameObject.GetComponent<Button>();
        finishedFirstDemo = false;
        finishSecondDemo = false;
        buttonAnim = gameObject.GetComponent<Animator>();
        firstTimeAfterFirstDemo = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (finishedFirstDemo == false)
        {
            if (smokePS.isPlaying || sparksPS.isPlaying || sparksPSBottom.isPlaying || smokePSBottom.isPlaying)
            {
                
                button.interactable = false;

            }

            


            if (!smokePS.IsAlive())
            {

                smokePS.gameObject.SetActive(false);
                //button.interactable = true;

            }
            if (!smokePSBottom.IsAlive())
            {
                smokePSBottom.gameObject.SetActive(false);
                //button.interactable = true;
            }

            if (!sparksPS.IsAlive())
            {
                sparksPS.gameObject.SetActive(false);
                //button.interactable = true;
            }
            if (!sparksPSBottom.IsAlive())
            {
                sparksPSBottom.gameObject.SetActive(false);
                //button.interactable = true;
            }
        }

        else if(finishedFirstDemo && firstTimeAfterFirstDemo)
        {
            buttonAnim.SetBool("canPulse", true);
            firstTimeAfterFirstDemo = false;
        }
        if (firstTimeAfterFirstDemo)
        {
            finishedFirstDemo = gameController.GetComponent<RaycastColliderDetection>().finishFirstDemo;
        }
        //if (finishedFirstDemo)





    }
    // verificar se esta a ser clicado enquanto ainda esta a correr de maneira a maximizar performance
    public void checkConditionsToPhone()
    {
        buttonAnim.SetBool("canPulse", false); //meter a animação a false quando se clica pois já fez o efeito que era dar atenção
        finishedFirstDemo = gameController.GetComponent<RaycastColliderDetection>().finishFirstDemo;
        finishSecondDemo = gameController.GetComponent<RaycastColliderDetection>().finishSecondDemo;
        if (finishedFirstDemo == false && smokePS.isPlaying == false)
        {
            //infoPanel.SetActive(true);
            smokePS.gameObject.SetActive(true);
            sparksPS.gameObject.SetActive(true);
            
            gameController.GetComponent<EletricityController>().telefonou = true;
            gameController.GetComponent<EletricityController>().eletricityBobineON = true;


        }

        if (finishedFirstDemo)
        {
            gameController.GetComponent<EletricityController>().telefonou = true;
            gameController.GetComponent<EletricityController>().possoAtender = true;
            gameController.GetComponent<EletricityController>().eletricityBobineON = true;
            
        }
        Debug.Log("o boleano de ter acabado primeiro demo é: " + finishedFirstDemo);
        Debug.Log("o boleano de ter acabado o segundo demo é: " + finishSecondDemo);

        if (!finishSecondDemo && smokePSBottom.isPlaying == false)
        {
            //infoPanel.SetActive(true);
            smokePSBottom.gameObject.SetActive(true);
            sparksPSBottom.gameObject.SetActive(true);

            


        }




    }
}
