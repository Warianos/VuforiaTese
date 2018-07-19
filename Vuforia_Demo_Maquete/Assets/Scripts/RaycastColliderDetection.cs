using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastColliderDetection : MonoBehaviour {
    public float rayLength;
    public float distanceMousePosDoorUP;
    public float DoorRotSpeed;

    public bool finishFirstDemo;
    public bool finishSecondDemo;
    public bool canStartSecondPartOfSecondDemo;
    private int batteryClickCounter;
    public ParticleSystem starsPS;

    public GameObject empty;
    public GameObject emptyBatteryPivot;
    private float distCameraToMiddle;
    public bool finishedBobinePuzzle;
    private bool flagDragDoor = false;
    public bool isInsideFinCollider = false;
    public bool isInsideColliderBattery = false;
    public bool canReturnOriginalPlace = false;
    [SerializeField]
    public bool flagObjectToMouse = false;
    [SerializeField]
    public bool flagBatteryToMouse = false;
    [SerializeField]
    public bool atendeu = false;
    [SerializeField]
    public bool speaking = false;

    private RaycastHit hit;
    private GameObject target;
    Vector3 mousePosition;
    Vector3 worldMousePosition;

    Vector3 bobineIniPos;
    Vector3 batteryIniPos;
    public GameObject bobineToDrag;
    public GameObject batteryToDrag;

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
    private GameObject phones;

    //canvas
    private bool canInteractWithPhone;

    //public LayerMask;
    // Use this for initialization

    private void Awake()
    {
        
        

        
    }
    void Start()
    {
        finishFirstDemo = false;
        finishSecondDemo = false;
        layerMask = 9;
        contarColliders = true;
        telefonar = GetComponent<EletricityController>().telefonou;
        canInteractWithPhone = GetComponent<EletricityController>().canInteractWithPhone;
        batteryIniPos = batteryToDrag.transform.position;
        bobineIniPos = bobineToDrag.transform.position;
        canStartSecondPartOfSecondDemo = false;
        batteryClickCounter = 0;
        //bobineIniPos = 
        //rayCastLength = 0;
        //rayLength = 0;
        //distanceMousePosDoorUP = 0;
        //DoorRotSpeed = 0;
        //meter os colliders dos auscutadores a false inicialmente, e só ficam activos quando o primeiro demo está completado e foi clicado no telefonar
        // phones = GameObject.FindGameObjectWithTag("Phones");
        //phones = GameObject.FindGameObjectWithTag("Phones");
        //Debug.Log(phones.name);





    }
	// Update is called once per frame
	void Update () {
        //só depois do canvas comecar é que se pode interagir com o resto
       // Debug.Log("canInteractWithPhone e!!!!!!!!!!!!!!!!!!!!!!!" + canInteractWithPhone);
        if (!canInteractWithPhone) //sóa ctualiza a variavel até n ser precisa novamente
        {
            canInteractWithPhone = GetComponent<EletricityController>().canInteractWithPhone;
        }
        
        if (canInteractWithPhone)
        {

        
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
                    
                        //Debug.Log("distancia percorrida pelo raio: "+ rayCastLength);
                        target = hit.collider.gameObject;
                        flagDragDoor = true;
                       // Debug.Log("nomeDoGameObject: " + target.name);
                    }

                    else if (hit.collider.gameObject.tag == "BottomDoor")
                    {

                        //Debug.Log("distancia percorrida pelo raio: "+ rayCastLength);
                        target = hit.collider.gameObject;
                        flagDragDoor = true;
                        //Debug.Log("nomeDoGameObject: " + target.name);
                    }

                    else if (!finishFirstDemo && hit.collider.gameObject.tag == "Bobine" /*|| !finishFirstDemo*/)
                    {

                        //Debug.Log("distancia percorrida pelo raio: "+ rayCastLength);
                        target = hit.collider.gameObject;
                        target.transform.parent = null;

                        //objectIniPos = bobineToDrag.transform.position;
                        flagObjectToMouse = true;
                        //Debug.Log("nomeDoGameObject: " + target.name);
                    }

                    else if (!finishSecondDemo && hit.collider.gameObject.tag == "Battery" /*|| !finishFirstDemo*/)
                    {
                      
                        //Debug.Log("distancia percorrida pelo raio: "+ rayCastLength);
                        target = hit.collider.gameObject;
                        target.transform.parent = null;
                        scaleBatteryOnFirstClick = true;
                        Debug.Log("CLIQUEI NA BATERIA");
                        if (canStartSecondPartOfSecondDemo)
                        {
                            Debug.Log("FIZ POWERUP CARALHO");
                            batteryClickCounter++;
                            target.GetComponent<Animator>().SetTrigger("canPowerUp");
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

            if (Input.GetMouseButtonUp(0))
            {
                if (finishFirstDemo)
                {
                   // Debug.Log("entrei aqui na flag de terminar a bobine");
                }
                //verifica se o objecto actual é a bobine e se o demo nãoa cabou e se não esta dentro do collider de chegada da bobine
                if (target != null && target.gameObject.tag == "Bobine" && (finishFirstDemo == false && isInsideFinCollider == false))
                {
                    Debug.Log("posso retornar a posição inicial");
                    canReturnOriginalPlace = true;
                    //flagObjectToMouse = true;
                    //returnObjectToPlace();
                }
                if (target != null && target.gameObject.tag == "Battery" && (finishSecondDemo == false && isInsideColliderBattery == false))
                {
                    Debug.Log("bateria pode retornar a posição inicial");
                    canReturnOriginalPlace = true;
                    //flagObjectToMouse = true;
                    //returnObjectToPlace();
                }

                flagDragDoor = false;
                flagObjectToMouse = false;
                flagBatteryToMouse = false;
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
        float rotY = Input.GetAxis("Mouse X") * DoorRotSpeed * Mathf.Deg2Rad; //rotY tendo em conta o movimento em X do rato
        
        

        //limitar as rotações entre 360 e 250


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

