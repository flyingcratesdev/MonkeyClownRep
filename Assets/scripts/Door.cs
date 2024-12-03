using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform target;         
    public Transform door;         
    public float smoothTime = 0.3f;    // Time for smooth transition
    public float maxSpeed = 10f;       // Maximum movement speed
    public bool isOpen = false;
    private bool isLocked = true;
    private Vector2 currentVelocity;   // Stores the current velocity


    void Start()
    {
        Key.OnKeyCollected += UnlockDoor;
    }
    void Update()
    {
        if (isOpen)
        {
            // Get the current position of the object
            Vector2 currentPosition = door.position;

            // Target position (set to target's position)
            Vector2 targetPosition = target.position;

            // Smoothly move towards the target position
            Vector2 newPosition = Vector2.SmoothDamp(
                currentPosition,
                targetPosition,
                ref currentVelocity,
                smoothTime,
                maxSpeed,
                Time.deltaTime
            );

            // Apply the new position to the object
            door.position = newPosition;
        }
    }

    private void OnDestroy()
    {
        Key.OnKeyCollected -= UnlockDoor;
    }

    private void UnlockDoor()
    {
        isLocked = false;
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<playerController>() && !isLocked)
        {
            isOpen = true;
        }
    }


}
