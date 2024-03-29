﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;

    // Start is called before the first frame update
    void Awake()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateOrthographicSize();
    }

    void UpdateOrthographicSize()
    {
        float ratio = cam.aspect;

        if(ratio < 1.6)
        {
            cam.orthographicSize = 8.3f * 1.6f / ratio;
        }
        else
        {
            cam.orthographicSize = 8.3f;
        }
        
    }
}
