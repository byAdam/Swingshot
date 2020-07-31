using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulationController : MonoBehaviour
{
    private bool pressProcessed = false;
    private bool isDragging = false;
    private PlanetController planetController;
    private Vector3 pressPos;
    private float pressTime;

    public float minDragDistance = 0.2f; 
    public float longTime = 0.5f; 

    void Awake()
    {
        planetController = gameObject.GetComponent<PlanetController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!pressProcessed && Vector3.Distance(pressPos, getPressPos()) > minDragDistance)
        {
            isDragging = true;
            pressProcessed = true;
        }

        if(!pressProcessed && Time.time - pressTime > longTime)
        {
            planetController.SwapGravity();
            pressProcessed = true;
        }

        if(isDragging)
        {
            WhileDragging();
        }
    }

    void WhileDragging()
    {
       planetController.UpdatePosition(getPressPos());
    }

    public void OnMouseDown()
    {
    	pressProcessed = false;
        pressPos = getPressPos();
        pressTime = Time.time;
    }

    public void OnMouseUp()
    {
    	if(!pressProcessed)
    	{	
    		planetController.ChangeSize();
        }

        isDragging = false;
        pressProcessed = true;
    }

    private Vector3 getPressPos()
    {
   		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       	pos.z = 0;
       	return pos;
    }
}
