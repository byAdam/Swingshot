using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{	
	GameObject currentObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Place planet - Quick Tap
    // Double tap - Sawp
    // Quick tap - cycle through descrete options
    // Long press - Moving

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
        	OnMouseDown(Input.mousePosition);
        }
        
        if(Input.GetMouseButtonUp(0))
        {
        	OnMouseUp(Input.mousePosition);
        }
    }

    void OnMouseDown(Vector3 pos)
    {
    	Ray ray = Camera.main.ScreenPointToRay(pos);
    	RaycastHit hit;

    	if(Physics.Raycast(ray, out hit))
    	{
    		ProcessHit(hit.transform.gameObject, true);
    	}
    	else
    	{
    		//LevelManager.instance.PlacePlanet(pos);
    	}
    }

    void ProcessHit(GameObject obj, bool isDown)
    {
    	if(obj.GetComponent<PlanetController>() != null)
    	{
    		ManipulationController m = obj.GetComponent<ManipulationController>();

    		if(isDown)
    		{
    			m.OnMouseDown();
    			currentObject = obj;
    		}
    		else
    		{
    			m.OnMouseUp();
    		}
    	}
    }

    void OnMouseUp(Vector3 pos)
    {
    	if(currentObject != null)
    	{
    		ProcessHit(currentObject, false);
    	}

    	currentObject = null;
    }
}
