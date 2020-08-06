using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
	private float gravitationalConstant = 0.1F;
    public float distancePow = 2f;

    private Rigidbody2D rigidBody;
	private LevelManager levelManager;

    private static float roundConstant = Mathf.Pow(10, 7);

	void Awake()
	{
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		levelManager = LevelManager.instance;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 currentPos = transform.position;
        foreach (GameObject planet in levelManager.planets)
        {
            PlanetController planetController = planet.GetComponent<PlanetController>();
            float mass = planetController.mass;

            Vector2 planetPos = planet.transform.position;
            float distance = Vector2.Distance(currentPos, planetPos);

            float acceleration = CalculateAcceleration(mass, distance);
            float angle = CalculateAngle(currentPos, planetPos);

            float accelerationX = acceleration * Mathf.Cos(angle);
            float accelerationY = acceleration * Mathf.Sin(angle);

            accelerationX = Mathf.Round(accelerationX * roundConstant) / roundConstant;
            accelerationY = Mathf.Round(accelerationY * roundConstant) / roundConstant;

            Vector3 force = new Vector2(accelerationX, accelerationY);

            rigidBody.AddForce(force, ForceMode2D.Impulse);
        }

        
    }

    private float CalculateAngle(Vector2 me, Vector2 target)
    {
        return Mathf.Atan2(target.y - me.y, target.x - me.x);
    }

    private float CalculateAcceleration(float mass, float distance)
    {
        if(distance == 0) { return 0;}
        return gravitationalConstant * (mass / Mathf.Pow(distance, distancePow));
    }
}
