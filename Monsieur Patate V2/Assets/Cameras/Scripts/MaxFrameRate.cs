using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxFrameRate : MonoBehaviour
{
    public int maxFrameRate = 60;
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = maxFrameRate;
    }
}
