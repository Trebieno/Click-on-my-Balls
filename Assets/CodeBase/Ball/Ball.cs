using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;


public enum Buffs
{
    DamageX2,
    SpeedY2,
    GoldX2,
    HP1,
    HP2,
    HP3
}


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

    [Range(0, 100)] [SerializeField] private float _buffPercent = 10;
    [SerializeField] private Buffs _buff;
    private Specifications _specifications;

    private float randomPercentage 
    {
        get { return Random.Range(0.5f, 1.5f); }
    }

    private void Start()
    {
        _healing.Murdered += Murdered;
        _healing.Died += Death;
        
        if(!BallAll.Instance.Balls.ContainsKey(gameObject))
            BallAll.Instance.Balls.Add(gameObject, this);
    }

    public void StartBall(float level)
    {
        var buffs = Enum.GetValues(typeof(Buffs));
        int randomBuffIndex = Random.Range(0, buffs.Length - 1);
        _buff = (Buffs)buffs.GetValue(randomBuffIndex);

        _specifications = new Specifications(_level, _healing.MaximumHealth, _silver, _gold, _speed, _damage);
        Initialize(level);

    }
    
    public void Initialize(float level)
    {
        _level = level;
        _healing.MaximumHealth = _specifications.Health * _level * randomPercentage;
        _silver = _specifications.Silver * _level * randomPercentage;
        _gold = _specifications.Gold * _level * randomPercentage;
        _speed = _specifications.Speed * _level * randomPercentage;
        _damage = _specifications.Damage;

        _healing.CurrentHealth = _healing.MaximumHealth;
    }


    private void OnDestroy()
    {
        _healing.Murdered -= Murdered;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KillZone"))
        {
            _healing.Death();
            PlayerCache.Instance.Player.Healing.SetDamage(_damage, transform.position);
        }
        if (other.CompareTag("bullet"))
        {
            Destroy(other.gameObject);
            _healing.SetDamage(PlayerCache.Instance.Player.Damage, other.transform.position);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _healing.SetDamage(PlayerCache.Instance.Player.Damage, transform.position);
    }

    public void BuffPlayer()
    {
        if (PlayerCache.Instance.Player.SpeedY2)
            _speed /= 2;
    }

    public void RemoveBuffPlayer()
    {
        if (!PlayerCache.Instance.Player.SpeedY2)
            _speed *= 2;
    }
    
    private void Murdered()
    {
        float random = Random.Range(0, 1);
        if (random <= _buffPercent/100)
        {
            Effect effect = new Effect(_buff, 15);
            PlayerCache.Instance.Player.AddEffect(effect);
        }
        PlayerCache.Instance.Player.ResourceModifying(_gold, _silver);
    }
    
    private void Death()
    {
          if(gameObject.CompareTag("Boss"))
            Destroy(gameObject);
        else
            SpawnerCache.Instance.Spawner.BallsFree.Add(this);
    }
}
