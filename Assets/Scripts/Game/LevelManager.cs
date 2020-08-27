using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance {get; private set;}

	public List<GameObject> planets {get; private set;} = new List<GameObject>();
    public List<GameObject> stars {get; private set;} = new List<GameObject>();

    public bool isPlaying = false;
    public GameObject rocket {get; private set;}
    private GameObject grid;
    private Vector3 rocketStartPosition;

    void Awake()
    { 
        if(instance != null)
        {
            Destroy(instance);
        }
        
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LevelEvents.instance.OnCollectStar += OnCollectStar;        
        LevelEvents.instance.OnPlayChange += OnPlayChange;
        LevelEvents.instance.OnDragChange += OnDragChange;

        OnLoad();
    }

    void OnDestroy()
    {
        LevelEvents.instance.OnCollectStar -= OnCollectStar;        
        LevelEvents.instance.OnPlayChange -= OnPlayChange;
        LevelEvents.instance.OnDragChange -= OnDragChange;
    }

    void OnLoad()
    {

        rocket = GameObject.Find("Rocket");
        grid = GameObject.Find("Background Grid");


        rocketStartPosition = rocket.transform.position;

        planets.Clear();
        stars.Clear();

        PlanetController[] pObjects = GameObject.FindObjectsOfType(typeof(PlanetController)) as PlanetController[];
        foreach(PlanetController planet in pObjects)
        {
            AddPlanet(planet.gameObject);
        }

        StarController[] sObjects = GameObject.FindObjectsOfType(typeof(StarController)) as StarController[];
        foreach(StarController star in sObjects)
        {
            AddStar(star.gameObject);
        }
    }


    void AddPlanet(GameObject planet)
    {
    	if(!planets.Contains(planet))
    	{
    		planets.Add(planet);
    	}
    }

    void AddStar(GameObject star)
    {
        if(!stars.Contains(star))
        {
            stars.Add(star);
        }
    }

    void RemoveStar(GameObject star)
    {
        if(stars.Contains(star))
        {
            stars.Remove(star);
        }
    }

    void OnCollectStar(GameObject star)
    {
        RemoveStar(star);
        SoundManager.instance.PlayEffect(SoundEffect.CollectStar);

        if(stars.Count == 0)
        {
            SoundManager.instance.PlayEffect(SoundEffect.WinGame);
            LevelEvents.instance.EndLevel(true);
        }
    }

    void OnPlayChange(bool isPlaying)
    {
        this.isPlaying = isPlaying;

        if(this.isPlaying)
        {
            OnPlayStart();
        }
        else
        {
            OnPlayEnd();
        }
    }

    void OnPlayEnd()
    {
        SoundManager.instance.PlayEffect(SoundEffect.Crash);
    }

    void OnPlayStart()
    {
        SoundManager.instance.PlayEffect(SoundEffect.StartSim);
        rocket.transform.position = rocketStartPosition;
    }

    void OnDragChange(bool isDragging)
    {
        grid.GetComponent<SpriteRenderer>().enabled = isDragging;
    }
}
