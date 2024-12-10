using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeCall : MonoBehaviour
{

    audioManager audioManager;

    public List<enemyScript> wakeClowns;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();
    }

    // Update is called once per frame
    void Update()
    {
       


    }

    public void WakeUp()
    {
        audioManager.playSound(audioManager.sfx6);
        foreach (enemyScript script in wakeClowns)
        {

            script.StopSleeping();

        }

    }
}
