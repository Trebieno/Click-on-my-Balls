using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoCache, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform _transform;
    [SerializeField] private Turret _turret;
    [SerializeField] private GameObject _hologramPrefab;

    [SerializeField] private GameObject _content;


    [SerializeField] private float _minDistance = 0.3f;
    private bool _hologramMove;
    private GameObject _hologramView;

    public override void OnUpdateTick()
    {
        if (_hologramView != null)
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            _hologramView.transform.position = pos;

            if (Input.GetButtonDown("Cancel"))
            {
                Cancel();
            }
        } 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!_content.activeSelf) return;
        
        _hologramView = Instantiate(_hologramPrefab, eventData.position, Quaternion.identity);
        
        _content?.SetActive(false);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if(_hologramView == null) return;
        
        if (_minDistance >= Vector2.Distance(_transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            Cancel();
            return;
        }
            
        Vector2 scale = _turret.transform.localScale;
        
        var turret = Instantiate(_turret, _hologramView.transform.position, Quaternion.identity);
        turret.transform.localScale = scale;
        
        Destroy(_hologramView);
    }

    private void Cancel()
    {
        _content?.SetActive(true);
        Destroy(_hologramView);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        
        Gizmos.DrawWireSphere(_transform.position, _minDistance);
    }
}
