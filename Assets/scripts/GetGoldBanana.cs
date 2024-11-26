using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGoldBanana : MonoBehaviour
{
    // Called when another object enters the collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Destroy the banana object to make it disappear
            Destroy(gameObject);
            Debug.Log("Player got the Gold Banana!");
        }
    }
}