using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class enemyScript : MonoBehaviour
{
    public float rotationSpeed = 5;
    public FieldOfView view;
    public Transform[] patrolPoints;
    public int patrolIndex = 0;
    public int enemyTarget = 0;
    public float moveSpeed;
    public NavMeshAgent agent;
    public Vector3 offset;
    public int stateID = 0;
    public Transform player;
    private float spottedTime = 3;
    [SerializeField]
    private float spottedTimer;
    int previousIndex = -1;
    float minDistance = 0.4f;

    public float stunTimer = 3;
    public float destroyTimer = 3;
    public bool isStunned = false;
    //Distraction item detection
    public float radius = 5;
    public LayerMask detectionMask;
    public GameObject currentDistraction;
    public int health;
    public int maxHealth = 4;
    public GameObject baseClown;
    public GameObject playerDetectedVisual;
    public float seenSpeedMult = 2f;
    public bool isSleeping = false;

    void Start()
    {
        health = maxHealth;
        agent.speed = moveSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if(stateID == 3)
        {

            isSleeping = true;
            view.SetSleeping(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (agent.destination - transform.position).normalized;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float angle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
        if (!isStunned && !isSleeping)
        {
            if (spottedTimer > 0)
            {
                agent.speed = moveSpeed * seenSpeedMult;

                stateID = 1;
                spottedTimer -= Time.deltaTime;
            }
            else if (currentDistraction != null)
            {
                agent.speed = moveSpeed;

                playerDetectedVisual.SetActive(false);
                stateID = 2;
            }
            else
            {
                playerDetectedVisual.SetActive(false);
                agent.speed = moveSpeed;

                stateID = 0;
            }

 
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }else
        {


            if (spottedTimer > 0)
            {
                spottedTimer -= Time.deltaTime;
            }
        }
        if(stateID == 3)
        {


        }
        else if(stateID == 2)
        {
            agent.SetDestination(currentDistraction.transform.position);
            if (Vector2.Distance(transform.position, currentDistraction.transform.position) <= 0.5f)
            {

                StartCoroutine(stunEnemy(stunTimer));
                StartCoroutine(destroyDistraction(destroyTimer));
                currentDistraction = null;

            }

        }
        else if (stateID == 0)
        {
            if (Vector2.Distance(transform.position, patrolPoints[patrolIndex].position) > minDistance)
            {
                agent.SetDestination(patrolPoints[patrolIndex].position);
            }
            else
            {
                // Update the previous index
                previousIndex = patrolIndex;

                // Select a new random index that is different from the previous index
                do
                {
                    patrolIndex = Random.Range(0, patrolPoints.Length);
                }
                while (patrolIndex == previousIndex);

                // Set the destination to the new patrol point
                agent.SetDestination(patrolPoints[patrolIndex].position);
            }
        }
        else if(stateID == 1) {

            agent.SetDestination(player.position);
        }
        view.SetOrigin(transform.position);
        view.SetAimDirection(transform.right * -1);

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, direction, 0, detectionMask);

        // Check if something was hit
        if (hit.collider != null && currentDistraction == null)
        {
            if(hit.collider.GetComponent<DistractionObject>())
            {
                currentDistraction = hit.collider.gameObject;
                print("found item!");
            }
           
        }

    }


    IEnumerator stunEnemy(float stunTimer)
    {
        Debug.Log("stunned");
        stateID = -1;
        isStunned = true;
        agent.speed = 0;
        agent.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTimer);
        isStunned = false;
        agent.speed = moveSpeed;

        stateID = 0;
    }

    IEnumerator destroyDistraction(float destroyTimer)
    {
        yield return new WaitForSeconds(destroyTimer);
        Destroy(currentDistraction);
    }

    public void TakeDamage(int a)
    {

        health -= a;
        if(health <= 0)
        {
            Destroy(baseClown); 

        }


    }
    public void PlayerFound()
    {
        playerDetectedVisual.SetActive(true);
        spottedTimer = spottedTime;
    }
    public void PlayerHidden()
    {
        spottedTimer = 0;
    }
    public void StunPlayer()
    {
        StartCoroutine(stunEnemy(5));


    }
}
