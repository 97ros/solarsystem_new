using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FPSLimiter : MonoBehaviour
{
    public int targetFPS = 90;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;
    }

}
