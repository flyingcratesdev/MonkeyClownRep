using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    private bool canReenterBarrel = true; // Tracks if the player can re-enter the barrel

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if the colliding object has the Player tag
        if (collision.CompareTag("Player") && canReenterBarrel)
        {
            // Starts hiding
            StartCoroutine(HidePlayer(collision.gameObject));
        }
    }

    private IEnumerator HidePlayer(GameObject player)
    {
        // Disable immediate re-entry
        canReenterBarrel = false;
        player.GetComponent<playerController>().isHidden = true;
        // Move the player to the center of the barrel
        player.transform.position = transform.position;

        // Disable player movement
        var rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Stop current movement
            rb.constraints = RigidbodyConstraints2D.FreezePosition; // Freeze position
        }

        yield return new WaitForSeconds(1f); // Wait for 1 second

        // Allow the player to move again
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Allow movement but keep rotation frozen
        }

        // Wait for the player to move
        yield return new WaitUntil(() =>
            rb != null && (rb.velocity.magnitude > 0.1f || Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0));
            player.GetComponent<playerController>().isHidden = false;

        // Wait .5 seconds before allowing the player to re-enter
        yield return new WaitForSeconds(.5f);

        canReenterBarrel = true;
    }
}