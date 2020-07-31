using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       GameEvents.instance.OnCollectStar += OnCollect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollect(GameObject star)
    {
    	if(gameObject != null && star.GetInstanceID() == gameObject.GetInstanceID())
    	{
    		Destroy(gameObject);
    	}
    }

    void OnDestroy()
    {
    	GameEvents.instance.OnCollectStar -= OnCollect;
    }
}
