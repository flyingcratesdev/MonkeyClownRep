using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

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
    public bool isHidden = false;
    Vector2 mousePosition;

    //StunGun
    public GameObject StunPellet;
    public GameObject Bullet;


    public ItemPickUp potentialItem;
    public TMP_Text itemText;
    //ID bananna, stunball, 
    public int currentItem;
    public int itemSwitchTag = 0;
    public int[] inventoryID = {0,0};
    public List<Transform> slots;
    public GameObject selector;
    bool isFast = false;
    public int[] uses = {0,0};
    float stunGunFirerate = 1;
    float timeStunGun;
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
        switchItem();
    }



    void PickUpItem()
    {
        if (Input.GetKeyDown(KeyCode.E) && potentialItem != null)
        {

            inventoryID[itemSwitchTag] = potentialItem.GetItem();
            currentItem = inventoryID[itemSwitchTag];
            uses[itemSwitchTag] = potentialItem.item.uses;
            slots[itemSwitchTag].GetComponent<Image>().sprite = potentialItem.item.icon;
            Destroy(potentialItem.gameObject);
            slots[itemSwitchTag].GetComponentInChildren<TMP_Text>().text = "" + uses[itemSwitchTag];
            potentialItem = null;



        }



    }

    void switchItem()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (itemSwitchTag < 1)
            {
                itemSwitchTag++;
            }else
            {
                itemSwitchTag = 0;
            }
            selector.transform.position = slots[itemSwitchTag].transform.position;
            currentItem = inventoryID[itemSwitchTag];
         
        }

        
    }

    void UsingItems()
    {
        switch(currentItem)
        {
            case 0:

                break;
            case 1:
                if (Input.GetKey(KeyCode.Mouse0) && uses[itemSwitchTag] > 0)
                {
                    if (timeThrow <= maxThrowTime)
                        timeThrow += Time.deltaTime;
                }
                else if (timeThrow > 0)
                {
                    throwObject(timeThrow);

                    uses[itemSwitchTag]--;
                    slots[itemSwitchTag].GetComponentInChildren<TMP_Text>().text = "" + uses[itemSwitchTag];



                }

                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Mouse0) && uses[itemSwitchTag] > 0 && timeStunGun <= 0)
                {
                    GameObject ball = Instantiate(StunPellet, firePoint.position, firePoint.rotation);

                    uses[itemSwitchTag]--;
                    slots[itemSwitchTag].GetComponentInChildren<TMP_Text>().text = "" + uses[itemSwitchTag];
                    timeStunGun = stunGunFirerate;

                }
                if(timeStunGun > 0)
                {

                    timeStunGun -= Time.deltaTime;

                }

                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Mouse0) && !isFast && uses[itemSwitchTag] > 0)
                {

                    playerSpeed *= 2;
                    Invoke("SpeedReset", 2);

                    uses[itemSwitchTag]--;
                    slots[itemSwitchTag].GetComponentInChildren<TMP_Text>().text = "" + uses[itemSwitchTag];

                    isFast = true;
                }
                break;
            case 4:
                if (Input.GetKey(KeyCode.Mouse0) && uses[itemSwitchTag] > 0 && timeStunGun <= 0)
                {
                    GameObject ball = Instantiate(Bullet, firePoint.position, firePoint.rotation);

                    uses[itemSwitchTag]--;
                    slots[itemSwitchTag].GetComponentInChildren<TMP_Text>().text = "" + uses[itemSwitchTag];
                    timeStunGun = 0.3f;

                }
                if (timeStunGun > 0)
                {

                    timeStunGun -= Time.deltaTime;

                }

                break;
        }



      


    }
    void SpeedReset()
    {

        playerSpeed = 3.5f;
        isFast = false;

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