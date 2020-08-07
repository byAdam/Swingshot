using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{

    public Vector2 orbitPos;
    public float orbitRadius = 8;
    public float rps = 1;

    private float W;
    private float angle;

    void Start()
    {
        W = rps * Mathf.PI * 2;

        Vector2 currentPos = transform.position;
        angle = CalculateAngle(currentPos, orbitPos) + Mathf.PI;
    }

    void Update()
    {
        angle += W * Time.deltaTime;
 
        Vector2 newPos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * orbitRadius;
        transform.position = newPos + orbitPos;
    }

    float CalculateAngle(Vector2 me, Vector2 target)
    {
        return Mathf.Atan2(target.y - me.y, target.x - me.x);
    }

}
