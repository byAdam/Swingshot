using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassLevel
{
    public int mass = 0;
    public float scale = 0;
    public float spin = 0;

    public MassLevel(int mass, float scale, float spin)
    {
        this.mass = mass;
        this.scale = scale;
        this.spin = spin;
    }

}

public class PlanetController : MonoBehaviour
{
    public bool isAnti = false;
	public float mass = 0;
    public float scale = 1;

    public Sprite normalSprite;
    public Sprite antiSprite;

    private SpinController spinController;
    private SpriteRenderer spriteRenderer;
    private float radius;

    private List<MassLevel> massData = new List<MassLevel>()
    {
        new MassLevel(100, 1, 0.2f),
        new MassLevel(300, 1.5f, 0.07f),
        new MassLevel(900, 2, 0.04f),
    };
    private int massIndex = -1;

    // Reset variables
    private Vector3 startPosition;

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spinController = gameObject.GetComponent<SpinController>();
        startPosition = gameObject.transform.position;
    }

    void Start()
    {
        GameEvents.instance.OnResetLevel += OnReset;

        ChangeSize();
    }

    public void ChangeSize()
    {
        int tmpMassIndex = massIndex;
        tmpMassIndex++;
        if(tmpMassIndex == massData.Count)
        {
            tmpMassIndex = 0;
        }

        if(!WillCollideWithAny(null, massData[tmpMassIndex].scale))
        {
            massIndex = tmpMassIndex;

            mass = massData[massIndex].mass;

            if(isAnti)
            {
                mass = -mass;
            }

            scale = massData[massIndex].scale;

            UpdateScale();
            UpdateSpin();
        }
    }

    public void UpdateSpin()
    {
        spinController.rps = massData[massIndex].spin;

        if(isAnti)
        {
            spinController.rps *= -1;
        }
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

        UpdateSpin();
    }

    public bool WillCollide(Vector3 posA, float scaleA, GameObject objB)
    {
        Vector3 posB = objB.transform.position;

        float distance = Vector3.Distance(posA, posB);

        // Collision Radius is scale * 1.3
        float radii = scaleA * 1.3f + objB.GetComponent<CircleCollider2D>().radius;

        return distance < radii;
    }

    public bool WillCollideWithAny(Vector3? posArg = null, float? scaleArg = null)
    {
        Vector3 newPos = posArg ?? transform.position;
        float newScale = scaleArg ?? scale;

        // Check if too close to rocket
        if((Vector3.Distance(newPos,LevelManager.instance.rocket.transform.position) - newScale * 1.8f) < 1)
        {
            return true;
        }

        foreach(GameObject planet in LevelManager.instance.planets)
        {
            if(planet.GetInstanceID() != gameObject.GetInstanceID())
            {
                if(WillCollide(newPos, newScale, planet))
                {
                    return true;
                }
            }
        }

        foreach(GameObject star in LevelManager.instance.stars)
        {
            if(WillCollide(newPos, newScale, star))
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
