using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}
    public int latestLevel = 1;
    public int selectedLevel = 0;

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


    // Start is called before the first frame update
    void Start()
    {
        MenuEvents.instance.OnSelectLevel += OnSelectLevel;
        LevelEvents.instance.OnEndLevel += OnEndLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSelectLevel(int levelNo)
    {
        selectedLevel = levelNo;
    }

    void OnEndLevel(bool isComplete)
    {
        if(isComplete && selectedLevel >= latestLevel)
        {
            latestLevel = selectedLevel + 1;
            SettingsManager.instance.OnUnlockLevel(latestLevel);
        }
        
        OnLevelEnd();
    }

    void OnLevelEnd()
    {
        selectedLevel = 0;
    }
}
