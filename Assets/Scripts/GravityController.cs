﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
	public float gravitationalConstant = 0.01F;

	private LevelManager levelManager;
	private Rigidbody2D rigidBody;

	void Awake()
	{
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		levelManager = LevelManager.instance;
	}

    // Update is called once per frame
    void Update()
    {
        float velocityChangeX = 0;
        float velocityChangeY = 0;

        Vector2 currentPos = transform.position;
        foreach (GameObject planet in levelManager.planets)
        {
            PlanetController planetController = planet.GetComponent<PlanetController>();
            float mass = planetController.mass;

            Vector2 planetPos = planet.transform.position;
            float distance = Vector2.Distance(currentPos, planetPos);

            float acceleration = CalculateAcceleration(mass, distance);
            float angle = CalculateAngle(currentPos, planetPos);

            velocityChangeX += acceleration * Mathf.Cos(angle);
            velocityChangeY += acceleration * Mathf.Sin(angle);
        }

        Vector2 currentVelocity = rigidBody.velocity;
        Vector2 newVelocity = new Vector2(currentVelocity.x + velocityChangeX, currentVelocity.y + velocityChangeY);
        rigidBody.velocity = newVelocity;
    }

    private float CalculateAngle(Vector2 me, Vector2 target)
    {
        return Mathf.Atan2(target.y - me.y, target.x - me.x);
    }

    private float CalculateAcceleration(float mass, float distance)
    {
        return gravitationalConstant * (mass / (distance * distance));
    }
}
