using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobineToTarget : MonoBehaviour {
    private bool canMove;
    public float speed;
    public ParticleSystem starsPS;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider other)
    {

        GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().isInsideFinCollider = true;
    }

    void OnTriggerStay(Collider other)
    {
        //apenas pode mudar quando o objecto n esta a ser clicado quer dizer que o objecto é para fazer snap 
        canMove = !GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().flagObjectToMouse;
        

        if (canMove)
        {
            moveToPlace(other);
        }
        //Se a posição do objecto for a mm que o target então retiramos o trigger para não haver Extra computação
        if(other.gameObject.transform.position == transform.position)
        {
            //Debug.Log("entrei aqui nesta cena da flag a true");
            Destroy(GameObject.FindGameObjectWithTag("FadeBobine"));
            GetComponent<BoxCollider>().isTrigger = false;
            //GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().canReturnOriginalPlace = false;
            Instantiate(starsPS);
            
            GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().finishFirstDemo = true; //mete flag a true para dizer que ja acabou o demo para puder passar a outras coisas
            GameObject.FindGameObjectWithTag("GameController").GetComponent<EletricityController>().telefonou = false;
        }

    }

    void OnTriggerExit(Collider other)
    {
       // Debug.Log("sai no Trigger");
        GameObject.FindGameObjectWithTag("GameController").GetComponent<RaycastColliderDetection>().isInsideFinCollider = false;
    }

    void moveToPlace(Collider other)
    {
        float step = speed * Time.deltaTime;
        other.gameObject.transform.position = Vector3.MoveTowards(other.gameObject.transform.position, transform.position, step);
        
    }
}
