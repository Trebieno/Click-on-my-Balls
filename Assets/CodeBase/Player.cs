using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _timer;
    [SerializeField] private float _silver;
    [SerializeField] private float _gold;
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private int _score;
    
    [SerializeField] private Healing _healing;

    public Healing Healing => _healing;
    
    public float Damage => _damage;

    public void ResourceModifying(float gold, float silver)
    {
        _gold += gold;
        _silver += silver;
    } 
    
    private string FormatTime(float time)
    {
        // Форматируем время в минуты:секунды:миллисекунды
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        // int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
    
    
