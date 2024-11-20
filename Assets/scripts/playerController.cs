using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{

    public float playerSpeed;
    private float horizontalSpeed, verticalSpeed;
    Rigidbody2D rb;

    public GameObject distractionObject;
    public Transform firePoint;

    public float objectSpeed;
    public float timeThrow;
    public float maxThrowTime = 6;

    Vector2 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetKey(KeyCode.Mouse0))
        {
            if(timeThrow <= maxThrowTime)
            timeThrow += Time.deltaTime;
        }else if (timeThrow > 0)
        {
            throwObject(timeThrow);

        }
    }

    void throwObject(float force)
    {
        GameObject distraction = Instantiate(distractionObject, firePoint.position, firePoint.rotation);
        distraction.GetComponent<Rigidbody2D>().AddForce(transform.up * force / 35);

        distraction.GetComponent<DistractionObject>().VelocityReset(0.5f);
        timeThrow = 0;
    }

    void movePlayer()
    {
        horizontalSpeed = Input.GetAxisRaw("Horizontal");
        verticalSpeed = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(horizontalSpeed, verticalSpeed).normalized * playerSpeed;
    }

    private void FixedUpdate()
    {
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<enemyScript>()) {
            SceneManager.LoadScene(0);
        
        }
    }
}