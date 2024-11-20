using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTutorial : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    public Transform playerPosition;
    void Start()
    {
        agent.speed = 3;   
    }

    void Update()
    {
        agent.SetDestination(playerPosition.position);


       



    }
}
