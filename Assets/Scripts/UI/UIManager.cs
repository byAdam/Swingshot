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

        LevelEvents.instance.OnPlayChange += OnPlayChange;
    }

    void OnDestroy()
    {
        LevelEvents.instance.OnPlayChange -= OnPlayChange;
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
