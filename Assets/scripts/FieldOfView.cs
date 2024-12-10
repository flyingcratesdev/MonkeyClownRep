using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class FieldOfView : MonoBehaviour
{

    [SerializeField]
    private LayerMask layerMask;
    private Mesh mesh;
    public float fov = 75f;
    Vector3 origin;
    [SerializeField]
    private float startingAngle;
    int rayCount = 40;
    public float viewDistance = 5f;
    public enemyScript enemy;
    public bool isSleeping = false;
    void Start()
    {
        transform.position = new Vector3(0,0,0f);
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
       
    }

    public void SetSleeping(bool a)
    {
        if (a)
        {
            isSleeping = true;
            fov = 360;
            viewDistance = 2;
        }
        else

        {

            isSleeping = false;
            viewDistance = 5;
            fov = 75;

        }

    }

    public Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));


    }
    public float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(n < 0) n += 360;
        return n;

    }
 


    // Update is called once per frame
    void LateUpdate()
    {
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            if (raycastHit2D.collider == null)
            {

                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                if (raycastHit2D.collider.GetComponent<playerController>())
                {
                    if(isSleeping)
                    {
                        SetSleeping(false);
                        enemy.isSleeping = false;
                    }
                    else if (raycastHit2D.collider.GetComponent<playerController>().isHidden)
                    {
                        enemy.PlayerHidden();


                    }
                    else if (raycastHit2D.collider.GetComponent<playerController>())
                    {
                        enemy.PlayerFound();

                    }
                }
                vertex = raycastHit2D.point;

            }
            vertices[vertexIndex] = vertex;
            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;



        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public void SetOrigin(Vector3 neworigin)
    {
        this.origin = neworigin;
    }
    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVectorFloat(aimDirection) - fov/2f;


    }
}
