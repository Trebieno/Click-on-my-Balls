using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Turret : MonoCache
{
    [SerializeField] private RectTransform _target;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _actionRadius = 10f;
    [SerializeField] private float _fireDelay = 1f;
    [SerializeField] private float _forceBullet = 5f;
    [SerializeField] private LayerMask _actionMask;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private Transform _canvas;
    
    private float fireTimer;

    private void Start()
    {
        _canvas = CanvasCache.Instance.Canvas;
    }

    public override void OnUpdateTick()
    {
        if (_target == null || !_target.gameObject.activeSelf)
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, _actionRadius, _actionMask);
            if (colliders.Length > 0)
                _target = colliders[0].GetComponent<RectTransform>();
        }
        
        if (_target != null)
        {
            Vector2 direction = _target.position - transform.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

            float angleDifference = Mathf.DeltaAngle(_rigidbody2D.rotation, angle);

            float rotationAmount = Mathf.Clamp(angleDifference, -_rotationSpeed, _rotationSpeed);

            _rigidbody2D.MoveRotation(_rigidbody2D.rotation + rotationAmount);
        }
        
        if (_target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, _target.position);
            if (distanceToTarget <= _actionRadius)
            {
                if (fireTimer <= 0f)
                {
                    Fire();

                    fireTimer = _fireDelay;
                }
                else
                {
                    fireTimer -= Time.deltaTime;
                }
            }
        }
    }
    
    

    private void Fire()
    {
        var scale = _bulletPref.transform.localScale;
        var bullet = Instantiate(_bulletPref, transform.GetComponent<RectTransform>().position, Quaternion.identity).GetComponent<Rigidbody2D>();
        bullet.transform.localScale = scale;
        bullet.AddForce(transform.up * _forceBullet, ForceMode2D.Impulse);
        bullet.transform.parent = _canvas;
        Destroy(bullet.gameObject, 5f);
    } 
}
