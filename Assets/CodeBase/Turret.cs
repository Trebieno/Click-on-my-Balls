using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Turret : MonoCache
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _actionRadius = 10f;
    [SerializeField] private float _fireDelay = 1f;
    [SerializeField] private float _forceBullet = 5f;
    [SerializeField] private LayerMask _actionMask;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private GameObject _bulletPref;
    private float fireTimer;

    private void Start()
    {
        
    }

    public override void OnUpdateTick()
    {
        if (_target != null && !_target.gameObject.activeSelf)
            _target = null;
        
        if (_target == null || !_target.gameObject.activeSelf)
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, _actionRadius, _actionMask);
            if (colliders.Length > 0)
                if(colliders[0].gameObject.activeSelf)
                    _target = colliders[0].transform;
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
        var bullet = Instantiate(_bulletPref, _rigidbody2D.transform.position, _rigidbody2D.transform.rotation).GetComponent<Rigidbody2D>();
        bullet.AddForce(_rigidbody2D.transform.up * _forceBullet, ForceMode2D.Impulse);
        Destroy(bullet.gameObject, 5f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _actionRadius); 
    }
}
