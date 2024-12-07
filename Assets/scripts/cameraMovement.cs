using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] Transform player;
    [SerializeField] float threshold;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPosition = (player.position + mousePosition);

        targetPosition.x = Mathf.Clamp(targetPosition.x, -threshold + player.position.x, threshold + player.position.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -threshold + player.position.y, threshold + player.position.y);

        this.transform.position = targetPosition;
    }
}
