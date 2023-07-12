using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Spawner : MonoCache
{
    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;

    [SerializeField] private List<Ball> _ballsPref;
    [SerializeField] private List<Ball> _ballsFree;
    
    [SerializeField] private Transform _canvas;
    [SerializeField] private float _maxTimeSpawn = 1;
    [SerializeField] private int _startBall;
    [SerializeField] private float _modifyLvl = 0.5f;
    private float _level = 1;
    private float _timeSpawn = 1;

    private float _randomY
    {
        get { return Random.Range(_point1.position.y, _point2.position.y); }
    }
    public List<Ball> BallsFree => _ballsFree;

    private void Start()
    {
        PlayerCache.Instance.Player.MinutesChange += LvlUp;
        _maxTimeSpawn = _timeSpawn;
        for (int i = 0; i < _startBall; i++)
        {
            var ball = SpawnPoint(_ballsPref[0]);
            ball.Healing.Death();
        }
    }

    private void OnDestroy()
    {
        PlayerCache.Instance.Player.MinutesChange -= LvlUp;
    }

    public override void OnFixedUpdateTick()
    {
        if (_timeSpawn <= 0)
        {
            if (_ballsFree.Count <= 0)
                SpawnPoint(_ballsPref[0]);
            else
                RespawnPoint(_ballsFree[0]);

            _timeSpawn = _maxTimeSpawn;
        }
        else
            _timeSpawn -= Time.fixedDeltaTime;
    }

    private Ball SpawnPoint(Ball ball)
    {
        Vector3 scale = ball.transform.localScale;
        float offsetX = GetOffsetX(ball.transform.localScale);

        ball = Instantiate(ball, new Vector3(offsetX, _randomY, 0), Quaternion.identity);
        ball.transform.parent = _canvas;
        ball.transform.localScale = scale;
        
        return ball;
    }
    
    private void RespawnPoint(Ball ball)
    {
        float offsetX = GetOffsetX(ball.transform.localScale);
        ball.StartBall(_level);
        ball.transform.position = new Vector3(offsetX, _randomY, 0);
        ball.gameObject.SetActive(true);
        _ballsFree.Remove(ball);
    }

    private void SpawnBoss(Ball ball)
    {
        ball = SpawnPoint(ball);
        ball.transform.position = new Vector3(ball.transform.position.x, 0, 0);
    }

    private float GetOffsetX(Vector3 scale)
    {
        float objectRadius = Mathf.Max(scale.x, scale.y, scale.z) / 2f;
        float offsetX = objectRadius + scale.x * objectRadius;
        return offsetX;
    }

    private void LvlUp()
    {
        _level += _modifyLvl;
        SpawnBoss(_ballsPref[1]);
    }
}
