using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
	public static SettingsManager instance {get; private set;}
    
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
    	if(PlayerPrefs.HasKey("latestLevel"))
    	{
    		GameManager.instance.latestLevel = PlayerPrefs.GetInt("latestLevel");
    	}
    }

    public void OnUnlockLevel(int latestLevel)
    {
    	PlayerPrefs.SetInt("latestLevel", latestLevel);
    }
}
