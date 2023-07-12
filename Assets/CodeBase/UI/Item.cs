using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoCache, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Turret _turret;
    [SerializeField] private GameObject _hologramPrefab;

    private bool _hologramMove;
    private GameObject _hologramView;

    public override void OnUpdateTick()
    {
        if (_hologramView != null)
        {
            _hologramView.transform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        } 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _hologramView = Instantiate(_hologramPrefab, eventData.position, Quaternion.identity);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        Instantiate(_turret, eventData.position, Quaternion.identity);
        Destroy(_hologramView);
    }
}
