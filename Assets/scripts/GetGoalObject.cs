using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaPickup : MonoBehaviour
{
    public AudioClip getBanana; // Drag and drop your sound effect here
    private AudioSource audioSource;

    private void Start()
    {
        // Add an AudioSource component to play the sound if one isn't already attached
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // Prevent the sound from playing automatically
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Play the pickup sound
            if (getBanana != null)
            {
                audioSource.PlayOneShot(getBanana);
            }
            else
            {
                Debug.LogWarning("No sound effect assigned to getBanana!");
            }

            // Destroy the banana object after a small delay to let the sound play
            Destroy(gameObject, 0.2f);
        }
    }
}