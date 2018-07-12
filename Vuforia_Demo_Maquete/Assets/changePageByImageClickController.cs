using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePageByImageClickController : MonoBehaviour {

    public static changePageByImageClickController changePageClickController;
    private PageController pageControllerScript;
    // Use this for initialization
    void Awake () {
        changePageClickController = this;
    
    }

    private void Start()
    {
        pageControllerScript = this.gameObject.GetComponent<PageController>();
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void ChangePageByClick(string clickedObjectName)
    {
        //////////////////////////////////////////////////////////////////////////////////// se tiver na primeira pagina dos objectos/////////////////////////////////////////
        if (pageControllerScript.targetObject.name ==  "Tutorial2") 
        {
            if (clickedObjectName == "Telephone")//se clicar no telefone n acontece anda
            {
                //NÃO ACONTECE NADA
            }

            else if(clickedObjectName == "FreqMachine") // se clicar no freqmachine tando na pagina do telefone muda uma pagina para a esquerda
            {
                pageControllerScript.turnPage(pageControllerScript.pageAnimators[2], 2, false);
            }
           //ADICIONAR AQUI MAIS CONDIÇÕES DE OUTROS BUTÕES
        }

        else if (pageControllerScript.targetObject.name == "Page1")
        {
            if (clickedObjectName == "Telephone")// se tiver na pagina 1 e clicar no telefone anda uma para a direita
            {
                pageControllerScript.turnPage(pageControllerScript.pageAnimators[2], 2, true);
            }
            else if (clickedObjectName == "FreqMachine")
            {
                //NÃO ACONTECE NADA
            }
            
        }
        
      
    }
}
