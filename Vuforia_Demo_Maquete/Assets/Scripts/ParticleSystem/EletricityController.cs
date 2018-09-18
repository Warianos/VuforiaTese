using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EletricityController : MonoBehaviour {

    public bool filter;
    public ParticleSystem ps;
    public ParticleSystem soundWavesLeft;
    public ParticleSystem soundWavesRight;
    public ParticleSystem soundWavesCamera;
    public ParticleSystem photonParticleLeft;
    public ParticleSystem photonParticleRight;
    public ParticleSystem photonParticleBobine;
    public ParticleSystem photonParticleSendSound;
    public ParticleSystem photonParticleRecieveSound;
    public ParticleSystem sparksFinalPS;

    public GameObject eletricityBobine;
    public GameObject eletricityGoingUp;
    public GameObject eletricityDownOFF;
    public GameObject eletricityDown;
    public GameObject eletricityGoingDown;
    public GameObject eletricityBatteryDown;
    public GameObject eletricityBatteryUP;

    public float eletricityBobineTime;
    public float eletricityDownTime;
    public float timeToAnswerAgain;

    private bool eletricityDownFlag1;
    public bool eletricityBobineON;
    private bool eletricityFlag1;
    private bool eletricityFlag2;
    private bool eletricityFlag3;


    //tempo final
    public float timeBobine;
    public float timeLeftParticle;
    public float timeRightParticle;
    public float timeSendSound;
    public float timeRecieveSound;
    public float timeSoundWavesLeft;
    public float timeSoundWavesRight;
    public float timeSoundWavesCamera;

    //tempo inicial
    public float timeBobineINI;
    public float timeLeftParticleINI;
    public float timeRightParticleINI;
    public float timeSendSoundINI;
    public float timeRecieveSoundINI;
    public float timeSoundWavesLeftINI;
    public float timeSoundWavesRightINI;
    public float timeSoundWavesCameraINI;

    //bolleanos unicos para parar o isntantiate
    private bool activateCancelBobineTelephoneOnce;
    private bool activateCancelBobineOnce;
    private bool activateCancelLeftParticleOnce;
    private bool activateCancelRightParticleOnce;
    private bool activateCancelSendSoundOnce;
    private bool activateCancelRecieveSoundOnce;
    private bool activateCancelSoundWavesLeft;
    private bool activateCancelSoundWavesRight;
    private bool activateCancelSoundWavesCamera;

    //canvas Tools
    public Button Atender;
    public Button Falar;
    public Button Telefonar;
    public Button FiltroVisual;
    public Text atenderText;
    public Text desligarText;
    public Text falarText;
    public Text naoFalarText;
    public GameObject canvasAnimatorController;
    public bool canvasStart;
    private bool infoFirstTime;
    public bool canInteractWithPhone;

    //canvas Objetives
    public Text primeiroDesafioText;
    public Text segundoDesafioText;
    public Text terceiroDesafioText;

    //canvas infoPanel
    //public GameObject infoPanel;

    //canvas timers
    public float canvasTimerToAppear;
    //Objetos para animar

    public GameObject Sinos;
    public GameObject PlacaPreta;
    public GameObject Tubos;
    

    //Animators

    private Animator SinosAnimator;
    private Animator PlacaPretaAnimator;
    private Animator TubosAnimator;


    public bool call;
    private float time;
    private bool canRing;

    private bool finishFirstDemo;
    public bool finishSecondDemo;
    private bool finishHandHeldDemo;
    private bool canSendSoundTubes;
    public bool atendeu;
    private bool atendiUmaVez;
    private bool falou;
    private bool atendeuPrimeiro;
    public bool possoAtender;
    private bool discoveredAll;
    private int countObjectivesDone;

    //informação que é desbloquada
    public GameObject infoPanelToEnable;
    public Text Info1;
    public Text Info2;
    public Text Info3;
    public Text Info;

    //escrita presente no telefone
    private string bobineDoneStr;
    private string motorDoneStr;
    private string batteryDoneBeforeMotorStr;
    private string bateryDoneStr;
    private string atendeuDoneStr;
    private string batteryLowStr;
    private string finishedGameStr;
    private string startingToHaveEnergyStr;
    private string notEaringAnythingStr;
    private string vibratePlateStr;
    private string handHeldStr;

    private bool showInfo1;
    private bool showInfo2;
    private bool showInfo3;
    private bool showInfo4;
    private bool showInfoMotor;
    public bool canShowInfo5;
    public bool canShowInfo2;
    private bool canshowInfo2Flag = true;
    public bool showInfoTelephone;
    private bool canShowInfoVibratePlate;
    private bool showInfoBatterDidnotFinishFristDemo;
    public bool showInfoAcceptCallnot2Challenge;
    public bool canShowHandheldInfo;
    public bool handHeldBool;
    public bool vibratePlateBool;

    public bool firstTimeObjectiveShakeAnim;

    XMLManager xmlManager;
    private bool xmlManagerSticker;

    public AudioClip notificationSound;
    private bool canNotificateFirstTime = true;
    private bool canRingSound = true;
    //public AudioSource eletrcityTop;
    //public AudioSource eletricityMiddle;
    // public AudioSource eletricityBottom;
    public AudioClip telephoneRinging;
    public AudioSource audio;
    public GameObject AudioSourceObject;
    

    public bool telefonou;
    public GameObject cameraAR;
    private ParticleSystem targetChild;
    private Collider[] phonesColliders;
    //Use this for initialization
    void Start () {
        audio = AudioSourceObject.GetComponent<AudioSource>();
        time = ps.GetComponent<ParticleSystemFollowPath>().time;

        finishFirstDemo = GetComponent<RaycastColliderDetection>().finishFirstDemo;
        finishSecondDemo = GetComponent<RaycastColliderDetection>().finishSecondDemo;
        canSendSoundTubes = true;
        atendeu = GetComponent<RaycastColliderDetection>().atendeu;
        phonesColliders = GetComponent<RaycastColliderDetection>().phonesColliders;
        SinosAnimator = Sinos.GetComponent<Animator>();
        PlacaPretaAnimator = PlacaPreta.GetComponent<Animator>();
        TubosAnimator = Tubos.GetComponent<Animator>();
        canvasStart = false;
        showInfo1 = true;
        showInfo2 = true;
        showInfo3 = true;
        showInfo4 = true;
        showInfoMotor = true;
        canShowInfo2 = false;
        discoveredAll = false;
        showInfoTelephone = true;
        canShowInfoVibratePlate = true;
        showInfoBatterDidnotFinishFristDemo = true;
        showInfoAcceptCallnot2Challenge = true;
        canInteractWithPhone = false;
        infoFirstTime = true;
        firstTimeObjectiveShakeAnim = false;

        eletricityDownFlag1 = false;

        telefonou = false;
        eletricityBobineON = false;
        eletricityBobineTime = 0;
        eletricityDownTime = 0;
        timeToAnswerAgain = 0;
        countObjectivesDone = 0;
        eletricityFlag1 = true;
        eletricityFlag2 = true;
        eletricityFlag3 = true;
        falarText.enabled = true;
        naoFalarText.enabled = false;

        activateCancelBobineTelephoneOnce = true;
        activateCancelBobineOnce = true;
        activateCancelLeftParticleOnce = true;
        activateCancelRightParticleOnce = true;
        activateCancelSendSoundOnce = true;
        activateCancelRecieveSoundOnce = true;
        activateCancelSoundWavesLeft = true;
        activateCancelSoundWavesRight = true;

        timeBobineINI = 0;
        timeLeftParticleINI = 0;
        timeRightParticleINI = 0;
        timeSendSoundINI = 0;
        timeRecieveSoundINI = 0;
        timeSoundWavesLeftINI = 0;
        timeSoundWavesRightINI = 0;

        
        //INICIAR O CANVAS ANTES DE TER LIGADO
        //canvasAnimatorController.
        //FINAL DE CANVAS TER LIGADO

        //GetComponent<RaycastColliderDetection>().finishFirstDemo
        //Invoke("instantiatePSLeft", 0);
        //Invoke("instantiatePSRight", 0);




        // xmlManager = GameObject.FindGameObjectWithTag("XMLManager");
        //xmlManager = new XMLManager();
        XMLManager.ins.LoadItems();
        //xmlManagerSticker = xmlManager.GetComponent<XMLManager>().itemDB.list[0].earnedSticker; //primeiro elemento é o telefone e tras o booleano actual feito do load do XML
        //xmlManagerSticker = XMLManager.ins.itemDB.list[0].earnedSticker;
        bobineDoneStr = XMLManager.ins.itemDB.list[0].objectInfoText1;
        bateryDoneStr = XMLManager.ins.itemDB.list[0].objectInfoText2;
        atendeuDoneStr = "Já estamos a conseguir ouvir a chamada, aumenta o som do telemovel e exprimenta aproximar-te dos auscutadores digitais";
        motorDoneStr = "Este motor ao receber enegia eletrica fazia com que as pessoas soubessem que lhes tavam a querer telefonar. Exprimenta telefonar agora para o Gower Bell ";
        batteryDoneBeforeMotorStr = "Já temos a bateria a funcionar, mas parece que o telefone ainda não está a tocar. Tenta investigar";
        batteryLowStr = "oh não, a bateria está sem energia, sera que podes utilizar a tua energia para carrega-la ? ";
        finishedGameStr = "Parabêns! conseguiste arranjar o telefone! Agora se quiseres podes voltar atras ao livro e ver os pedaços de história que descobriste, ou então podes continuar a interagir com o telefone";
        startingToHaveEnergyStr = "Já estamos a receber energia, em principio poderemos ouvir a chamada";
        notEaringAnythingStr = "Estamos a receber a chamada, mas não estamos a conseguir ouvir nada...será que poderá ser falta da bateria?";
        vibratePlateStr = XMLManager.ins.itemDB.list[0].objectInfoText3;
        Info1.text = bobineDoneStr;
        Info2.text = bateryDoneStr;
        Info3.text = vibratePlateStr;

        //debugManager.debugInst.debugText.text = "Nome do primeiro objecto: " + XMLManager.ins.itemDB.list[0].objectInfoText1 + "\n";
        //debugManager.debugInst.debugText.text += "Nome do primeiro objecto: " + XMLManager.ins.itemDB.list[0].objectInfoText2 + "\n";
        //debugManager.debugInst.debugText.text += "Nome do primeiro objecto: " + XMLManager.ins.itemDB.list[0].objectInfoText3 + "\n";

    }


  

    void instantiatePSLeft()
    {
        Instantiate(photonParticleLeft);
    }
    void instantiatePSRight()
    {
        Instantiate(photonParticleRight);
    }
    void instantiatePSBobine()
    {
        Instantiate(photonParticleBobine);
    }
    void instantiatePSRecieveSound()
    {
        Instantiate(photonParticleRecieveSound);
    }
    void instantiatePSSendSound()
    {
        Instantiate(photonParticleSendSound);
    }
    void instantiatePSSoundRight()
    {
        Instantiate(soundWavesRight);
    }
    void instantiatePSSoundLeft()
    {
        Instantiate(soundWavesLeft);
    }
    void instantiatePSSoundCamera()
    {

        //mudar posição sempre que mudar posição no inspector
        //Instantiate(soundWavesCamera);
        targetChild = Instantiate(soundWavesCamera);
        
        targetChild.transform.parent = cameraAR.transform; //tornar filho da camera mal são inicializados
        targetChild.transform.localPosition = new Vector3(-0.0007857245f, -0.003699996f, 0.0422f);
        targetChild.transform.localRotation = new Quaternion(0,0,0,0);
        //Instantiate(ps);
    }

    void Update()
    {
        Debug.Log("a variavel é: "  + telefonou);
        /*
        if (!IsInvoking("refreshVariables")){
            Invoke("refreshVariables", 0.1f);
        }
          */

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////INI ElectricityController////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////INI BOBINE ESTRAGADA////////
        // if (!finishFirstDemo && !eletricityBobineON)
        //{
        //eletricityBobineON = true;
        //}
        if (!finishFirstDemo)
        {
            if (eletricityBobineON)
            {
                eletricityBobine.SetActive(true);
               // eletrcityTop.Play();
                eletricityBobineTime += Time.deltaTime;
                //Debug.Log(eletricityBobineTime);
            }

            if (eletricityBobineTime >= 3.0f)
            {

                eletricityBobine.GetComponent<Animator>().SetBool("canFadeOut", true);
                //eletricityBobineON = false;



            }
            if (eletricityBobineTime >= 5.0f)
            {
               // eletrcityTop.Stop();
                eletricityBobineTime = 0;
                eletricityBobine.SetActive(false);
                eletricityBobineON = false;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////FIN BOBINE ESTRAGADA////////

        ////////////////////////////////////////////////////////////////////////////////////////////INI BOBINE ARRANJADA////////
        if (finishFirstDemo)
        {
            if (eletricityFlag2)
            {
                eletricityBatteryDown.SetActive(true);
                //eletricityBottom.Play();
                eletricityFlag2 = false;
            }
            if (eletricityBobineTime >= 3.0f) //caso tenha finalizado o primeiro demo sem acabar a animação toda, reinicia aqui as variaveis
            {
                eletricityBobineTime = 0;
                eletricityBobineON = false;
            }
            if (eletricityBobineON)
            {
                //eletricityBobine.SetActive(false);
                if(eletricityFlag1)
                {
                    eletricityBobine.SetActive(false);
                    eletricityBobine.SetActive(true);
                   // eletrcityTop.Play();
                    eletricityFlag1 = false;
                }
                
                //eletricityBobineTime += Time.deltaTime;
               // Debug.Log(eletricityBobineTime);

                if (atendeu)
                {
                    eletricityBobineTime += Time.deltaTime;
                    eletricityBobine.GetComponent<Animator>().SetBool("canFadeOut", true);
                    


                    if (eletricityBobineTime >= 1.5)
                    {
                        eletricityDownFlag1 = true;
                        eletricityBobineTime = 0;
                        eletricityBobineON = false;
                        eletricityBobine.SetActive(false);
                        //eletrcityTop.Stop();
                        eletricityFlag1 = true;

                        eletricityGoingDown.SetActive(true);//enviar
                        //eletricityMiddle.Play();
                        //activar ondas de som a irem pelo cano?
                        //eletricityDown.SetActive(true);
                    }
                }
               

            }

            if (eletricityDownFlag1)
            {
                //eletricityDownTime += Time.deltaTime;
            }
            if (!atendeu)
            {
                Debug.Log("eletricityDownFlag1 é: " + eletricityDownFlag1);
                if (eletricityDownFlag1)
                {
                    Debug.Log("entrei no eletricityDownFlag1 " + eletricityDownFlag1);
                    eletricityGoingDown.GetComponent<Animator>().SetBool("canFadeOut", true);
                    //desligar ondas de som a irem pelo cano?
                    //eletricityDown.SetActive(false);
                    eletricityDownTime += Time.deltaTime;
                    if (eletricityDownTime >= 1.5f)
                    {
                        eletricityDownTime = 0;
                        eletricityDownFlag1 = false;
                        eletricityGoingDown.SetActive(false);
                        //eletricityMiddle.Stop();
                    }
                    //enviar 
                    //eletricityDown.GetComponent<Animator>().SetBool("canFadeOut", true);
                    //eletricityDownTime += Time.deltaTime;
                }


            }


            //public GameObject eletricityBobine;
            //public GameObject eletricityGoingUp;
            //public GameObject eletricityDownOFF;
            //public GameObject eletricityDown;

        }
        
        if (finishSecondDemo && atendeu)
        {
            //Debug.Log("timeSoundWavesLeftINI = " + timeSoundWavesLeftINI);
            //Debug.Log("timeSoundWavesLeft = " + timeSoundWavesLeft);
            //Debug.Log("activateCancelSoundWavesLeft = " + activateCancelSoundWavesLeft);
            //////////////////////////////////////////////////////////////////SOUND TUBES INI//////////////////////////////////////////////////////////////
            if (timeSoundWavesLeftINI <= timeSoundWavesLeft)
            {
                timeSoundWavesLeftINI += Time.deltaTime;
            }

            if (timeSoundWavesLeftINI >= timeSoundWavesLeft)
            {
                activateCancelSoundWavesLeft = true;
            }

            if (activateCancelSoundWavesLeft)
            {

                timeSoundWavesLeftINI = 0;
                activateCancelSoundWavesLeft = false;
                instantiatePSSoundLeft();
                instantiatePSSoundRight();
            }

            canSendSoundTubes = true;
            //////////////////////////////////////////////////////////////////SOUND TUBES FIN//////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////Enviar mensagem de ter feito o segundo desafio




        }
        if (finishSecondDemo && eletricityFlag3)
        {
            eletricityBatteryUP.SetActive(true);
           // eletricityBottom.Play();
            eletricityFlag3 = false;
        }
        /*
        if (finishSecondDemo && !atendeu)
            if (canSendSoundTubes) {
                pssound
        }
        */

        ////////////////////////////////////////////////////////////////////////////////////////////FIN BOBINE ARRANJADA////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////FIN ElectricityController////////////

        refreshVariables();
        ///////////////////////////////////////////////////////INI START CANVAS////////////
        Debug.Log("canInteractWithPhone ELETRICITY CONTROLLER" + canInteractWithPhone);
        if (!finishFirstDemo)
        {
            if (canvasStart)
            {
                canvasTimerToAppear += Time.deltaTime;
            }

            if (telefonou && infoFirstTime)
            {
                
                sendinfo(Info);
                infoFirstTime = false;
            }
            if (telefonou && canvasStart && canvasTimerToAppear >= 0.5f)
            {
                Debug.Log("entreia qui no temporizador");
                canvasAnimatorController.GetComponent<canvasAnimationController>().returnButtonAnimator.SetBool("canShowMenu", true);
                canvasAnimatorController.GetComponent<canvasAnimationController>().objectivePanelAnimator.SetBool("canShowMenu", true);

                
                //canvasStart = false;
            }
            //vibrar a primeira vez apenas
            if (firstTimeObjectiveShakeAnim && canvasTimerToAppear >= 2.0f && canNotificateFirstTime)
            {
                canNotificateFirstTime = false;
                canvasStart = false;
                canInteractWithPhone = true;
                firstTimeObjectiveShakeAnim = false;
                //canvasTimerToAppear = 0 ;
                canvasAnimatorController.GetComponent<canvasAnimationController>().objectivePanelAnimator.SetBool("somethingNew", true);
                audio.PlayOneShot(notificationSound,0.7F);
            }
        }
        
        //////////////////////////////////////////////////////FIN START CANVAS///////////
        if (possoAtender)
        {
                
            Atender.interactable = true;
        }

        

            //se não acabou o primeiro demo
            if (!finishFirstDemo)
        {
            
            //se atendeu muda as letras para desligar SE PUXOU OS AUSCUTADORES
            if (atendeu)
            {
                atenderText.enabled = false;
                desligarText.enabled = true;
                Telefonar.interactable = false;
            }
            //se não atendeu muda as letras para ligar
            else if (!atendeu)
            {
                atenderText.enabled = true;
                desligarText.enabled = false;
                Telefonar.interactable = true;
            }

           
            
            
        }

        ////////////////////////////////////////////////////////////////verificação dos acontecimentos para aparecer informação//////////////////////////////////////////////////
        Debug.Log("O FINISHHADNHELDDEMO É: " + finishHandHeldDemo);
        if (finishHandHeldDemo && showInfo1 )
        {
            Debug.Log("ENTREI AQUI NO FINISHHADNHELDDEMO");
            showInfo1 = false;
         
            primeiroDesafioText.transform.Find("CheckBox").gameObject.SetActive(false);
            primeiroDesafioText.transform.Find("CheckBoxCorrect").gameObject.SetActive(true);
            countObjectivesDone++;
            segundoDesafioText.color = new Color(0.1f, 0.1f, 0.1f,1.0f); //meter cor cinzenta escura
            sendinfo(Info1);
            canvasAnimatorController.GetComponent<canvasAnimationController>().objectivePanelAnimator.SetBool("somethingNew", true);
            audio.PlayOneShot(notificationSound, 0.7F);
            
            XMLManager.ins.SetVariableInDatabase("Telephone", true, 0);
            XMLManager.ins.SaveItems();

        }
        if(finishFirstDemo && showInfoMotor && !telefonou)
        {
            showInfoMotor = false;
            Info.text = motorDoneStr;
            sendinfo(Info);
        }

        if (finishSecondDemo && showInfo4)
        {

            sendinfo(Info2);
            telefonou = false;
            countObjectivesDone++;
            XMLManager.ins.SetVariableInDatabase("Telephone", true, 1);
            XMLManager.ins.SaveItems();
            showInfo4 = false;

        }

        Debug.Log("finishFirstDemo: " + finishFirstDemo + " finishSecondDemo: " + finishSecondDemo + " showInfoBatterDidnotFinishFristDemo: " + showInfoBatterDidnotFinishFristDemo + " telefonou: " + telefonou );
       // Debug.Log("ENTREEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEI " + infoPanelToEnable.GetComponent<Animator>().GetBool("clickedOrTimeOut"));
        if (!finishFirstDemo && finishSecondDemo && showInfoBatterDidnotFinishFristDemo && telefonou)
        {
            
          //  if (infoPanelToEnable.GetComponent<Animator>().GetBool("clickedOrTimeOut") == true){
                Debug.Log("ENTREEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEI");
                showInfoBatterDidnotFinishFristDemo = false;
                Info.text = batteryDoneBeforeMotorStr;
                sendinfo(Info);
            //}
            
        }
        if (canShowInfo2 && canshowInfo2Flag)
        {
            canShowInfo2 = false;
            canshowInfo2Flag = false;
            Info.text = batteryLowStr;
            sendinfo(Info);
            

        }
        if (finishFirstDemo && showInfo2 && telefonou)
        {
            segundoDesafioText.transform.Find("CheckBox").gameObject.SetActive(false);
            segundoDesafioText.transform.Find("CheckBoxCorrect").gameObject.SetActive(true);
            terceiroDesafioText.color = new Color(0.1f, 0.1f, 0.1f, 1.0f); //meter cor cinzenta escura
            canvasAnimatorController.GetComponent<canvasAnimationController>().objectivePanelAnimator.SetBool("somethingNew", true);
            audio.PlayOneShot(notificationSound, 0.7F);
            
            showInfo2 = false;
            
        }

        if(canShowInfoVibratePlate && vibratePlateBool)
        {
            canShowInfoVibratePlate = false;
            sendinfo(Info3);
            XMLManager.ins.SetVariableInDatabase("Telephone", true, 2);
            XMLManager.ins.SetVariableInDatabase("Telephone", true);
            XMLManager.ins.SaveItems();
        }

       
        if (finishSecondDemo && showInfo3 && atendeu)
        {
            showInfo3 = false;
            //atendeu = false;
            terceiroDesafioText.transform.Find("CheckBox").gameObject.SetActive(false);
            terceiroDesafioText.transform.Find("CheckBoxCorrect").gameObject.SetActive(true);
            terceiroDesafioText.color = new Color(0.1f, 0.1f, 0.1f, 1.0f); //meter cor cinzenta escura
            Info.text = atendeuDoneStr;
            sendinfo(Info);
            countObjectivesDone++;
            
        }

        if (countObjectivesDone == 3 && infoPanelToEnable.GetComponent<Animator>().GetBool("clickedOrTimeOut") == true)
        {
            Instantiate(sparksFinalPS);
            countObjectivesDone = 0;
            Info.text = finishedGameStr;
            sendinfo(Info);
            
        }
        ////////////////////////////////////////////////////////////////FIM verificar se acabou o demo a primeira vez para desbloquear a primeira informação//////////////////////////////////////////////////


        if (finishFirstDemo)
        {
            //se acabou o primeiro demo e telefona, tem de ficar a espera que atendaõ
            Debug.Log("telefonou bool: " + telefonou);
            Debug.Log("atender bool: " + atendeu);
            if (telefonou)
            {
                if (!showInfoAcceptCallnot2Challenge && !finishSecondDemo)
                {
                    showInfoAcceptCallnot2Challenge = true;
                }
                if (showInfoTelephone)//mostrar informação sobre ter telefonado depois de ter feito o primeiro desafio
                {
                    Info.text = startingToHaveEnergyStr;
                    sendinfo(Info);
                    showInfoTelephone = false;
                }
                //por os 2 primeiros colliders do telefone ligados
                if(phonesColliders[0].enabled == false) //COLOCAR ISTO A APSSAR APENAS UMA VEZ
                {
                    phonesColliders[0].enabled = true;
                    phonesColliders[1].enabled = true;
                }
               
                Telefonar.interactable = false;
                Atender.interactable = true;
                SinosAnimator.SetBool("canRing", true);
                if (canRingSound)
                {
                    canRingSound = false;
                    audio.clip = telephoneRinging;  
                    audio.Play();
                }
                
                
                //primeiroDesafioText.color = new Color(0.302f, 0.671f, 0.318f);
            }
            if (!telefonou && !atendeu)
            {
                Atender.interactable = false;
                
            }

            
            //se atendeu muda as letras para desligar
            if (atendeu)
            {
                if (showInfoAcceptCallnot2Challenge && !finishSecondDemo)//mostrar informação sobre ter telefonado depois de ter feito o primeiro desafio
                {
                    
                    Info.text = notEaringAnythingStr;
                    sendinfo(Info);
                    showInfoAcceptCallnot2Challenge = false;
                }
                atenderText.enabled = false;
                desligarText.enabled = true;
                telefonou = false;
                Falar.interactable = true;
                SinosAnimator.SetBool("canRing", false);
                canRingSound = true;
                audio.Stop();
                
                TubosAnimator.SetBool("canAnimTubes", true);
                TubosAnimator.SetBool("canReturnStartPos", false);
                segundoDesafioText.transform.Find("CheckBox").gameObject.SetActive(false);
                terceiroDesafioText.color = new Color(0.1f, 0.1f, 0.1f, 1.0f); //meter cor cinzenta escura
                
                //Telefonar.interactable = false;
            }
            //se não atendeu muda as letras para ligar
            else if (!atendeu)
            {
                atenderText.enabled = true;
                desligarText.enabled = false;
                Telefonar.interactable = true;
                if (falou)
                {
                    GetComponent<RaycastColliderDetection>().speaking = false;
                }
                 
                
                Falar.interactable = false;
                TubosAnimator.SetBool("canAnimTubes", false);
                TubosAnimator.SetBool("canReturnStartPos", true);
                //Atender.interactable = false;
            }
            
            Debug.Log("Falar bool : " +  falou);
            if (falou)
            {
                PlacaPretaAnimator.SetBool("canVibrate", true);
                falarText.enabled = false;
                naoFalarText.enabled = true;
            }
            else if (!falou)
            {
                PlacaPretaAnimator.SetBool("canVibrate", false);
                falarText.enabled = true;
                naoFalarText.enabled = false;
            }

        }

       

      

                 /*   
                    if (falou)
                    {
                        //////////////////////////////////////////////////////////////////SEND SOUND INI//////////////////////////////////////////////////////////////
                        //if(falei pa tabua)!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        if (timeSendSoundINI <= timeSendSound)
                        {
                            timeSendSoundINI += Time.deltaTime;
                        }
                        if (timeSendSoundINI >= timeSendSound)
                        {
                            activateCancelSendSoundOnce = true;
                        }
                        if (activateCancelSendSoundOnce)
                        {
                            Debug.Log("estou a enviar som para o utilizador");
                            timeSendSoundINI = 0;
                            activateCancelSendSoundOnce = false;
                            //telefonou = false;
                            instantiatePSSendSound();
                        }
                        //enviar som para a placa vibratoria
                        if (timeSoundWavesCameraINI <= timeSoundWavesCamera)
                        {
                            timeSoundWavesCameraINI += Time.deltaTime;
                        }
                        if (timeSoundWavesCameraINI >= timeSoundWavesCamera)
                        {
                            activateCancelSoundWavesCamera = true;
                        }
                        if (activateCancelSoundWavesCamera)
                        {
                            Debug.Log("estou a enviar para a camera");
                            timeSoundWavesCameraINI = 0;
                            activateCancelSoundWavesCamera = false;
                            //telefonou = false;
                            instantiatePSSoundCamera();
                        }

                        //falou = false;
                        //////////////////////////////////////////////////////////////////SEND SOUND FIN//////////////////////////////////////////////////////////////
                    }

                */

         


        // refresh variaveis do filtro
        /*
        else
        {
            activateCancelBobineTelephoneOnce = true;
            activateCancelBobineOnce = true;
            activateCancelLeftParticleOnce = true;
            activateCancelRightParticleOnce = true;
            activateCancelSendSoundOnce = true;
            activateCancelRecieveSoundOnce = true;
            activateCancelSoundWavesLeft = true;
            activateCancelSoundWavesRight = true;
            timeBobineINI = 0;
            timeLeftParticleINI = 0;
            timeRightParticleINI = 0;
            timeSendSoundINI = 0;
            timeRecieveSoundINI = 0;
            timeSoundWavesLeftINI = 0;
            timeSoundWavesRightINI = 0;
        }
        
        */
    }

    private void refreshVariables()
    {
        finishFirstDemo = GetComponent<RaycastColliderDetection>().finishFirstDemo;
        finishSecondDemo = GetComponent<RaycastColliderDetection>().finishSecondDemo;
        finishHandHeldDemo = GetComponent<RaycastColliderDetection>().finishHandHeldDemo;
        atendeu = GetComponent<RaycastColliderDetection>().atendeu;
        falou = GetComponent<RaycastColliderDetection>().speaking;
        phonesColliders = GetComponent<RaycastColliderDetection>().phonesColliders;
    }

    private void sendinfo(Text info)
    {
        Debug.Log("entreia qui no sendINFO");
        /////////////////////////////////////////////Desativar tudo o que n diz respeito a info que se quer mostrar
        if (info.gameObject.name == "Q1")
        {
            Info2.gameObject.SetActive(false);
            Info3.gameObject.SetActive(false);
            Info.gameObject.SetActive(false);
        }
        else if(info.gameObject.name == "Q2")
        {
            Info1.gameObject.SetActive(false);
            Info3.gameObject.SetActive(false);
            Info.gameObject.SetActive(false);
        }
        else if (info.gameObject.name == "Q3")
        {
            Info1.gameObject.SetActive(false);
            Info2.gameObject.SetActive(false);
            Info.gameObject.SetActive(false);
        }
        else if (info.gameObject.name == "Info")
        {
            Info1.gameObject.SetActive(false);
            Info2.gameObject.SetActive(false);
            Info3.gameObject.SetActive(false);
        }
        

        infoPanelToEnable.SetActive(false);
        /////////////////////////////////////////////Desativar tudo o que n diz respeito a info que se quer mostrar

        infoPanelToEnable.SetActive(true);
        info.gameObject.SetActive(true);
    }
}
