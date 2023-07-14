using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textGold; 
    private Player _player => PlayerCache.Instance.Player;

    private void Start() => _player.GoldChanged += GoldUpdate;

    public void GoldUpdate(int gold) => _textGold.text = gold.ToString();
}
