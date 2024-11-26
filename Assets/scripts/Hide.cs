using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    private bool isNearBarrel = false; // Tracks if the player is near a barrel
    private bool isHidden = false; // Tracks if the player is hidden
    private GameObject currentBarrel; // Tracks the barrel the player is hiding in
    private SpriteRenderer playerSpriteRenderer; // Reference to the player's sprite renderer

    void Start()
    {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Check for the "hide/unhide" action
        if (Input.GetKeyDown(KeyCode.Space) && isNearBarrel)
        {
            if (isHidden)
            {
                Unhide();
            }
            else
            {
                Hide();
            }
        }
    }

    private void Hide()
    {
        isHidden = true;
        playerSpriteRenderer.enabled = false; // Make the player "invisible"
        transform.position = currentBarrel.transform.position; // Snap to the barrel's position
    }

    private void Unhide()
    {
        isHidden = false;
        playerSpriteRenderer.enabled = true; // Make the player "visible" again
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrel"))
        {
            isNearBarrel = true;
            currentBarrel = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrel"))
        {
            isNearBarrel = false;
            currentBarrel = null;
        }
    }
}