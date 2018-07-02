using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendWavesFromCamera : MonoBehaviour
{
    public GameObject gameController;
    public GameObject plate;
    private bool speaking;
    private RaycastHit hit;
    private float rayCastLenght;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsInvoking("refreshVariables"))
        {

            Invoke("refreshVariables", 1.0f);
            
        }
        if (speaking)
        {
            Ray ray = Camera.main.ScreenPointToRay(plate.transform.position);
            if (Physics.Raycast(ray, out hit, 100))
            {

                Debug.DrawRay(ray.origin, ray.direction, Color.red, 100, true);
                if (hit.collider.gameObject.tag == "VibratePlate")
                {
                    rayCastLenght = hit.distance;
                }
            }
        }


    }

    private void sendSoundWavesToPlate()
    {

    }

    private void refreshVariables()
    {
        speaking = gameController.GetComponent<RaycastColliderDetection>().speaking;
        //gameController.GetComponent<RaycastColliderDetection>().atendeu
    }
}