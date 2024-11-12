using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyScript : MonoBehaviour
{
    public FieldOfView view;
    public Transform[] patrolPoints;
    public int enemyTarget = 0;
    public float moveSpeed;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent.updatePosition = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        view.SetOrigin(transform.position);
        view.SetAimDirection(transform.up);
    }

    void moveEnemy()
    {

    }

}
