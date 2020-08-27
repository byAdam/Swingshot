using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtonController : MonoBehaviour
{
	public int levelNo;  
	public GameObject levelText;
    public bool isUnlocked = false;

    public GameObject textObject;
    public GameObject lockObject;

    // Start is called before the first frame update
    void Start()
    {
        levelText.GetComponent<TextMeshProUGUI>().SetText(levelNo.ToString());

        if(levelNo <= GameManager.instance.latestLevel)
        {
            Unlock();
        }
    }

    void Unlock()
    {
        isUnlocked = true;
        textObject.SetActive(true);
        lockObject.SetActive(false);
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void OnClick()
    {
        if(isUnlocked)
        {
            MenuEvents.instance.SelectLevel(levelNo);
        }
    }
}
