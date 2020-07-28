using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RocketController : MonoBehaviour
{

	private Rigidbody2D rigidBody;

	void Awake()
	{
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
	}

    void Update()
    {
    	// Set rotation based on velcitr
	 	float newRotation = CalculateRotation(rigidBody.velocity);
	 	transform.eulerAngles = new Vector3(0, 0, newRotation);
    }

	float CalculateRotation(Vector3 velocity)
    {
        return Mathf.Atan2(velocity.x, velocity.y) * Mathf.Rad2Deg * -1;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
    	GameObject other = collision.gameObject;

    	if(other.GetComponent<PlanetController>() != null)
    	{
    		Destroy(gameObject);
    	}
    	else if(other.GetComponent<StarController>() != null)
    	{
    		GameEvents.instance.CollectStar(other);
    	}
    }
}
