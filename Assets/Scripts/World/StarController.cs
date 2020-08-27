using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    public GameObject destoryParticle;

    private ParticleSystem destoryParticleSystem;


    // Start is called before the first frame update
    void Start()
    {
       LevelEvents.instance.OnCollectStar += OnCollect;
       LevelEvents.instance.OnPlayChange += OnReset;

       destoryParticleSystem = destoryParticle.GetComponent<ParticleSystem>();
    }

    void OnDestroy()
    {
        LevelEvents.instance.OnCollectStar -= OnCollect;
        LevelEvents.instance.OnPlayChange -= OnReset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollect(GameObject star)
    {
    	if(gameObject != null && star.GetInstanceID() == gameObject.GetInstanceID())
    	{
            destoryParticleSystem.transform.position = gameObject.transform.position;
            destoryParticleSystem.Play();

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
}
