using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanTelephone : MonoBehaviour {
    public ParticleSystem smokePS;
    public ParticleSystem sparksPS;
    public ParticleSystem smokePSBottom;
    public ParticleSystem sparksPSBottom;

    //public ParticleSystem s;
    public GameObject gameController;
    private Button button;
    private bool finishedFirstDemo;
    public GameObject infoPanel;
	// Use this for initialization
	void Start () {
        button = gameObject.GetComponent<Button>();
        finishedFirstDemo = false;
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

        //if (finishedFirstDemo)





    }
    // verificar se esta a ser clicado enquanto ainda esta a correr de maneira a maximizar performance
    public void checkConditionsToPhone()
    {
        finishedFirstDemo = gameController.GetComponent<RaycastColliderDetection>().finishFirstDemo;
        if (finishedFirstDemo == false && smokePS.isPlaying == false)
        {
            
            smokePS.gameObject.SetActive(true);
            sparksPS.gameObject.SetActive(true);
            
            gameController.GetComponent<EletricityController>().telefonou = true;

        }

        if (finishedFirstDemo)
        {
            gameController.GetComponent<EletricityController>().telefonou = true;
            gameController.GetComponent<EletricityController>().possoAtender = true;
        }

        infoPanel.SetActive(true);
       

    }
}
