using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public delegate void KeyCollectedAction();
    public static event KeyCollectedAction OnKeyCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnKeyCollected?.Invoke(); //Notify the key has been collected
            Destroy(gameObject); //Remove key from scene
        }
    }
}

