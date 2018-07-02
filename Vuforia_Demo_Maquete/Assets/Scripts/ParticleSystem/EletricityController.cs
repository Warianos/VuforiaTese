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

    //canvas Objetives
    public Text primeiroDesafioText;
    public Text segundoDesafioText;
    public Text terceiroDesafioText;

    //canvas infoPanel
    public GameObject infoPanel;
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
    private bool finishSecondDemo;
    public bool atendeu;
    private bool atendiUmaVez;
    private bool falou;
    private bool atendeuPrimeiro;
    public bool possoAtender;


    public bool telefonou;
    public GameObject cameraAR;
    private ParticleSystem targetChild;
    private Collider[] phonesColliders;
    // Use this for initialization
    void Start () {
        time = ps.GetComponent<ParticleSystemFollowPath>().time;
        finishFirstDemo = GetComponent<RaycastColliderDetection>().finishFirstDemo;
        finishSecondDemo = GetComponent<RaycastColliderDetection>().finishSecondDemo;
        atendeu = GetComponent<RaycastColliderDetection>().atendeu;
        phonesColliders = GetComponent<RaycastColliderDetection>().phonesColliders;
        SinosAnimator = Sinos.GetComponent<Animator>();
        PlacaPretaAnimator = PlacaPreta.GetComponent<Animator>();
        TubosAnimator = Tubos.GetComponent<Animator>();

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
        //GetComponent<RaycastColliderDetection>().finishFirstDemo
        //Invoke("instantiatePSLeft", 0);
        //Invoke("instantiatePSRight", 0);
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
        /*
        if (!IsInvoking("refreshVariables")){
            Invoke("refreshVariables", 0.1f);
        }
          */  
        refreshVariables();
        
        
        
        if (possoAtender)
        {
                
            Atender.interactable = true;
        }

        

            //se não acabou o primeiro demo
            if (!finishFirstDemo)
        {
            
            //se atendeu muda as letras para desligar
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
        
        
       
        if (finishFirstDemo)
        {
            //se acabou o primeiro demo e telefona, tem de ficar a espera que atendaõ
            Debug.Log("telefonou bool: " + telefonou);
            Debug.Log("atender bool: " + atendeu);
            if (telefonou)
            {
                //por os 2 primeiros colliders do telefone ligados
                if(phonesColliders[0].enabled == false)
                {
                    phonesColliders[0].enabled = true;
                    phonesColliders[1].enabled = true;
                }
               
                Telefonar.interactable = false;
                Atender.interactable = true;
                SinosAnimator.SetBool("canRing", true);
                primeiroDesafioText.color = new Color(0.302f, 0.671f, 0.318f);
            }
            if (!telefonou && !atendeu)
            {
                Atender.interactable = false;
                
            }
            //se atendeu muda as letras para desligar
            if (atendeu)
            {
                atenderText.enabled = false;
                desligarText.enabled = true;
                telefonou = false;
                Falar.interactable = true;
                SinosAnimator.SetBool("canRing", false);
                TubosAnimator.SetBool("canAnimTubes", true);
                TubosAnimator.SetBool("canReturnStartPos", false);
                segundoDesafioText.color = new Color(0.302f, 0.671f, 0.318f);
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

       

        //FAZER RESET AS VARIAVEIS QUANDO SAI DO FILTER!!!!!
        if (filter)
        {
            terceiroDesafioText.color = new Color(0.302f, 0.671f, 0.318f); //greenish
            //////////////////////////////////////////////////////////////////BOBINE ESTRAGADA INI//////////////////////////////////////////////////////////////
            if (!finishFirstDemo && !photonParticleBobine.IsAlive() && telefonou && activateCancelBobineTelephoneOnce)
            {
                
                telefonou = false;
                instantiatePSBobine();
            }
            if (finishFirstDemo && telefonou == false)
            {
                activateCancelBobineTelephoneOnce = false;
            }
            //////////////////////////////////////////////////////////////////BOBINE ESTRAGADA FIN//////////////////////////////////////////////////////////////

            if (finishFirstDemo)
            {
                //////////////////////////////////////////////////////////////////BOBINE ARRANJADA INI//////////////////////////////////////////////////////////////
                if (timeBobineINI <= timeBobine)
                {
                    timeBobineINI += Time.deltaTime;
                }
                if (timeBobineINI >= timeBobine)
                {
                    activateCancelBobineOnce = true;
                }
                if (finishFirstDemo && telefonou && activateCancelBobineOnce && atendeu == false)
                {
                    Debug.Log("entrei aqui no invoke da bobine repetidamente");
                    timeBobineINI = 0;
                    activateCancelBobineOnce = false;
                    //telefonou = false;
                    instantiatePSBobine();
                   
                }
                //////////////////////////////////////////////////////////////////BOBINE ARRANJADA FIN//////////////////////////////////////////////////////////////
                if (atendeu)// e FALOU MAS AINDA N ESTA FEITO
                {

                    //Falar.interactable = true;

                    //////////////////////////////////////////////////////////////////RECIEVE SOUND INI//////////////////////////////////////////////////////////////
                    if (timeRecieveSoundINI <= timeRecieveSound)
                    {
                        timeRecieveSoundINI += Time.deltaTime;
                    }
                    if (timeRecieveSoundINI >= timeRecieveSound)
                    {
                        activateCancelRecieveSoundOnce = true;
                    }
                    if (activateCancelRecieveSoundOnce)
                    {
                        timeRecieveSoundINI = 0;
                        activateCancelRecieveSoundOnce = false;
                        instantiatePSRecieveSound();
                    }
                
                    //////////////////////////////////////////////////////////////////RECIEVE SOUND FIN//////////////////////////////////////////////////////////////

                    //////////////////////////////////////////////////////////////////ELETRICITY TUBES INI//////////////////////////////////////////////////////////////
                    if (timeLeftParticleINI <= timeLeftParticle)
                    {
                        timeLeftParticleINI += Time.deltaTime;
                    }
                    if (timeLeftParticleINI >= timeLeftParticle)
                    {
                        activateCancelLeftParticleOnce = true;
                    }
                    if (activateCancelLeftParticleOnce)
                    {

                        timeLeftParticleINI = 0;
                        activateCancelLeftParticleOnce = false;
                        instantiatePSLeft();
                        instantiatePSRight();
                    }

                    //////////////////////////////////////////////////////////////////ELETRICITY TUBES FIN//////////////////////////////////////////////////////////////

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

                    //////////////////////////////////////////////////////////////////SOUND TUBES FIN//////////////////////////////////////////////////////////////
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

                }

            }
            

           

        }


        // refresh variaveis do filtro
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
        
        
    }

    private void refreshVariables()
    {
        finishFirstDemo = GetComponent<RaycastColliderDetection>().finishFirstDemo;
        finishSecondDemo = GetComponent<RaycastColliderDetection>().finishSecondDemo;
        atendeu = GetComponent<RaycastColliderDetection>().atendeu;
        falou = GetComponent<RaycastColliderDetection>().speaking;
    }
}
