using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public float playerSpeed;
    private float horizontalSpeed, verticalSpeed;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    void movePlayer()
    {
        horizontalSpeed = Input.GetAxisRaw("Horizontal") * playerSpeed;
        verticalSpeed = Input.GetAxisRaw("Vertical") * playerSpeed;
        rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }
}