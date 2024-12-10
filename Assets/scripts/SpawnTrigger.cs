using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{


    public Transform spawnPos;
    public GameObject zombieClown;
    public int enemyCount = 2;
    bool once = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<playerController>() && !once)
        {

            for(int i = 0; i < enemyCount; i++)
            {
                Instantiate(zombieClown, spawnPos.position, Quaternion.identity);
                
            }
            once = true;
        }
    }
}
