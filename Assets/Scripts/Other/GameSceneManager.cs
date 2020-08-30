using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MenuButton
{
    Play,
    About,
    Settings
}

public class GameSceneManager : MonoBehaviour
{
	public static GameSceneManager instance {get; private set;}
    Stack<string> sceneStack = new Stack<string>();
    string currentScene;

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

    void Start()
    {
        string activeScene = SceneManager.GetActiveScene().name;

        if(activeScene == "Init")
        {
            LoadScene("Menu");
        }

        MenuEvents.instance.OnSelectLevel += OnSelectLevel;
        MenuEvents.instance.OnClickMenuButton += OnClickMenuButton;
        MenuEvents.instance.OnClickBack += OnClickBack;
        MenuEvents.instance.OnClickEditor += OnClickEditor;
        LevelEvents.instance.OnEndLevel += OnEndLevel;
    }

    void OnClickEditor()
    {
        LoadScene("Playground");
    }

    void OnSelectLevel(int levelNo)
    {
        var sceneName = $"LVL{levelNo}";
    	LoadScene(sceneName);

        SoundManager.instance.PlayEffect(SoundEffect.Level);
    }

    void LoadScene(string sceneName, bool addToStack = true)
    {
        SceneManager.LoadScene(sceneName);
        if(addToStack)
        {
            sceneStack.Push(currentScene);
        }
        currentScene = sceneName;
    }

    void OnClickMenuButton(MenuButton buttonType)
    {
        switch(buttonType)
        {
            case MenuButton.Play:
                LoadScene("Level Select");
                break;
            case MenuButton.About:
                LoadScene("About");
                break;
            case MenuButton.Settings:
                LoadScene("Settings");
                break;
        }

        SoundManager.instance.PlayEffect(SoundEffect.Click);
        
    }

    void OnClickBack()
    {
        LoadScene("Menu");
        SoundManager.instance.PlayEffect(SoundEffect.Click);
    }

    void OnEndLevel(bool isComplete)
    {
        LoadScene("Level Select");
    }
}
