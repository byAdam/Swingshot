using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaygroundManager : MonoBehaviour
{
    public static PlaygroundManager instance {get; private set;}
    public GameObject planetPrefab;

    private List<GameObject> planets;

    void Awake()
    { 
        if(instance != null)
        {
            Destroy(instance);
        }
        
        instance = this;
    }

    void Start()
    {
        LevelEvents.instance.OnPlaygroundPlanet += OnPlanetChange;

        planets = LevelManager.instance.planets;
    }

    void OnDestroy()
    {
        LevelEvents.instance.OnPlaygroundPlanet -= OnPlanetChange;
    }

    Vector3 FindAnyFreeSpace(PlanetController planetController)
    {
        int randX = Random.Range(-6,6)*2;
        int randY = Random.Range(-3,3)*2;
        int i = 0;

        while(planetController.WillCollideWithAny(new Vector3(randX, randY)) && i < 72)
        {
            randX = Random.Range(-6,6)*2;
            randY = Random.Range(-3,3)*2;
            i++;
        }

        return new Vector3(randX, randY, 0);
    }

    void OnPlanetChange(bool isAdd)
    {
        GameObject planet;

        if(isAdd)
        {
            planet = Instantiate(planetPrefab);
            planet.transform.SetParent(GameObject.Find("Planets").transform);
            planet.transform.position = FindAnyFreeSpace(planet.GetComponent<PlanetController>());
            LevelManager.instance.AddPlanet(planet);
        }

        else
        {  
            if(planets.Count >= 1)
            {
                Destroy(planets[0]);
                planets.RemoveAt(0);
            }
        }
        
    }
}
