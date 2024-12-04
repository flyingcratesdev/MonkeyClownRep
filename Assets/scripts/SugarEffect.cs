using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarEffect : MonoBehaviour
{
    public float sugarEffectDuration = 5f;
    private TrailRenderer trailRenderer;

    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer != null)
            trailRenderer.enabled = false; // Ensures trail is off at start
    }

    public void ActivateSugarEffect()
    {
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
            Invoke(nameof(DeactivateSugarEffect), sugarEffectDuration);
        }
    }

    private void DeactivateSugarEffect()
    {
        if (trailRenderer != null)
            trailRenderer.enabled = false;
    }
}
