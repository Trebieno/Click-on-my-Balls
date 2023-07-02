using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoCache
{
    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;

    [SerializeField] private List<Ball> _ballsPref;
    [SerializeField] private List<Ball> _ballsFree;
    

    [SerializeField] private Transform _canvas;
    [SerializeField] private float _timeSpawn = 1;
    [SerializeField] private int _startBall;
    private float _maxTimeSpawn;
    private float _level = 1;

    private float _randomY
    {
        get { return Random.Range(_point1.position.y, _point2.position.y); }
    }

    private void Start()
    {
        _maxTimeSpawn = _timeSpawn;
        for (int i = 0; i < _startBall; i++)
        {
            SpawnPoint(_ballsPref[0]);
        }
    }

    public override void OnFixedUpdateTick()
    {
        if (_timeSpawn <= 0)
        {
            if(_ballsFree.Count <= 0)
                SpawnPoint(_ballsPref[0]);
            else
                RespawnPoint(_ballsFree[0]);
            _timeSpawn = _maxTimeSpawn;
        }
    }

    private void SpawnPoint(Ball ball)
    {
        float offsetX = GetOffsetX(ball.transform.localScale);

        ball = Instantiate(ball, new Vector2(offsetX, _randomY), Quaternion.identity);
    }
    
    private void RespawnPoint(Ball ball)
    {
        float offsetX = GetOffsetX(ball.transform.localScale);
        
        ball.transform.position = new Vector2(offsetX, _randomY);
        ball.gameObject.SetActive(true);
        ball.Initialize(_level);

        _ballsFree.Remove(ball);
    }

    private void SpawnBoss(Ball ball)
    {
        
    }

    private float GetOffsetX(Vector3 scale)
    {
        float objectRadius = Mathf.Max(scale.x, scale.y, scale.z) / 2f;
        float offsetX = objectRadius + scale.x * objectRadius;
        return offsetX;
    }
}
