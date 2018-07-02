using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneParticleController : MonoBehaviour {

    public bool filter;
    public ParticleSystem ps;
    private float time;
    // Use this for initialization
    void Start()
    {
        time = ps.GetComponent<ParticleSystemFollowPath>().time;
        Invoke("instantiatePSLeft", 0);
        Invoke("instantiatePSRight", 0);

    }




    void instantiatePSLeft()
    {
        ps.GetComponent<ParticleSystemFollowPath>().pathName = "EletricityPathLeft";
        Instantiate(ps);
    }
    void instantiatePSRight()
    {
        ps.GetComponent<ParticleSystemFollowPath>().pathName = "EletricityPathRight";
        Instantiate(ps);
    }

    void Update()
    {
        if (filter && !IsInvoking("instantiatePSLeft"))
        {
            Debug.Log("entrei aqui no invoke");
            InvokeRepeating("instantiatePSLeft", 0, time);
            InvokeRepeating("instantiatePSRight", 0, time);

        }
        else if (filter == false)
        {
            CancelInvoke();
        }


    }
}
