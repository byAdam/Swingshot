using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RocketController : MonoBehaviour
{
	private Rigidbody2D rigidBody;
    private Vector3 startPosition;
    private SpriteRenderer spriteRenderer;
    private GravityController gravityController;
    private Animator rocketAnimator;
    public Sprite baseSprite;

    private bool isPlaying;

	void Awake()
	{
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        startPosition = gameObject.transform.position;
        gravityController = gameObject.GetComponent<GravityController>();
        rocketAnimator = gameObject.GetComponent<Animator>();
	}

    void Start()
    {
        GameEvents.instance.OnPlayChange += OnPlayChange;

        OnPlayChange(false);
    }

    void Update()
    {
    	// Set rotation based on velcitr
	 	float newRotation = CalculateRotation(rigidBody.velocity);
	 	transform.eulerAngles = new Vector3(0, 0, newRotation);

        if(!isPlaying)
        {
            rigidBody.velocity = new Vector3(0, 0, 0);
        }
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
    		GameEvents.instance.PlayChange();
    	}
    	else if(other.GetComponent<StarController>() != null)
    	{
    		GameEvents.instance.CollectStar(other);
    	}
    }

    void OnPlayChange(bool isPlaying)
    {
        this.isPlaying = isPlaying;

        if(isPlaying)
        {
            OnPlayStart();
        }
        else
        {
            OnPlayEnd();
        }
    }

    void OnPlayStart()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        rocketAnimator.enabled = true;
        rigidBody.simulated = true;
    }

    void OnPlayEnd()
    {
        spriteRenderer.color = new Color(0.64f, 0.64f, 0.64f, 0.24f);
        spriteRenderer.sprite = baseSprite;

        rocketAnimator.enabled = false;
        rigidBody.simulated = false;

        gameObject.transform.position = startPosition;
        rigidBody.velocity = new Vector3(0, 0, 0);
    }
}
