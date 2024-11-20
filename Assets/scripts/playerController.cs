using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    [Range(0f, 10f)]
    public float playerSpeed;
    private float horizontalSpeed, verticalSpeed;
    Rigidbody2D rb;

    public GameObject distractionObject;
    public Transform firePoint;

    public float objectSpeed;
    public float timeThrow;
    public float maxThrowTime = 6;

    Vector2 mousePosition;

    //StunGun
    public GameObject StunPellet;


    public ItemPickUp potentialItem;
    //ID bananna, stunball, 
    public int currentItem;
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
        UsingItems();
        PickUpItem();
    }



    void PickUpItem()
    {
        if( Input.GetKeyDown(KeyCode.E))
        {

            currentItem = potentialItem.GetItem();
            Destroy(potentialItem.gameObject);
            potentialItem = null;


        }



    }

    void UsingItems()
    {
        switch(currentItem)
        {
            case 0:

                break;
            case 1:
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (timeThrow <= maxThrowTime)
                        timeThrow += Time.deltaTime;
                }
                else if (timeThrow > 0)
                {
                    throwObject(timeThrow);

                }
                break;
            case 2:
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    GameObject ball = Instantiate(StunPellet, firePoint.position, firePoint.rotation);
                    
                    currentItem = 0;
                }

                    break;
            case 3:


                break;
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
        if (collision.GetComponent<ItemPickUp>())
        {
            potentialItem = collision.GetComponent<ItemPickUp>();

        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<ItemPickUp>()) {
            potentialItem = null;


        }
    }
}