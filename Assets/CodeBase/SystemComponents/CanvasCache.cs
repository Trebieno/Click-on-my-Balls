using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCache : MonoBehaviour
{
    public static CanvasCache Instance { get; private set; }

    public Transform Canvas;

    // private void OnEnable()
    // {
    //     Canvas = GameObject.Find("MainCanvas").transform;
    // }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
