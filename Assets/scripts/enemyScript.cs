using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class enemyScript : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(spottedTimer > 0)
        {
            stateID = 1;
            spottedTimer -= Time.deltaTime;
        }else
        {
            stateID = 0;
        }

        Vector2 direction = (agent.destination - transform.position).normalized;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; 

        float angle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, 4 * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (stateID == 0)
        {
            if (Vector2.Distance(transform.position, patrolPoints[0].position) > 0.25f && patrolIndex == 0)
            {
                agent.SetDestination(patrolPoints[0].position);
            }
            else if (patrolIndex == 0)
            {

                patrolIndex++;
            }
            if (Vector2.Distance(transform.position, patrolPoints[1].position) > 0.25f && patrolIndex == 1)
            {
                agent.SetDestination(patrolPoints[1].position);
            }
            else if (patrolIndex == 1)
            {

                patrolIndex++;
            }
            if (Vector2.Distance(transform.position, patrolPoints[2].position) > 0.25f && patrolIndex == 2)
            {
                agent.SetDestination(patrolPoints[2].position);
            }
            else if (patrolIndex == 2)
            {

                patrolIndex++;
            }
            if (Vector2.Distance(transform.position, patrolPoints[3].position) > 0.25f && patrolIndex == 3)
            {
                agent.SetDestination(patrolPoints[3].position);
            }
            else if (patrolIndex == 3)
            {

                patrolIndex = 0;
            }
        }else if(stateID == 1) {

            agent.SetDestination(player.position);
        }
        view.SetOrigin(transform.position);
        view.SetAimDirection(transform.right * -1);
    }
    public void PlayerFound()
    {
        spottedTimer = spottedTime;
     
    }

}
