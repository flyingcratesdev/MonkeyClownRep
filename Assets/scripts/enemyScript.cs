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
    int previousIndex = -1;
    float minDistance = 0.4f;
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
    }
    public void PlayerFound()
    {
        spottedTimer = spottedTime;
     
    }

}
