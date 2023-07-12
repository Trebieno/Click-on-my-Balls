using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Effect
{
    public Buffs Buff;
    public float CurrentTime;
    public float MaximumTime;

    public Effect(Buffs buff, float time)
    {
        Buff = buff;
        CurrentTime = time;
        MaximumTime = time;
    }
}

public class Player : MonoCache
{
    [SerializeField] private float _timer;
    [SerializeField] private float _silver;
    [SerializeField] private float _gold;
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private int _score;
    [SerializeField] private List<Effect> _effects = new List<Effect>();
    [SerializeField] private Healing _healing;
    private bool _goldX2;
    private bool _speedY2;
    
    public Healing Healing => _healing;
    public float Damage => _damage;
    public bool GoldX2 => _goldX2;
    public bool SpeedY2 => _speedY2;
    

    public Action MinutesChange;


    public void ResourceModifying(float gold, float silver)
    {
        if (_goldX2)
            _gold += (gold * 2);
        else
            _gold += gold;
        _silver += silver;
    }

    public void AddEffect(Effect effect)
    {
        _effects.Add(effect);

        switch (effect.Buff)
        {
            case Buffs.DamageX2:
                _damage *= 2;
                break;
            
            case Buffs.GoldX2:
                _goldX2 = true;
                break;
            
            case Buffs.SpeedY2:
                _speedY2 = true;
                var balls = BallAll.Instance.Balls.ToList();
                for (int i = 0; i < balls.Count; i++)
                {
                    balls[i].Value.BuffPlayer();
                }
                
                break;
            
            case Buffs.HP1:
                _healing.MaximumHealth += 1;
                _healing.CurrentHealth += 1;
                break;
            
            case Buffs.HP2:
                _healing.MaximumHealth += 2;
                _healing.CurrentHealth += 2;
                break;
            
            case Buffs.HP3:
                _healing.MaximumHealth += 3;
                _healing.CurrentHealth += 3;
                break;
        }
    }

    public void RemoveEffect(Effect effect)
    {
        switch (effect.Buff)
        {
            case Buffs.DamageX2:
                _damage /= 2;
                break;
            
            case Buffs.GoldX2:
                _goldX2 = false;
                break;
            
            case Buffs.SpeedY2:
                _speedY2 = false;
                var balls = BallAll.Instance.Balls.ToList();
                for (int i = 0; i < balls.Count; i++)
                {
                    balls[i].Value.RemoveBuffPlayer();
                }
                break;
            
            case Buffs.HP1:
                _healing.MaximumHealth += 1;
                _healing.CurrentHealth += 1;
                break;
            
            case Buffs.HP2:
                _healing.MaximumHealth += 2;
                _healing.CurrentHealth += 2;
                break;
            
            case Buffs.HP3:
                _healing.MaximumHealth += 3;
                _healing.CurrentHealth += 3;
                break;
        }
        
        _effects.Remove(effect);
    }

    public override void OnFixedUpdateTick()
    {
        _timer += Time.fixedDeltaTime;
        FormatTime(_timer);
        if(_effects.Count >0)
            for (int i = 0; i < _effects.Count; i++)
            {
                _effects[i].CurrentTime -= Time.fixedDeltaTime;
                if (_effects[i].CurrentTime <= 0)
                    RemoveEffect(_effects[i]);
            }
    }

    private int previousMinutes;
    private string FormatTime(float time)
    {
        // Форматируем время в минуты:секунды:миллисекунды
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        // int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);
        
        if(previousMinutes != minutes)
            MinutesChange?.Invoke();
        
        previousMinutes = minutes;

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
    
    
