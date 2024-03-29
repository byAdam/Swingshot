using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ManipulationController : MonoBehaviour
{
    private bool pressProcessed = true;
    private bool isDragging = false;
    private PlanetController planetController;
    private Vector3 pressPos;
    private float pressTime;
    private SpriteRenderer borderRenderer;

    private Vector3 lastValidPos;
    private SpriteRenderer spriteRenderer;

    public float minDragDistance = 0.2f; 
    public float longTime = 0.5f; 

    public GameObject validityBorder;
    public Sprite validSprite;
    public Sprite invalidSprite;

    private bool isEditable = true;

    void Awake()
    {
        planetController = gameObject.GetComponent<PlanetController>();
        borderRenderer = validityBorder.GetComponent<SpriteRenderer>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        LevelEvents.instance.OnPlayChange += OnPlayChange;
    }

    void OnPlayChange(bool isPlaying)
    {
        isEditable = !isPlaying;
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
            if(planetController != null)
            {
                planetController.SwapGravity();   
            }
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
        pos.x = Mathf.Round(pos.x/2)*2;
        pos.y = Mathf.Round(pos.y/2)*2;

        // If outside border
        if(planetController != null && planetController.WillCollideWithBorder(pos))
        {
            return;
        }

        if(planetController == null || !planetController.WillCollideWithAny(pos))
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

        borderRenderer.sortingLayerName = "Planet Drag";
        spriteRenderer.sortingLayerName = "Planet Drag";

        LevelEvents.instance.DragChange(true);

        SoundManager.instance.PlayEffect(SoundEffect.StartMove);
    }

    void OnDraggingEnd()
    {
        isDragging = false;
        validityBorder.SetActive(false);

        if(lastValidPos == gameObject.transform.position)
        {
            SoundManager.instance.PlayEffect(SoundEffect.GoodMove);
        }
        else
        {
            SoundManager.instance.PlayEffect(SoundEffect.BadMove);
        }

        gameObject.transform.position = lastValidPos;

        borderRenderer.sortingLayerName = "Planet";
        spriteRenderer.sortingLayerName = "Planet";

        LevelEvents.instance.DragChange(false);
    }

    public void OnMouseDown()
    {
        Debug.Log(Environment.StackTrace);
    	// If not editable, silently process press
        pressProcessed = !isEditable;

        pressPos = getPressPos();
        pressTime = Time.time;
    }

    public void OnMouseUp()
    {
    	if(!pressProcessed)
    	{	
            if(planetController != null)
            {
                planetController.ChangeSize();
            }
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
