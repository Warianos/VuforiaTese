﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PageController : MonoBehaviour {
    
    //pages variables
    public pageData[] pages;
    private bool correr = true;

    //animators variables
    public List<Animator> pageAnimators;
    private Animator bookAnimator;
    private AnimatorStateInfo animationStateFirstPage;
    private AnimatorClipInfo[] myAnimatorClipFirstPage;
    private float myFirstPageTime;
    private float myFirstPageLength;
    private float myFirstPageIniTime;
    private float myFirstPageIniTimeHelper;

    //raycast variables
    private RaycastHit hit;

    
    public GameObject targetObject;

    private float mouseXIniPos;
    private float mouseXEndPos;

    //drag variables
    private bool leftDrag;
    private bool rightDrag;

    //colliders
    private Collider[] bookColliders;
    public bool changeCollider;
    

    //generic class variables
    private bool bookIsOpen;
    private bool enabledFirstPage;
    private bool activatedFirstTwoCanvas;
    public int bookLayerMask;
    private bool didTurnPage;
    private int actualPageIndex;
    private bool actualGesture;

    //XML VARIABLES
    public GameObject xmlManager;
    public XMLManager xmlManagerScript;

    //sprites variables
    Sprite[] stickerSprites;
    GameObject[] pageLeftImages;

    // Use this for initialization
    void Start () {
        pageAnimators = new List<Animator>();
        bookAnimator = gameObject.GetComponent<Animator>();
        mouseXIniPos = 0;
        mouseXEndPos = 0;
        myFirstPageTime = 0;
        myFirstPageLength = 0;
        bookIsOpen = false;
        leftDrag = false;
        rightDrag = false;
        actualGesture = false;
        didTurnPage = false;
        actualPageIndex = 0;
        activatedFirstTwoCanvas = false;
        enabledFirstPage = false;
        bookColliders = gameObject.GetComponents<Collider>();

         
    // Use this for initialization
    
        if (GameObject.FindGameObjectWithTag("XMLManager")) //se nao existe, passa a existir
        {
            //Instantiate(xmlManager);
            //xmlManager = GameObject.FindGameObjectWithTag("XMLManager");
            //Debug.Log("existe XML");
            //Debug.Log(xmlManager.name);
            //XMLManager.ins.LoadItems();
            //Debug.Log(XMLManager.ins.itemDB.list[0].objectName);
        }


        //Debug.Log(xmlManager.GetComponent<XMLManager>().itemDB.list[0].objectName);

        //xmlManagerScript = xmlManager.gameObject.GetComponent<XMLManager>();
        //xmlManagerScript.LoadItems();

        stickerSprites = Resources.LoadAll<Sprite>("ObjectosColecaoReduced");
        //FIND OBJECTS WITH TAG SÓ FUNCIONA SE O OBJECTO ESTIVER ACTIVO NA HIERARQUIA
        //pageLeftImages = GameObject.FindGameObjectsWithTag("SmallImages");
        //Debug.Log(pageLeftImages[0].name);

        foreach (pageData pagedata in pages)
        {
            pageAnimators.Add(pagedata.page.gameObject.GetComponent<Animator>());
            
            if (pagedata.canvasRight != null)
            {
                //vai colocar os componentes do canvas direito com rotação 180 para ficar na rotação da pagina
                foreach (Transform child in pagedata.canvasRight.transform)
                {
                    
                    child.localRotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
                    

                }
                //se tiver na pagina do telefone faz load das coisas do telefone
                if (pagedata.name == "Page1")
                {
                    foreach (MuseumObject obj in XMLManager.ins.itemDB.list)
                    {

                        RefreshBookInfo(pagedata, obj, "Telephone");
                    }
                }
                if (pagedata.name == "Page2")
                {
                    foreach (MuseumObject obj in XMLManager.ins.itemDB.list)
                    {

                        RefreshBookInfo(pagedata, obj, "FreqMachine");
                    }
                }
                //ADICIONAR AQUI O RESPECTIVO DE CADA PÁGINA


            }
            //como queremos fazer com que a página da esquerda esteja sempre actualizada e são basicamente copias umas das outras
            //vai encontrar todos os objectos na cena com essa tag, depois iteramos e comparamos com a base de dados

            if (pagedata.canvasLeft != null)
            {

                if(pagedata.name == "Tutorial2" || pagedata.name == "Page1")
                {

               
                    //vai colocar os componentes do canvas direito com rotação 180 para ficar na rotação da pagina
                    foreach (Transform child in pagedata.canvasLeft.transform) //buscar os filho de canvas
                    {

                        if (child.name == "Panel")
                        {
                            foreach (Transform image in child)//buscar os filhos de panel
                            {
                                

                                if (image.childCount > 0)
                                { //se tiver filhos quer dizer que tem uma imagem para ser alterada
                                    
                                    foreach (MuseumObject obj in XMLManager.ins.itemDB.list) //iterar pelos objectos existentes no xml
                                    {
                                        Debug.Log("O nome do objecto é: " + obj.objectName);
                                        Debug.Log("O nome da imagem é: " + image.name);
                                        if (image.name == obj.objectName)
                                        {
                                            
                                            RefreshLeftCanvas(image, obj);
                                            break;
                                        }
                                        
                                        //break;
                                    }
                                }
                            }
                        }
                    }
                }



                //ADICIONAR AQUI O RESPECTIVO DE CADA PÁGINA


            }
        }

       

        animationStateFirstPage = bookAnimator.GetCurrentAnimatorStateInfo(0);
        myAnimatorClipFirstPage = bookAnimator.GetCurrentAnimatorClipInfo(0);
    }

    //funciona partindo do principio que os nomes da UI entre objectos se mantêm
    void RefreshLeftCanvas(Transform imageGameObject, MuseumObject obj)
    {
        int indexEarnedSticker = 0;
        int indexDidntEarnSticker = 0;
        if(obj.objectName == "Telephone")
        {
            indexEarnedSticker = 4;
            indexDidntEarnSticker = 3;
        }
        else if (obj.objectName == "FreqMachine")
        {
            Debug.Log("ENTREI AQUI NO FREQMACHINEEEEEE");
            indexEarnedSticker = 1;
            indexDidntEarnSticker = 0;
        }

        //ADICIONAR AQUI OUTROS OBJECTOS 

        if (obj.earnedSticker)
        {
            Debug.Log("entrei no sticker = true");
            imageGameObject.Find("ObjectImage").GetComponent<Image>().sprite = stickerSprites[indexEarnedSticker];//posição do telefone ganho

        }

        else
        {
            Debug.Log("entrei no sticker = false");
            imageGameObject.Find("ObjectImage").GetComponent<Image>().sprite = stickerSprites[indexDidntEarnSticker];

        }

            
        
    }
    void RefreshBookInfo(pageData page, MuseumObject obj, string objectName)
    {

        int indexEarnedSticker = 0;
        int indexDidntEarnSticker = 0;
        if (objectName == "Telephone")
        {
            indexEarnedSticker = 4;
            indexDidntEarnSticker = 3;
        }
        else if (objectName == "FreqMachine")
        {
            indexEarnedSticker = 1;
            indexDidntEarnSticker = 0;
        }
        //ADICIONAR AQUI OUTROS OBJECTOS 

        if (obj.objectName == objectName)
        {

            if (obj.earnedSticker)
            {
                Debug.Log("entrei no sticker = true");
                page.canvasRight.transform.Find("ClickableImage").Find("ObjectImage").GetComponent<Image>().sprite = stickerSprites[indexEarnedSticker];//posição do telefone ganho
            }
            else
            {
                Debug.Log("entrei no sticker = false");
                page.canvasRight.transform.Find("ClickableImage").Find("ObjectImage").GetComponent<Image>().sprite = stickerSprites[indexDidntEarnSticker];
            }
            if (obj.discoveredFirstInfo)
            {
                page.canvasRight.transform.Find("Panel").Find("Info1").Find("Text").GetComponent<Text>().text = obj.objectInfoText1;
            }
            else if (!obj.discoveredFirstInfo)
            {
                page.canvasRight.transform.Find("Panel").Find("Info1").Find("Text").GetComponent<Text>().text = "Click in the button above to discover more info";
            }

            if (obj.discoveredSecondInfo)
            {
                page.canvasRight.transform.Find("Panel").Find("Info2").Find("Text").GetComponent<Text>().text = obj.objectInfoText2;
            }
            else if (!obj.discoveredSecondInfo)
            {
                page.canvasRight.transform.Find("Panel").Find("Info2").Find("Text").GetComponent<Text>().text = "Click in the button above to discover more info";
            }

            if (obj.discoveredThirdInfo)
            {
                page.canvasRight.transform.Find("Panel").Find("Info3").Find("Text").GetComponent<Text>().text = obj.objectInfoText3;
            }
            else if (!obj.discoveredThirdInfo)
            {
                page.canvasRight.transform.Find("Panel").Find("Info3").Find("Text").GetComponent<Text>().text = "Click in the button above to discover more info";
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //TENHO DE COLOCAR OS COLLIDERS ENABLEDS DEPENDENDO DOS DRAGS
        animationStateFirstPage = pageAnimators[0].GetCurrentAnimatorStateInfo(0);
        myAnimatorClipFirstPage = pageAnimators[0].GetCurrentAnimatorClipInfo(0);




        myFirstPageIniTimeHelper = Time.deltaTime;
        //Isto está feito de maneira a que o utilizador tenha mesmo de fazer drag, caso clique para fazer drag mas arraste apenas um pouco, não conta como drag

        if (bookIsOpen && !enabledFirstPage)
        {
            pages[1].page.SetActive(true);
            enabledFirstPage = true;
            myFirstPageLength = animationStateFirstPage.length;
            //Debug.Log("Comprimento" + myFirstPageLength);
        }
        //se o livro tiver aberto E ANIMAÇÃO TER ACABADO DAPRIMEIRA PAGINA
        if(bookIsOpen && !activatedFirstTwoCanvas && myFirstPageIniTime <= myFirstPageLength + myFirstPageLength / 2)
        {
            myFirstPageIniTime += myFirstPageIniTimeHelper;
            //se o tempo que passou for superior a metade do lentgh, quer dizer que  a pagina está a meio e poderá mostrar o canvas da segunda pagina
            if (myFirstPageIniTime >= myFirstPageLength + myFirstPageLength / 2)
            {
                //Debug.Log("entrei");
                pages[0].canvasLeft.gameObject.SetActive(true);
                pages[1].canvasRight.gameObject.SetActive(true);
                activatedFirstTwoCanvas = true;
                myFirstPageIniTime = 0;
            }
        }
        if(!bookIsOpen && activatedFirstTwoCanvas && myFirstPageIniTime <= myFirstPageLength + myFirstPageLength / 2)
        {
            myFirstPageIniTime += myFirstPageIniTimeHelper;
            if (myFirstPageIniTime >= myFirstPageLength + myFirstPageLength / 2)
            {
               // Debug.Log("entrei");
                pages[0].canvasLeft.gameObject.SetActive(false);
                pages[1].canvasRight.gameObject.SetActive(false);
                activatedFirstTwoCanvas = false;
                myFirstPageIniTime = 0;
            }
        }

        
        //pagina 2

        // Debug.Log("myFirstPageIniTime: " + myFirstPageIniTime);
        // Debug.Log("myFirstPageLength: " + myFirstPageLength);




        if (Input.GetMouseButtonDown(0)/* && !EventSystem.current.IsPointerOverGameObject()*/)
        {
            mouseXIniPos = Input.mousePosition.x; // da o valor do mouse na posição X normalizado entre -1 a 1 tendo como base 
            //Debug.Log("entrei aqui no mouseIniPos" + mouseXIniPos);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (bookIsOpen) // se o livro estiver aberto , o raiu vai excluir o collider do livro de maneira a puder colider com os colliders das folhas
            {
                
                


                //}
                //Debug.Log("entreia qui no bookIsOpen");
                if (Physics.Raycast(ray, out hit, 500, bookLayerMask))
                {

                    Debug.DrawRay(ray.origin, ray.direction, Color.red, 100, true);


                    //if (hit.collider.gameObject.name == "Tutorial1")
                    // {
                    //  Debug.Log("entrei aqui no tutorial1");
                        targetObject = hit.collider.gameObject;
                    //  Debug.Log("nome do argetObject no Page Controller: " + targetObject);
                    // }

                    Debug.Log("targetObjectName: " + targetObject.name );
                }
            }
            // caso o livro esteja fechado o raio vai colidir com o livro de maneira a abri-lo
            else
            {
                if (Physics.Raycast(ray, out hit, 500))
                {
                    Debug.DrawRay(ray.origin, ray.direction, Color.red, 100, true);
                    if (hit.collider.gameObject.tag == "Book")
                    {
                        openBook();
                        

                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0) /*&& !EventSystem.current.IsPointerOverGameObject()*/)
        {
            mouseXEndPos = Input.mousePosition.x;
           // Debug.Log("entrei aqui no mouseEndPos" + mouseXEndPos);
            if (bookIsOpen)
            {
                if (mouseXIniPos - mouseXEndPos > 0.0f) //drag esquerda
                {
                    mouseXEndPos = 0;//reset a variavel
                    mouseXIniPos = 0;//reset a variavel

                    //houve drag para a esquerda
                    // Debug.Log("Houve drag para a esquerda");

                    
                    if (targetObject.name == "Tutorial2")
                    {
                        turnPage(pageAnimators[1], 1, false);
                        
                    }
                    if (targetObject.name == "Page1")
                    {
                        turnPage(pageAnimators[2], 2, false);
                    }
                    if (targetObject.name == "Page2")
                    {
                        turnPage(pageAnimators[3], 3, false);
                    }
                }

                if (mouseXIniPos - mouseXEndPos < 0.0f)// drag direita
                {
                    mouseXEndPos = 0;//reset a variavel
                    mouseXIniPos = 0;//reset a variavel

                    if (targetObject.name == "Tutorial1")
                    {
                        closeBook();
                    }
                    if (targetObject.name == "Tutorial2")
                    {
                        turnPage(pageAnimators[1] , 1 , true);
                    }
                    if (targetObject.name == "Page1")
                    {
                        turnPage(pageAnimators[2] , 2, true);
                    }
                    if (targetObject.name == "Page2")
                    {
                        turnPage(pageAnimators[3] , 3, true);
                    }
                    // Debug.Log("Houve drag para a direita");
                }
                
            }
            //Debug.Log("mouseXIniPos - mouseXEndPos: " + (mouseXIniPos - mouseXEndPos));
        }
        activateOrDeactivatePages(myFirstPageIniTimeHelper, actualPageIndex, actualGesture);
    }

    /*void OnMouseDrag(PointerEventData eventData)
    {
        // Debug.Log("entrei aqui no mouseDrag");
       // Debug.Log(eventData.pressPosition);
        //Debug.Log(eventData.position);
        //Debug.Log("entrei aqui no mouseEndPos" + mouseXEndPos);



    }*/

    void openBook()
    {
        if (!bookAnimator.GetBool("openBook"))
        {
            bookAnimator.SetBool("openBook", true);
            bookColliders[0].enabled = false; // colocar o collider maior quando o livro está aberto
            bookColliders[1].enabled = false; // colocar o collider maior quando o livro está aberto

            pageAnimators[0].SetBool("bookIsOpen", true);// virar primeira página do livro ao mesmo tempo que o livro abre
            pageAnimators[1].SetBool("bookIsOpen", true);// virar primeira página do livro ao mesmo tempo que o livro abre
            pageAnimators[2].SetBool("bookIsOpen", true);// virar primeira página do livro ao mesmo tempo que o livro abre
            pageAnimators[3].SetBool("bookIsOpen", true);// virar primeira página do livro ao mesmo tempo que o livro abre

            pageAnimators[0].SetBool("dragLeft", true);// virar primeira página do livro ao mesmo tempo que o livro abre
            pageAnimators[0].SetBool("dragRight", false);

            //colocar a pagina 2 no sitio certo
            
            pageAnimators[1].SetBool("bookIsOpen", true);// virar primeira página do livro ao mesmo tempo que o livro abre

            Collider[] page1Colliders = pages[0].page.GetComponents<Collider>();
            Collider[] page2Colliders = pages[1].page.GetComponents<Collider>();
            page1Colliders[0].enabled = true;
            page1Colliders[1].enabled = false;

            page2Colliders[0].enabled = false;
            page2Colliders[1].enabled = true;

            bookIsOpen = true;

            //Debug.Log("collilder 1 está : " + page1Colliders[0].enabled);
           // Debug.Log("collilder 2 está : " + page1Colliders[1].enabled);
        }
    }

    void closeBook()
    {
        if (bookAnimator.GetBool("openBook"))
        {
            pageAnimators[0].SetBool("dragLeft", false);
            pageAnimators[0].SetBool("dragRight", true);// virar primeira página do livro ao mesmo tempo que o livro abre

            bookAnimator.SetBool("openBook", false);
            bookColliders[0].enabled = false; // colocar o collider menor quando o livro está fechado
            bookColliders[1].enabled = true; // colocar o collider menor quando o livro está fechado

            //colocar a pagina 2 no sitio certo
            pageAnimators[0].SetBool("bookIsOpen", false);// virar primeira página do livro ao mesmo tempo que o livro abre
            pageAnimators[1].SetBool("bookIsOpen", false);// virar primeira página do livro ao mesmo tempo que o livro abre
            pageAnimators[2].SetBool("bookIsOpen", false);// virar primeira página do livro ao mesmo tempo que o livro abre
            pageAnimators[3].SetBool("bookIsOpen", false);// virar primeira página do livro ao mesmo tempo que o livro abre

            Collider[] page1Colliders = pages[0].page.GetComponents<Collider>();
            page1Colliders[0].enabled = false;
            page1Colliders[1].enabled = false;

            Collider[] page2Colliders = pages[1].page.GetComponents<Collider>();
            page2Colliders[0].enabled = false;
            page2Colliders[1].enabled = false;

            //pages[1].page.SetActive(false);
            bookIsOpen = false;
            enabledFirstPage = false;
        }
    }

   public void turnPage(Animator pageAnimator, int pageIndex, bool right)
    {
        actualPageIndex = pageIndex;
        actualGesture = right;
        didTurnPage = true;
        Debug.Log("pageIndex: " + actualPageIndex);
        Debug.Log("actualGesture: " + actualGesture);
        //Direita
       
        if (pageIndex != 0 && pageIndex != pages.Length - 1) {
            if (right)
            {
                
                //TENHO DE ATIVAR OS COLLIDERS DE ACORDO COM O INDEX SE INDEX 2 ANIMA PARA ESQUERDA ENTÃO TENHO DE DESATIVAR O INDEX 1 E ACTIVAR O INDEX 3 E ACTIVAR
                //RESPECTIVOS CANVAS
                if (pageAnimator.GetBool("dragLeft") && !pageAnimator.GetBool("dragRight") && pageAnimator.GetBool("bookIsOpen"))
                {
                    pageAnimator.SetBool("dragLeft", false);// virar primeira página do livro ao mesmo tempo que o livro abre
                    pageAnimator.SetBool("dragRight", true);
                    //pageAnimator.SetBool("canDrag", false);
                    Debug.Log("entrei na direita 1: ");
                    Collider[] page1Colliders = pages[pageIndex].page.GetComponents<Collider>();
                    page1Colliders[0].enabled = false;
                    page1Colliders[1].enabled = true;

                }
            }
            //Esquerda
            else if (!right)
            {
                Debug.Log("entrei na esquerda: ");
                if (!pageAnimator.GetBool("dragLeft") && !pageAnimator.GetBool("dragRight") && pageAnimator.GetBool("bookIsOpen"))
                {
                    pageAnimator.SetBool("dragLeft", true);// virar primeira página do livro ao mesmo tempo que o livro abre
                    pageAnimator.SetBool("dragRight", false);
                    //pageAnimator.SetBool("canDrag", false);
                    Debug.Log("entrei na esquerda 1: ");
                    Collider[] page1Colliders = pages[pageIndex].page.GetComponents<Collider>();
                    page1Colliders[0].enabled = true;
                    page1Colliders[1].enabled = false;
                }

                else if (!pageAnimator.GetBool("dragLeft") && pageAnimator.GetBool("dragRight") && pageAnimator.GetBool("bookIsOpen"))
                {
                    pageAnimator.SetBool("dragLeft", true);// virar primeira página do livro ao mesmo tempo que o livro abre
                    pageAnimator.SetBool("dragRight", false);
                    //pageAnimator.SetBool("canDrag", false);
                    Debug.Log("entrei na esquerda 2");
                    Collider[] page1Colliders = pages[pageIndex].page.GetComponents<Collider>();
                    page1Colliders[0].enabled = true;
                    page1Colliders[1].enabled = false;
                }

            }


            /*
            else if (pageAnimator.GetBool("dragRight") && !pageAnimator.GetBool("dragLeft") && pageAnimator.GetBool("bookIsOpen"))
            {
                pageAnimator.SetBool("dragLeft", true);// virar primeira página do livro ao mesmo tempo que o livro abre
                pageAnimator.SetBool("dragRight", false);
                Debug.Log("entrei 3");
                Collider[] page1Colliders = pages[pageIndex].page.GetComponents<Collider>();
                page1Colliders[0].enabled = true;
                page1Colliders[1].enabled = false;
            }*/
        }
      
    }

    void pageToRight(Animator pageAnimator, int pageIndex)
    {
        pageAnimator.SetBool("dragLeft", false);// virar primeira página do livro ao mesmo tempo que o livro abre
        pageAnimator.SetBool("dragRight", true);
    }

    void rotateCanvasAccordingToSideInPage()
    {

    }

    void activateOrDeactivatePages(float deltaTime,int actualPageIndex,bool right)
    {
       // Debug.Log("entrei aqui no desactivar paginas");
        if (actualPageIndex != 0 && actualPageIndex != pages.Length - 1) //ou seja se tiver na posição 1 2 ou numero de paginas -1 faz o de baixo
        {
            if (!right)
            {
               // Debug.Log("entrei AQUI NA ESQUERDA CARALHO");
                if (bookIsOpen && didTurnPage && myFirstPageIniTime <= myFirstPageLength + myFirstPageLength / 2)
                {
                   
                    myFirstPageIniTime += deltaTime;
                    //Debug.Log("tempoInicial DA ESQUERDA" + myFirstPageIniTime);
                    if (myFirstPageIniTime >= myFirstPageLength + myFirstPageLength / 2)
                    {
                       
                        //PAGINA QUE ESTAVA NA ESQUERDA
                        pages[actualPageIndex - 1].canvasLeft.gameObject.SetActive(false);
                        pages[actualPageIndex - 1].canvasRight.gameObject.SetActive(false);
                        Collider[] page1Colliders = pages[actualPageIndex - 1].page.GetComponents<Collider>();
                        page1Colliders[0].enabled = false;
                        page1Colliders[1].enabled = false;
                        //pages[actualPageIndex - 1].page.SetActive(false);

                        //PAGINA ESQUERDA
                        pages[actualPageIndex].canvasRight.gameObject.SetActive(false);
                        pages[actualPageIndex].canvasLeft.gameObject.SetActive(true);
                        Collider[] page2Colliders = pages[actualPageIndex].page.GetComponents<Collider>();
                        page2Colliders[0].enabled = true;
                        page2Colliders[1].enabled = false;

                        //PAGINA DIREITA
                       // pages[actualPageIndex + 1].page.SetActive(true);
                        pages[actualPageIndex + 1].canvasRight.gameObject.SetActive(true);
                        pages[actualPageIndex + 1].canvasLeft.gameObject.SetActive(false);
                        Collider[] page3Colliders = pages[actualPageIndex + 1].page.GetComponents<Collider>();
                        page3Colliders[0].enabled = false;
                        page3Colliders[1].enabled = true;
                        didTurnPage = false;
                        myFirstPageIniTime = 0;
                    }
                }

            }
            else if (right)
            {
                //Debug.Log("entrei AQUI NA DIREITA CARALHO");
                if (bookIsOpen && didTurnPage && myFirstPageIniTime <= myFirstPageLength + myFirstPageLength / 2)
                {
                    myFirstPageIniTime += deltaTime;
                    //Debug.Log("tempoInicial DA DIREITA" + myFirstPageIniTime);
                    if (myFirstPageIniTime >= myFirstPageLength + myFirstPageLength / 2)
                    {
                        //Debug.Log("entrei");
                       // pages[actualPageIndex - 1].page.SetActive(true);
                        Collider[] page1Colliders = pages[actualPageIndex - 1].page.GetComponents<Collider>();
                        page1Colliders[0].enabled = true;
                        page1Colliders[1].enabled = false;
                        pages[actualPageIndex - 1].canvasLeft.gameObject.SetActive(true);
                        pages[actualPageIndex - 1].canvasRight.gameObject.SetActive(false);

                        Collider[] page2Colliders = pages[actualPageIndex].page.GetComponents<Collider>();
                        page2Colliders[0].enabled = false;
                        page2Colliders[1].enabled = true;
                        pages[actualPageIndex].canvasLeft.gameObject.SetActive(false);
                        pages[actualPageIndex].canvasRight.gameObject.SetActive(true);

                        Collider[] page3Colliders = pages[actualPageIndex + 1].page.GetComponents<Collider>();
                        page3Colliders[0].enabled = false;
                        page3Colliders[1].enabled = false;
                        pages[actualPageIndex + 1].canvasLeft.gameObject.SetActive(false);
                        pages[actualPageIndex + 1].canvasRight.gameObject.SetActive(false);
                        // pages[actualPageIndex + 1].page.SetActive(false);
                        didTurnPage = false;
                        myFirstPageIniTime = 0;
                    }
                }
            }
        }
    }
}
