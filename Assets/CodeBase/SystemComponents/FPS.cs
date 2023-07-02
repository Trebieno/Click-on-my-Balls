using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoCache
{
    [SerializeField] private int _maxFps;
    private void Start()
    {
        Application.targetFrameRate = _maxFps;
    }

    private void OnValidate()
    {
        Application.targetFrameRate = _maxFps;
    }
}
