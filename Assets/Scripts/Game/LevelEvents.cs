using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;


public class LevelEvents : MonoBehaviour
{
	public static LevelEvents instance {get; private set;}

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

    void Start()
    {

    }

    public event Action<GameObject> OnCollectStar;
    public void CollectStar(GameObject star)
    {
    	instance.OnCollectStar.Invoke(star);
    }

    public event Action OnResetLevel;
    public void ResetLevel()
    {
        if(!LevelManager.instance.isPlaying)
        {
            instance.OnResetLevel.Invoke();

            //TODO: Find better place for this
            SoundManager.instance.PlayEffect(SoundEffect.ResetSim);
        }
    }

    public event Action<bool> OnPlayChange;
    public void PlayChange()
    {
        instance.OnPlayChange.Invoke(!LevelManager.instance.isPlaying);
    }

    public event Action<bool> OnDragChange;
    public void DragChange(bool isDragging)
    {
        instance.OnDragChange.Invoke(isDragging);
    }

    public event Action<bool> OnEndLevel;
    public void EndLevel(bool isComplete)
    {
        instance.OnEndLevel.Invoke(isComplete);

        //TODO: Find better place for this
        if(!isComplete)
        {
            SoundManager.instance.PlayEffect(SoundEffect.Click);
        }
    }
}
