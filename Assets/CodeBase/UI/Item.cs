using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoCache, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Turret _turret;
    [SerializeField] private GameObject _hologramPrefab;
    [SerializeField] private Transform _canvas;

    [SerializeField] private GameObject _content;

    private bool _hologramMove;
    private GameObject _hologramView;

    public override void OnUpdateTick()
    {
        if (_hologramView != null)
        {
            _hologramView.transform.position = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        } 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _hologramView = Instantiate(_hologramPrefab, eventData.position, Quaternion.identity);

        Vector2 scale = _hologramView.transform.localScale;
        
        _hologramView.transform.parent = _canvas;

        _hologramView.transform.localScale = scale;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        Vector2 scale = _turret.transform.localScale;
        var turret = Instantiate(_turret, _hologramView.transform.position, Quaternion.identity);
        turret.transform.parent = _canvas;
        turret.transform.localScale = scale;
        Destroy(_hologramView);

        _content.SetActive(false);
    }
}
