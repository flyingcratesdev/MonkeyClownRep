using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownHealth : MonoBehaviour
{

    public int health;
    public int maxHealth = 4;
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int a)
    {

        health -= a;
        if (health <= 0)
        {
            Destroy(gameObject);

        }

    }
}
