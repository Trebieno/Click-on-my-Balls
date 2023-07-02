using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAll : MonoBehaviour
{
    public static BallAll Instance { get; private set; }

    [SerializeField] private Dictionary<GameObject, Ball> _balls = new Dictionary<GameObject, Ball>();
    public Dictionary<GameObject, Ball> Balls => _balls;

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
