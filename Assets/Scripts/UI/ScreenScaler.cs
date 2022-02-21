using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScaler : MonoBehaviour
{
    public float AspectRatio;
    public float tolerance = 0.01f;
    private const float MinAspect = 9f / 16f;
    private const float MinScale = 1f;
    private const float FixedScreenMultiplier = 0.5f;

    void Awake()
    {
        UpdateImageScale();
    }

    void Update()
    {
        if (Math.Abs(Camera.main.aspect - AspectRatio) > tolerance)
        {

            UpdateImageScale();
        }
    }

    void UpdateImageScale()
    {
        AspectRatio = Camera.main.aspect;
        if (AspectRatio > MinAspect)
        {

            gameObject.transform.localScale = new Vector3(MinScale, MinScale, 1f);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(AspectRatio / FixedScreenMultiplier, AspectRatio / FixedScreenMultiplier, 1f);
        }
    }
}
