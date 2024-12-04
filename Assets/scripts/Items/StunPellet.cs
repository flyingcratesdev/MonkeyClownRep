using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunPellet : MonoBehaviour
{

    public bool isBullet = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<enemyScript>()) {

            if(!isBullet)
            {
                collision.GetComponent<enemyScript>().StunPlayer();
                Destroy(gameObject);


            }else
            {
                collision.GetComponent<enemyScript>().TakeDamage();
                Destroy(gameObject);


            }



        }
    }
}
