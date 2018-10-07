using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastColliderDetection : MonoBehaviour {
    public float rayLength;
    public float distanceMousePosDoorUP;
    public float DoorRotSpeed;
    public float rotY;

    public GameObject darkBackground;
    private bool darkBackgroundFlag = false;
    public bool finishFirstDemo;
    public bool finishSecondDemo;
    public bool finishHandHeldDemo;
    public bool canStartSecondPartOfSecondDemo;
    private int batteryClickCounter;
    public ParticleSystem starsPS;

    public GameObject empty;
    public GameObject emptyBatteryPivot;
    public GameObject motorEmpty;
    private float distCameraToMiddle;
    public bool finishedBobinePuzzle;
    private bool flagDragDoor = false;
    public bool isInsideFinCollider = false;
    public bool isInsideColliderBattery = false;
    public bool isInsideColliderMotor = false;
    public bool canReturnOriginalPlace = false;
    [SerializeField]
    public bool flagObjectToMouse = false;
    [SerializeField]
    public bool flagBatteryToMouse = false;
    [SerializeField]
    public bool atendeu = false;
    [SerializeField]
    public bool speaking = false;

    public bool flagMotorToMouse = false;

    private RaycastHit hit;
    private GameObject target;
    Vector3 mousePosition;
    Vector3 worldMousePosition;

    Vector3 bobineIniPos;
    Vector3 batteryIniPos;
    Vector3 motorIniPos;
    public GameObject bobineToDrag;
    public GameObject batteryToDrag;
    public GameObject motorToDrag;
    public GameObject middlePiece;
    public GameObject topDoor;
    public GameObject bottomDoor;
    public GameObject phones;

    public bool scaleBatteryOnFirstClick;

    Vector3 currentBobinePos;

    Vector3 camaraPos;
    Vector3 screenCamaraPosition;
    Vector3 objectPos;
    Vector3 rayDirCM;
    private float rayCastLength;
    private LayerMask layerMask;
    private bool contarColliders = true;
    private bool telefonar;
    private bool firstTimeUpdate = true;
    public BoxCollider[] phonesColliders;
    //private GameObject phones;

    //canvas
    private bool canInteractWithPhone;

    //non Interaction Animation Variables
    private float interactFalseFirstDemoTimer;
    private float interactTrueFirstDemoTimer;
    private float interactFalseSecondDemoTimer;
    private float interactTrueSecondDemoTimer;

    public float lackingInteractionTimerLimit;

    public bool interactFalseFirstDemoBool;
    public bool interactTrueFirstDemoBool;
    public bool interactFalseSecondDemoBool;
    public bool interactTrueSecondDemoBool;

    //public LayerMask;
    // Use this for initialization

    private EletricityController eletricityController;
    private bool canSendFirstTimeTubes;

    private void Awake()
    {
        
        

        
    }
    void Start()
    {
        finishFirstDemo = false;
        finishSecondDemo = false;
        layerMask = 9;
        contarColliders = true;
        eletricityController = GetComponent<EletricityController>();
        telefonar = GetComponent<EletricityController>().telefonou;
        canInteractWithPhone = GetComponent<EletricityController>().canInteractWithPhone;
        batteryIniPos = batteryToDrag.transform.position;
        bobineIniPos = bobineToDrag.transform.position;
        motorIniPos = motorToDrag.transform.position;
        canStartSecondPartOfSecondDemo = false;
        batteryClickCounter = 0;
        interactFalseFirstDemoTimer = 0;
        interactTrueFirstDemoTimer = 0;
        interactFalseSecondDemoTimer = 0;
        interactTrueSecondDemoTimer = 0;
        canSendFirstTimeTubes = true;
        interactFalseFirstDemoBool = false;
        interactTrueFirstDemoBool = false;
        interactFalseSecondDemoBool = false;
        interactTrueSecondDemoBool = false;

        //bobineIniPos = 
        //rayCastLength = 0;
        //rayLength = 0;
        //distanceMousePosDoorUP = 0;
        //DoorRotSpeed = 0;
        //meter os colliders dos auscutadores a false inicialmente, e só ficam activos quando o primeiro demo está completado e foi clicado no telefonar
        //phones = GameObject.FindGameObjectWithTag("Phones");
        //phones = GameObject.FindGameObjectWithTag("Phones");
        //Debug.Log(phones.name);





    }
	// Update is called once per frame
	void Update () {
        Debug.Log("canReturnOriginalPlace é: " + canReturnOriginalPlace);
        //só depois do canvas comecar é que se pode interagir com o resto
        // Debug.Log("canInteractWithPhone e!!!!!!!!!!!!!!!!!!!!!!!" + canInteractWithPhone);

        ////////////////////////////////////////////////////////////////////////////////////////INI Controlar os timers de feedback se não tiver a interagir durante algum tempo//////////////////////////////////
        if (!finishFirstDemo && !interactFalseFirstDemoBool)
        {
            interactFalseFirstDemoTimer += Time.deltaTime;
        }

        if (!finishSecondDemo && !interactFalseSecondDemoBool)
        {
            interactFalseSecondDemoTimer += Time.deltaTime;
        }

        if (finishFirstDemo && !interactTrueFirstDemoBool)
        {
            interactTrueFirstDemoTimer += Time.deltaTime;
        }
        if (finishSecondDemo && !interactTrueSecondDemoBool)
        {
            interactTrueSecondDemoTimer += Time.deltaTime;
        }

        /*
         *  public GameObject bobineToDrag;
    public GameObject batteryToDrag;
    public GameObject topDoor;
    public GameObject bottomDoor;
         */

        //tubos e bateria são trigger
        if (interactFalseFirstDemoBool) //adicionar bool para entrar só uma vez
        {
            topDoor.GetComponent<Animator>().SetBool("canGlowScale", false);
            bottomDoor.GetComponent<Animator>().SetBool("canGlowScale", false);
            

        }
        if (!interactFalseFirstDemoBool && interactFalseFirstDemoTimer >= lackingInteractionTimerLimit) // se não passou o primeiro demo e não clicou em nada faz animação das cenas do primeiro demo
        {
            topDoor.GetComponent<Animator>().SetBool("canGlowScale",true);
            bottomDoor.GetComponent<Animator>().SetBool("canGlowScale", true);
            bobineToDrag.GetComponent<Animator>().SetBool("canGlowScale", true);
            interactFalseFirstDemoTimer = 0;
        }

        if (!interactFalseSecondDemoBool && interactFalseSecondDemoTimer >= lackingInteractionTimerLimit) // se não passou o primeiro demo e não clicou em nada faz animação das cenas do primeiro demo
        {
            batteryToDrag.GetComponent<Animator>().SetTrigger("canGlowScale");
            interactFalseSecondDemoTimer = 0;

        }
        if(!interactTrueFirstDemoBool && finishFirstDemo && interactTrueFirstDemoTimer >= lackingInteractionTimerLimit)
        {
            Debug.Log("Entrei aqui naquele IF que queres saber");
            bobineToDrag.GetComponent<Animator>().SetBool("canGlowScale", false);
            phones.GetComponent<Animator>().SetTrigger("canGlowScale");
            interactTrueFirstDemoTimer = 0;
        }
        ////////////////////////////////////////////////////////////////////////////////////////FIN Controlar os timers de feedback se não tiver a interagir durante algum tempo//////////////////////////////////
        if (!canInteractWithPhone) //sóa ctualiza a variavel até n ser precisa novamente
        {
            canInteractWithPhone = GetComponent<EletricityController>().canInteractWithPhone;
            if (!darkBackgroundFlag)
            {
                darkBackground.GetComponent<Animator>().SetBool("putDark",true);
                darkBackgroundFlag = true;
            }
        }
        
        if (canInteractWithPhone)
        {
            if (darkBackgroundFlag)
            {
                darkBackground.GetComponent<Animator>().SetBool("putDark", false);
                darkBackgroundFlag = false;
            }
        
            //só acontece primeira vez
            if (firstTimeUpdate)
            {
               // Debug.Log("entire aqui nos colliders");
                phonesColliders = GameObject.FindGameObjectWithTag("Phones").GetComponents<BoxCollider>();
                for (int i = 0; i < phonesColliders.Length; i++)
                {

                    phonesColliders[i].enabled = false;
                    //Debug.Log(phonesColliders[i].enabled);
                }
                firstTimeUpdate = false;
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                /*
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceMousePosDoorUP);
                Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                Debug.Log("Mouse Pos In World: " + worldMousePosition);
                */
               // int numeroDeColliders = 0;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray,out hit, rayLength,layerMask)){
                    rayCastLength = hit.distance;
                    Debug.DrawRay(ray.origin, ray.direction, Color.red, 100, true);
               
                
                    if (hit.collider.gameObject.tag == "TopDoor")
                    {
                        interactFalseFirstDemoBool = true;
                        //interactTrueFirstDemoBool = true;
                        //Debug.Log("distancia percorrida pelo raio: "+ rayCastLength);
                        target = hit.collider.gameObject;
                        flagDragDoor = true;
                       // Debug.Log("nomeDoGameObject: " + target.name);
                    }

                    else if (hit.collider.gameObject.tag == "BottomDoor")
                    {
                        interactFalseFirstDemoBool = true;
                        //interactTrueFirstDemoBool = true;
                        //Debug.Log("distancia percorrida pelo raio: "+ rayCastLength);
                        target = hit.collider.gameObject;
                        flagDragDoor = true;
                        //Debug.Log("nomeDoGameObject: " + target.name);
                    }

                    else if (!finishHandHeldDemo && hit.collider.gameObject.tag == "Bobine" /*|| !finishFirstDemo*/)
                    {

                        //Debug.Log("distancia percorrida pelo raio: "+ rayCastLength);
                        target = hit.collider.gameObject;
                        target.transform.parent = null;

                        //objectIniPos = bobineToDrag.transform.position;
                        flagObjectToMouse = true;
                        //Debug.Log("nomeDoGameObject: " + target.name);
                    }

                    else if (!finishFirstDemo && hit.collider.gameObject.tag == "Motor" /*|| !finishFirstDemo*/)
                    {

                        //Debug.Log("distancia percorrida pelo raio: "+ rayCastLength);
                        target = hit.collider.gameObject;
                        target.transform.parent = null;

                        //objectIniPos = bobineToDrag.transform.position;
                        flagMotorToMouse = true;
                        //Debug.Log("nomeDoGameObject: " + target.name);
                    }

                    else if (!finishSecondDemo && hit.collider.gameObject.tag == "Battery" /*|| !finishFirstDemo*/)
                    {
                      
                        //Debug.Log("distancia percorrida pelo raio: "+ rayCastLength);
                        target = hit.collider.gameObject;
                        target.transform.parent = null;
                        scaleBatteryOnFirstClick = true;
                        Debug.Log("CLIQUEI NA BATERIA");
                        interactFalseSecondDemoBool = true;
                        if (canStartSecondPartOfSecondDemo)//BUG DE CLICAR RAPIDO DE MAIS
                        {
                            Debug.Log("FIZ POWERUP LOL"); 
                            batteryClickCounter++;
                            target.GetComponent<Animator>().SetTrigger("canPowerUp");
                            target.GetComponent<Animator>().SetTrigger("IdleAnimation");
                        }
                        else
                        {
                            Debug.Log("ENTREIA AQUI NO flagBATTERYtoMOUSE");
                            flagBatteryToMouse = true; //se n tiver entrado ainda na segunda parte do desafio, já n deixa ir para a mão
                        }


                        //objectIniPos = batteryToDrag.transform.position;
                       
                        Debug.Log("nomeDoGameObject: " + target.tag);
                    }

                    else if(hit.collider.gameObject.tag == "Phones")
                    {
                        telefonar = GetComponent<EletricityController>().telefonou;
                        interactTrueFirstDemoBool = true;
                        //Collider[] colliders = hit.collider.gameObject.GetComponents<Collider>();
                        //Debug.Log("entrei no contarColliders");
                        
                        if (telefonar && phonesColliders[0].enabled)
                        {
                            phonesColliders[0].enabled = false;
                            phonesColliders[1].enabled = false;
                            phonesColliders[2].enabled = true;
                            phonesColliders[3].enabled = true;
                        atendeu = true;
                        }
                        else if (phonesColliders[2].enabled)
                        {
                            
                            phonesColliders[0].enabled = true;
                            phonesColliders[1].enabled = true;
                            phonesColliders[2].enabled = false;
                            phonesColliders[3].enabled = false;
                        atendeu = false;
                        }
                        

                    }
                    else if(hit.collider.gameObject.tag == "VibratePlate")
                    {
                        
                        Animator anim = hit.collider.gameObject.GetComponent<Animator>();
                        if (anim.GetBool("canMoveOut")){
                            anim.SetBool("canMoveOut", false);
                        }
                        else
                        {
                            eletricityController.vibratePlateBool = true;
                            anim.SetBool("canMoveOut", true);
                        }
                        
                    }
                    else if (hit.collider.gameObject.tag == "Handheld")
                    {
                        
                        Animator anim = hit.collider.gameObject.GetComponent<Animator>();
                        if (anim.GetBool("canRotate"))
                        {
                            anim.SetBool("canRotate", false);
                        }
                        else
                        {
                            
                            eletricityController.handHeldBool = true;
                            anim.SetBool("canRotate", true);
                        }

                    }

                }

            
            
            }
            if (batteryClickCounter == 5)
            {
                canStartSecondPartOfSecondDemo = false;
                finishSecondDemo = true;
                batteryClickCounter = 0;
                Instantiate(starsPS);
            }

            if (flagDragDoor )
            {
                OnMouseDrag();
           
            }

            if (flagObjectToMouse)
            {
               objectToMouse(empty);

            }

            if (flagBatteryToMouse)
            {
                objectToMouse(emptyBatteryPivot);

            }

            if (flagMotorToMouse)
            {
                objectToMouse(motorEmpty);

            }

            if (Input.GetMouseButtonUp(0))
            {
                if (finishFirstDemo)
                {
                   // Debug.Log("entrei aqui na flag de terminar a bobine");
                }
                //verifica se o objecto actual é a bobine e se o demo nãoa cabou e se não esta dentro do collider de chegada da bobine
                if (target != null && target.gameObject.tag == "Bobine"  && (!finishHandHeldDemo && isInsideFinCollider == false))
                {
                    Debug.Log("bobine posso retornar a posição inicial");
                    canReturnOriginalPlace = true;
                    //flagObjectToMouse = true;
                    //returnObjectToPlace();
                }
                if (target != null && target.gameObject.tag == "Battery" && (!finishSecondDemo && isInsideColliderBattery == false))
                {
                    Debug.Log("ENTREI AQUI NA BATERIA ");
                    canReturnOriginalPlace = true;
                    //flagObjectToMouse = true;
                    //returnObjectToPlace();
                }
                if (target != null && target.gameObject.tag == "Motor"   && (!finishFirstDemo && isInsideColliderMotor == false))
                {
                    Debug.Log("motor pode retornar a posição inicial");
                    canReturnOriginalPlace = true;
                }

                flagDragDoor = false;
                flagObjectToMouse = false;
                flagBatteryToMouse = false;
                flagMotorToMouse = false;
            }

            if (canReturnOriginalPlace && target.gameObject.tag == "Bobine")
            {
                Debug.Log("vou retornar a posição inicial");
                returnObjectToPlace(bobineIniPos);
            }
            if (canReturnOriginalPlace && target.gameObject.tag == "Battery")
            {
                Debug.Log("vou retornar a posição inicial a batteria");
                returnObjectToPlace(batteryIniPos);
            }
            if (canReturnOriginalPlace && target.gameObject.tag == "Motor")
            {
                Debug.Log("vou retornar a posição inicial a batteria");
                returnObjectToPlace(motorIniPos);
            }


        }

    }
 /// <summary>
 /// /////////////////////////////////////////////////////////////////////DragDoor//////////////////////////////////////////////////////////////////
 /// </summary>
    void OnMouseDrag()
    {
        Debug.Log("distancia percorrida pelo raio: " + rayCastLength);
        //regra 3 simples tendo como base rotação 540
        DoorRotSpeed = (rayCastLength * 200) / 1.62f;
        rotY = Input.GetAxis("Mouse X") * DoorRotSpeed * Mathf.Deg2Rad; //rotY tendo em conta o movimento em X do rato
        Debug.Log("a variavel rotY é: " + rotY);
        

        if(rotY >= 10)
        {
            rotY = 10;
        }
        else if(rotY <= -10)
        {
            rotY = -10;
        }
        //limitar as rotações entre 360 e 250
        //debugManager.debugInst.debugText.text = rotY.ToString();

        target.transform.Rotate(Vector3.back, rotY); //Vector3.back tras um vetor (0.0.-1)

        var rot = target.transform.rotation.eulerAngles;
        /*
        if (rotY > 0 && rot.y < 250) rot.y = 250; //se o movimento do rato for positivo e o rot.y for menor que 250 fica 250
        if (rotY < 0 && (rot.y > 0 && rot.y <250)) rot.y = 0;// se o movimento do rato for menor que 0 e o rot.y maior que 0 e menor que 250 fica 0
        */
        //rightDrag
        if (rotY > 0 && rot.y < 70) rot.y = 70; //se o movimento do rato for positivo e o rot.y for menor que 250 fica 250
       // if (rotY < 0 && (rot.y > 180 && rot.y < 70)) rot.y = 180;// se o movimento do rato for menor que 0 e o rot.y maior que 0 e menor que 250 fica 0

        //leftDrag
        if (rotY < 0 && rot.y > 180) rot.y = 180; //se o movimento do rato for positivo e o rot.y for menor que 250 fica 250
        //if (rotY < 0 && (rot.y > 180 && rot.y < 70)) rot.y = 180;// se o movimento do rato for menor que 0 e o rot.y maior que 0 e menor que 250 fica 0


        target.transform.rotation = Quaternion.Euler(rot);


      
    }

    /// <summary>
    /// /////////////////////////////////////////////////////////////////////ObjectMousePos//////////////////////////////////////////////////////////////////
    /// </summary>

    void objectToMouse(GameObject empty)
    {
        
        camaraPos = Camera.main.transform.position;
       // screenCamaraPosition = Camera.main.ScreenToWorldPoint(camaraPos);

        objectPos = GameObject.FindWithTag("CenterPiece").transform.position;

        rayDirCM = Vector3.Normalize(objectPos - camaraPos);

       
        
        Ray centerRay = new Ray(camaraPos, rayDirCM);
        if (Physics.Raycast(centerRay, out hit, rayLength))
        {
            Debug.DrawRay(centerRay.origin, centerRay.direction, Color.red, 100, true);
            distCameraToMiddle = hit.distance;
        }





        //DoorRotSpeed = (rayCastLength * 540) / 0.23f;
        

        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,1.0f);
        worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        empty.transform.position = worldMousePosition;
        
        target.transform.parent = empty.transform;
        if (scaleBatteryOnFirstClick && target.transform.tag == "Battery") //colocar o scale correcto dele quando está dentro do pai
        {
            target.GetComponent<Animator>().SetTrigger("clickedBattery");
            scaleBatteryOnFirstClick = false;
        }
        target.gameObject.transform.position = Vector3.MoveTowards(target.gameObject.transform.position, empty.transform.position, Time.deltaTime * 2.0f);
        //empty.transform.position = new Vector3(empty.transform.position.x, empty.transform.position.y, distCameraToMiddle / 100);


        // Debug.Log("worldMousePosition: " + worldMousePosition + "targetPos: " + target.transform.position);
    }
    

    void returnObjectToPlace(Vector3 objectIniPos)
    {
       // Debug.Log("entrei aqui no return Object To Place");
        target.transform.position = Vector3.MoveTowards(target.transform.position, objectIniPos, Time.deltaTime * 2.0f);
        if (target.gameObject.transform.position == objectIniPos)
        {
            //Debug.Log("cheguei a posição inicial");
            canReturnOriginalPlace = false;
            //flagObjectToMouse = false;
            //flagBatteryToMouse = false;
        }
    }

}

