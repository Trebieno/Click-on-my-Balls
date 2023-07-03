using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;


public struct Specifications
{
    public float Level;
    public float Health;
    public float Silver;
    public float Gold;
    public float Speed;
    public float Damage;
    
    public Specifications(float level, float health, float silver, float gold, float speed, float damage)
    {
        Level = level;
        Health = health;
        Silver = silver;
        Gold = gold;
        Speed = speed;
        Damage = damage;
    }
}

public class Ball : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Healing _healing;
    [SerializeField] private Movement _movement;

    public Healing Healing => _healing;
    public Movement Movement => _movement;

    [SerializeField] private float _level;
    [SerializeField] private float _silver;
    [SerializeField] private float _gold;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    private Specifications _specifications;

    private float randomPercentage 
    {
        get { return Random.Range(0.5f, 1.5f); }
    }
    private void Start()
    {
        _specifications = new Specifications(_level, _healing.MaximumHealth, _silver, _gold, _speed, _damage);
        Initialize(_level);
        BallAll.Instance.Balls.Add(gameObject, this);
        _healing.Murdered += TakeResourse;
    }
    
    public void Initialize(float level)
    {
        _level = level;
        _healing.MaximumHealth = _specifications.Health * _level * randomPercentage;
        _silver = _specifications.Silver * _level * randomPercentage;
        _gold = _specifications.Gold * _level * randomPercentage;
        _speed = _specifications.Speed * _level * randomPercentage;
        _damage = _specifications.Damage * _level * randomPercentage;

        _healing.Heal(_healing.MaximumHealth);
    }

    private void OnDisable()
    {
        SpawnerCache.Instance.Spawner.BallsFree.Add(this);
    }

    private void OnDestroy()
    {
        _healing.Murdered -= TakeResourse;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            _healing.Death();
            PlayerCache.Instance.Player.Healing.SetDamage(_damage, transform.position);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _healing.SetDamage(PlayerCache.Instance.Player.Damage, transform.position);
    }

    private void TakeResourse()
    {
        PlayerCache.Instance.Player.ResourceModifying(_gold, _silver);
    }
    
}
