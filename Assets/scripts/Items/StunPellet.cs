using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunPellet : MonoBehaviour
{


    public float force = 4;
    public float deathTime = 3f;
    void Awake()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * force);
        Destroy(gameObject, deathTime);
    }

    void Update()
    {
        
    }
}
