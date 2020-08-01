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
    private SpriteRenderer borderRenderer;

    private Vector3 lastValidPos;

    public float minDragDistance = 0.2f; 
    public float longTime = 0.5f; 

    public GameObject validityBorder;
    public Sprite validSprite;
    public Sprite invalidSprite;

    void Awake()
    {
        planetController = gameObject.GetComponent<PlanetController>();

        borderRenderer = validityBorder.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!pressProcessed && Vector3.Distance(pressPos, getPressPos()) > minDragDistance)
        {
            OnDraggingStart();
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
        Vector3 pos = getPressPos();

        if(!planetController.WillCollideWithAny(pos))
        {
            borderRenderer.sprite = validSprite;
            lastValidPos = pos;
        }
        else
        {
            borderRenderer.sprite = invalidSprite;
        }

        gameObject.transform.position = pos;
    }

    void OnDraggingStart()
    {
        isDragging = true;
        pressProcessed = true;
        validityBorder.SetActive(true);
    }

    void OnDraggingEnd()
    {
        isDragging = false;
        validityBorder.SetActive(false);
        gameObject.transform.position = lastValidPos;
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

        if(isDragging)
        {
            OnDraggingEnd();
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
