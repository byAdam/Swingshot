using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEvents : MonoBehaviour
{

    public static UIEvents instance {get; private set;}

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


    public GameObject stateButton;
    public GameObject resetButton;

    // Start is called before the first frame update
    void Start()
    {
        stateButton.GetComponent<Button>().onClick.AddListener(PlayChange); 
        resetButton.GetComponent<Button>().onClick.AddListener(Reset); 
    }

    void PlayChange()
    {   
        GameEvents.instance.PlayChange();
    }

    void Reset()
    {
        if(!LevelManager.instance.isPlaying)
        {
           GameEvents.instance.ResetLevel(); 
        }
    }
}
