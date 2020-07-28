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
	public bool isMoveable = false;
    public bool isAnti = false;
	public float mass = 0;
    public float scale = 0;

    public Sprite normalSprite;
    public Sprite antiSprite;

    private SpriteRenderer spriteRenderer;

    private List<MassLevel> massData = new List<MassLevel>()
    {
        new MassLevel(100, 1),
        new MassLevel(200, 1.5f),
        new MassLevel(300, 2),
    };
    private int massIndex = 0;

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSize()
    {
        massIndex++;
        if(massIndex == massData.Count)
        {
            massIndex = 0;
        }

        mass = massData[massIndex].mass;
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

    public void UpdatePosition(Vector3 pos)
    {
        gameObject.transform.position = pos;
    }
}
