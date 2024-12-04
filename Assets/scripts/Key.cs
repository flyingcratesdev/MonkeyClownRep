using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
   
    public Door doorScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            // OnKeyCollected?.Invoke(); //Notify the key has been collected
            doorScript.UnlockDoor();

            Destroy(gameObject); //Remove key from scene
        }
    }


}

