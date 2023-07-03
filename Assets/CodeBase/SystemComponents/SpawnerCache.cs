using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCache : MonoBehaviour
{
    public static SpawnerCache Instance { get; private set; }

    
    public Spawner Spawner;
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
