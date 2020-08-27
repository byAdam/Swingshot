using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{

    public Vector2 orbitPos;
    public float orbitRadius = 8;
    public float rps = 1;
    public bool isRelative;

    private float W;
    private float angle;

    private float canvasScale;

    public GameObject c;


    void Start()
    {
        canvasScale = c.GetComponent<Canvas>().scaleFactor;



        W = rps * Mathf.PI * 2; 

        Vector2 currentPos = transform.position;
        
        if(isRelative)
        {
            orbitPos.x = currentPos.x + (orbitPos.x * canvasScale);
            orbitPos.y = currentPos.y + (orbitPos.y * canvasScale);
        }

        orbitRadius = orbitRadius * canvasScale;


        angle = CalculateAngle(currentPos, orbitPos) + Mathf.PI;
    }

    void Update()
    {
        angle += W * Time.deltaTime;
 
        Vector2 newPos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * orbitRadius;
        transform.position = newPos + orbitPos;

        transform.eulerAngles = new Vector3(0, 0, (angle * (180/Mathf.PI)) + 180);
    }

    float CalculateAngle(Vector2 me, Vector2 target)
    {
        return Mathf.Atan2(target.y - me.y, target.x - me.x);
    }
}


