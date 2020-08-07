using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinController : MonoBehaviour
{
    public float rps = 1;
    
    private float dps;

    // Update is called once per frame
    void Update()
    {
        dps = rps * 360;
        float currentRot = transform.eulerAngles.z;
        float changeRot = Time.deltaTime * dps;
        transform.eulerAngles = new Vector3(0, 0, currentRot + changeRot);
    }
}
