using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{       
    private Rigidbody2D rigidBody;
    public float velocity;

    void Awake()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody.velocity = new Vector3(velocity, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameObject.transform.position.x >= 10.24)
        {
            gameObject.transform.position = new Vector3(0, 0, 1);
        }
    }
}
