using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject stateButton;
    private Image stateImage;

    public Sprite playButton;
    public Sprite stopButton;

    // Start is called before the first frame update
    void Start()
    {
        stateImage = stateButton.GetComponent<Image>();

        GameEvents.instance.OnPlayChange += OnPlayChange;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPlayChange(bool isPlaying)
    {
        if(isPlaying)
        {
            stateImage.sprite = stopButton;
        }
        else
        {
            stateImage.sprite = playButton;
        }
    }
}
