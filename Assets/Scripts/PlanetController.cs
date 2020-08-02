using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassLevel
{
    public int mass = 0;
    public float scale = 0;

    public MassLevel(int mass, float scale)
    {
        this.mass = mass;
        this.scale = scale;
    }

}

public class PlanetController : MonoBehaviour
{
    public bool isAnti = false;
	public float mass = 0;
    public float scale = 1;

    public Sprite normalSprite;
    public Sprite antiSprite;

    private SpriteRenderer spriteRenderer;
    private float radius;

    private List<MassLevel> massData = new List<MassLevel>()
    {
        new MassLevel(100, 1),
        new MassLevel(300, 1.5f),
        new MassLevel(900, 2),
    };
    private int massIndex = -1;

    // Reset variables
    private Vector3 startPosition;

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        startPosition = gameObject.transform.position;
    }

    void Start()
    {
        GameEvents.instance.OnResetLevel += OnReset;

        ChangeSize();
    }

    public void ChangeSize()
    {
        massIndex++;
        if(massIndex == massData.Count)
        {
            massIndex = 0;
        }

        mass = massData[massIndex].mass;

        if(isAnti)
        {
            mass = -mass;
        }

        scale = massData[massIndex].scale;

        UpdateScale();
    }

    public void UpdateScale()
    {
        transform.localScale = new Vector3(scale, scale, 1);
    }

    public void SwapGravity()
    {
        isAnti = !isAnti;
        mass = -mass;

        if(isAnti)
        {
            spriteRenderer.sprite = antiSprite;
        }
        else
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    public bool WillCollide(Vector3 posA, GameObject objB)
    {
        Vector3 posB = objB.transform.position;

        float distance = Vector3.Distance(posA, posB);

        // Collision Radius is scale * 0.8
        float radii = scale * 0.8f + objB.GetComponent<CircleCollider2D>().radius;

        return distance < radii;
    }

    public bool WillCollideWithAny(Vector3 newPos)
    {
        // Check if too close to rocket
        if((Vector3.Distance(newPos,LevelManager.instance.rocket.transform.position) - scale * 0.8f) < 1)
        {
            return true;
        }

        foreach(GameObject planet in LevelManager.instance.planets)
        {
            if(planet.GetInstanceID() != gameObject.GetInstanceID())
            {
                if(WillCollide(newPos, planet))
                {
                    return true;
                }
            }
        }

        foreach(GameObject star in LevelManager.instance.stars)
        {
            if(WillCollide(newPos, star))
            {
                return true;
            }
        }

        return false;
    }

    public void OnReset()
    {
        gameObject.transform.position = startPosition;

        // Reset size
        massIndex = -1;
        ChangeSize();

        // Reset gravityness
        if(isAnti)
        {
            SwapGravity(); 
        }
    }

}
