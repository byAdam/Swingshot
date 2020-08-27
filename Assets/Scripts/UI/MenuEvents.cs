using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class MenuEvents : MonoBehaviour
{

	public static MenuEvents instance {get; private set;}

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public event Action<int> OnSelectLevel;
    public void SelectLevel(int levelNo)
    {
    	instance.OnSelectLevel.Invoke(levelNo);
    }

    public event Action<MenuButton> OnClickMenuButton;
    public void ClickMenuButton(int buttonType)
    {
        
        // 0 = play, 1= about, 2 = settings
        instance.OnClickMenuButton.Invoke((MenuButton)buttonType);
    }

    public event Action OnClickBack;
    public void ClickBack()
    {
        instance.OnClickBack.Invoke();
    }
}
