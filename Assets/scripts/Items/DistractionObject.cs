using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionObject : MonoBehaviour
{

    public Rigidbody2D rb;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void VelocityReset(float time)
    {
        Invoke("ZeroVel", time);



    }

    void ZeroVel()
    {

        rb.velocity = Vector3.zero;


    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {

            print("Test");
            ZeroVel();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {

            print("Test");
            ZeroVel();
        }
    }

}
