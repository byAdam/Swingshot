using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulationController : MonoBehaviour
{
    private bool isPressed = false;
    private bool isDragging = false;
    private bool pressProccessed = false;
    private float timeSincePress;
    private PlanetController planetController;

    private float doublePressTime = 0.2f;

    void Awake()
    {
        planetController = gameObject.GetComponent<PlanetController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPressed && !isDragging && Time.time - timeSincePress > doublePressTime)
        {
            isDragging = true;
            pressProccessed = true;
        }

        if(!pressProccessed && Time.time - timeSincePress > doublePressTime)
        {
            planetController.ChangeSize();
            pressProccessed = true;
        }

        if(isDragging)
        {
            WhileDragging();
        }
    }

    void WhileDragging()
    {
       Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       pos.z = 0;
       planetController.UpdatePosition(pos);
    }

    public void OnMouseDown()
    {
        pressProccessed = false;

        if(Time.time - timeSincePress <= doublePressTime)
        {
            planetController.SwapGravity();
            pressProccessed = true;
        }

    	isPressed = true;
        timeSincePress = Time.time;
    }

    public void OnMouseUp()
    {
        isPressed = false;
        isDragging = false;
        timeSincePress = Time.time;
    }
}
