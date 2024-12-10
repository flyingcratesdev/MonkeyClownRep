using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeCall : MonoBehaviour
{
    public List<enemyScript> wakeClowns;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       


    }

    public void WakeUp()
    {

        foreach (enemyScript script in wakeClowns)
        {

            script.StopSleeping();

        }

    }
}
