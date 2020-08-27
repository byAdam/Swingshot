using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffect
{
	Click,
	Level,
	StartSim,
	ResetSim,
	StartMove,
	BadMove,
	GoodMove,
	ChangeSize,
	SwapGravity,
	CollectStar,
	WinGame,
	Crash
}

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance {get; private set;}
	private AudioSource audioSource;
	private Dictionary<SoundEffect, AudioClip> effectMap;
	

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

	    DontDestroyOnLoad(gameObject);
    }

    AudioClip GetClip(string path)
    {
    	return Resources.Load<AudioClip>(path);
    }

    void Start()
    {
	    audioSource = gameObject.GetComponent<AudioSource>();
	    
	    effectMap = new Dictionary<SoundEffect, AudioClip>
		{
		    [SoundEffect.Click] = GetClip("FX/UI/Click"),
		    [SoundEffect.Level] = GetClip("FX/UI/Level"),
		    [SoundEffect.StartSim] = GetClip("FX/Game/StartSim"),
		    [SoundEffect.ResetSim] = GetClip("FX/Game/ResetSim"),
		    [SoundEffect.StartMove] = GetClip(""),
		    [SoundEffect.BadMove] = GetClip("FX/Planet/BadMove"),
		    [SoundEffect.GoodMove] = GetClip("FX/Planet/GoodMove"),
		    [SoundEffect.ChangeSize] = GetClip("FX/Planet/ChangeSize"),
		    [SoundEffect.SwapGravity] = GetClip("FX/Planet/SwapGravity"),
		    [SoundEffect.CollectStar] = GetClip("FX/Game/CollectStar"),
		    [SoundEffect.WinGame] = GetClip("FX/Game/WinGame"),
		    [SoundEffect.Crash] = GetClip("FX/Game/Crash"),
		};
    }

    public void PlayEffect(SoundEffect soundEffect)
    {
    	audioSource.PlayOneShot(effectMap[soundEffect], 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
