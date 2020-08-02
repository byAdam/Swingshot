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

    public event Action OnResetLevel;
    public void ResetLevel()
    {
        OnResetLevel.Invoke();
    }

    public event Action<bool> OnPlayChange;
    public void PlayChange()
    {
        OnPlayChange.Invoke(!LevelManager.instance.isPlaying);
    }
}
