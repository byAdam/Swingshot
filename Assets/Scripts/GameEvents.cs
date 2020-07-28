using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GameEvents : MonoBehaviour
{
	public static GameEvents instance {get; private set;}

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

    public event Action<GameObject> OnCollectStar;

    public void CollectStar(GameObject star)
    {
    	OnCollectStar.Invoke(star);
    }

}
