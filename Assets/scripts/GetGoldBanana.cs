using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGoldBanana : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Destroy the banana object
            Destroy(gameObject);
            Debug.Log("Player got the Gold Banana!");
        }
    }
}