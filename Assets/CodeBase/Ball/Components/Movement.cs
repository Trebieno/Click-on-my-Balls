using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoCache
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Vector2 _movement;
    [SerializeField] private float _speed;

    public override void OnFixedUpdateTick()
    {
        _rigidbody2D.velocity = _movement * _speed;
    }
}
