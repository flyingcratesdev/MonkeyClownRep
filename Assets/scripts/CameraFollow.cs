using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public float lerpSpeed = 5f;
    void Start()
    {
        
    }

    void Update()
    {

        Vector3 newPos = new Vector3(target.position.x, target.position.y, transform.position.z);  
        transform.position = Vector3.Lerp(transform.position,newPos , Time.deltaTime * lerpSpeed);
    }

}
