using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateParticleSystem : MonoBehaviour {
    private ParticleSystem ps;
    private GameObject script;
    private Vector3 iniPos;
    private ParticleSystemFollowPath particleSystemFollowScript;
    // Use this for initialization
    void Start () {
        ps = GetComponent<ParticleSystem>();
        iniPos = GetComponent<Transform>().position;
        particleSystemFollowScript = GetComponent<ParticleSystemFollowPath>();
    }
	
	// Update is called once per frame
	void Update () {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                gameObject.transform.position = iniPos;
                particleSystemFollowScript.enabled = false;
                gameObject.SetActive(false);
            }
        }
    }
}
