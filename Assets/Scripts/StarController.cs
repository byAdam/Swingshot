using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       GameEvents.instance.OnCollectStar += OnCollect;
       GameEvents.instance.OnPlayChange += OnReset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollect(GameObject star)
    {
    	if(gameObject != null && star.GetInstanceID() == gameObject.GetInstanceID())
    	{
    		gameObject.SetActive(false);
    	}
    }

    void OnReset(bool isPlaying)
    {
        if(!isPlaying)
        {
            gameObject.SetActive(true);
        }
    }

    void OnDestroy()
    {
    	GameEvents.instance.OnCollectStar -= OnCollect;
    }
}
