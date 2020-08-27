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
    public bool isFixed = false;
    public bool isMassless = false;
	public float mass{get; private set;} = 100;
    public float scale{get; private set;} = 1;

    public Sprite normalSprite;
    public Sprite antiSprite;

    private SpinController spinController;
    private SpriteRenderer spriteRenderer;
    private float radius;

    private List<MassLevel> massData = new List<MassLevel>()
    {
        new MassLevel(100, 1, 0.2f),
        new MassLevel(300, 1.4f, 0.07f),
        new MassLevel(900, 2.5f, 0.04f),
    };

    public int startMassIndex = 0;
    private int massIndex;

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
        massIndex = startMassIndex - 1;
        ChangeSize(false);
        UpdateSprite();

        if(!isFixed)
        {
            LevelEvents.instance.OnResetLevel += OnReset;
        }
    }

    void OnDestroy()
    {
        if(!isFixed)
        {
            LevelEvents.instance.OnResetLevel -= OnReset;
        }
    }

    public void ChangeSize(bool isClick = true)
    {
        int tmpMassIndex = massIndex;
        tmpMassIndex++;
        if(tmpMassIndex == massData.Count)
        {
            tmpMassIndex = 0;
        }

        //Massless
        if(isMassless)
        {
            mass = 0;
            scale = 1;
            return;
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

            if(!isFixed)
            {
                UpdateSpin();
            }
        }
        else if(massIndex != 0)
        {
            massIndex++;
            ChangeSize();
        }

        if(isClick)
        {
            SoundManager.instance.PlayEffect(SoundEffect.ChangeSize);  
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

        UpdateSprite();
        UpdateSpin();

        SoundManager.instance.PlayEffect(SoundEffect.SwapGravity);
    }

    void UpdateSprite()
    {
        if(isAnti)
        {
            spriteRenderer.sprite = antiSprite;
        }
        else
        {
            spriteRenderer.sprite = normalSprite;
        }
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

        if(WillCollideWithBorder(newPos, newScale))
        {
            return true;
        }

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

    public bool WillCollideWithBorder(Vector3? posArg = null, float? scaleArg = null)
    {
        Vector3 newPos = posArg ?? transform.position;
        float newScale = scaleArg ?? scale;

        return (Mathf.Abs(newPos.x) + newScale * 1.3f) > 14 || (Mathf.Abs(newPos.y) + newScale * 1.3f) > 8;
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
