using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dismantle : MonoBehaviour {

    [SerializeField]
    private Transform objectToDismantle;

    [SerializeField]
    private float rotMin;

    [SerializeField]
    private float rotMax;

    [SerializeField]
    private float transMinX;

    [SerializeField]
    private float transMaxX;

    [SerializeField]
    private float transMinY;

    [SerializeField]
    private float transMaxY;

    [SerializeField]
    private float transMinZ;

    [SerializeField]
    private float transMaxZ;

    [SerializeField]
    private float animationTime;

    private List<float> rotationList;
    
    private List<Vector3> positionToGoList;

    private bool canDismantle;
    private bool canReturnOriginal;
    private int listCount;
    private bool start;
    private List<Vector3> objectContainerIni;
    private List<Transform> objectContainerToMove;

    
	// Use this for initialization
	void Start () {
        objectContainerIni = new List<Vector3>();
        objectContainerToMove = new List<Transform>();

        rotationList = new List<float>();
        positionToGoList = new List<Vector3>();
        canDismantle = false;
        canReturnOriginal = false;
        start = false;
       
        listCount = 0;
        populateContainer();
    }
	
	// Update is called once per frame
	void Update () {
        
        float step = animationTime * Time.deltaTime;
        
            if (Input.GetMouseButtonDown(0))
            {
                if (canDismantle)
                {
                    canDismantle = false;
                    canReturnOriginal = true;
                    step = 0;
                    //time = 0;

                }
                else if (canReturnOriginal)
                {
                    generateRandomPosition();
                    canDismantle = true;
                    canReturnOriginal = false;
                    step = 0;
                    //time = 0;
                }
                else if (canDismantle == false && canReturnOriginal == false)
                {
                    canDismantle = true;
                }
            }
            if (canDismantle)
            {
                dismantle(step);
            }
            if (canReturnOriginal)
            {
                returnToOriginalForm(step);
            }
        
    }

    void generateRandomPosition()
    {
        int sizeOfList = positionToGoList.Count;
        for (int i = 0; i < sizeOfList; i++)
        {
            positionToGoList[i] = new Vector3(Random.Range(transMinX, transMaxX), Random.Range(transMinY, transMaxY), Random.Range(transMinZ, transMaxZ));
        }
        
    }
    void populateContainer()
    {
        Debug.Log("Quero popular");
        foreach (Transform child in objectToDismantle)
        {
            objectContainerIni.Add(new Vector3(child.transform.position.x, child.transform.position.y, child.transform.position.z));
            objectContainerToMove.Add(child);
            rotationList.Add(Random.Range(rotMin, rotMax));
            positionToGoList.Add(new Vector3(Random.Range(transMinX, transMaxX), Random.Range(transMinY, transMaxY), Random.Range(transMinZ, transMaxZ)));
            Debug.Log("posições dos filhos inicialmente: " + child.transform.position.x);
        }
        //prints();
    }

    public void comecar()
    {
        start = true;
    }

    //TrabalhoFuturo possibilitar em orbita as translações
    void dismantle(float step)
    {
        foreach (Transform child in objectContainerToMove)
        {
            if (!child.Equals(objectToDismantle))
            {
                child.transform.position = Vector3.MoveTowards(child.transform.position, positionToGoList[listCount], step);
                listCount++;
            }
        }
        Debug.Log("saiDoDismantle");
        listCount = 0;
    }

    private void returnToOriginalForm(float step)
    {
        foreach (Transform child in objectContainerToMove)
        {
            if (!child.Equals(objectToDismantle))
            {
                child.transform.position = Vector3.MoveTowards(child.transform.position, objectContainerIni[listCount], step);
                listCount++;
            }
        }
        Debug.Log("saiDoReturOriginal");
        listCount = 0;
    }
  
    
}





