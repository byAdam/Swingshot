using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance {get; private set;}

	public List<GameObject> planets = new List<GameObject>();
    public List<GameObject> stars = new List<GameObject>();

    void Awake()
    { 
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
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

        GameEvents.instance.OnCollectStar += OnCollectStar;
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    public void PlacePlanet(Vector3 pos)
    {

    }

    void AddPlanet(GameObject planet)
    {
    	if(!planets.Contains(planet))
    	{
    		planets.Add(planet);
    	}
    }

    void RemovePlanet(GameObject planet)
    {
    	if(planets.Contains(planet))
    	{
    		planets.Remove(planet);
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

        if(stars.Count == 0)
        {
            Debug.Log("win");
        }
    }
}
