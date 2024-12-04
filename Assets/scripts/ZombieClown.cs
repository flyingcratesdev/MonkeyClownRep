using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieClown : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update()
    {

        agent.SetDestination(player.position);   
    }
}
