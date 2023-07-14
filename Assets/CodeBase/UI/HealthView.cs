using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textHealth; 
    private Player _player => PlayerCache.Instance.Player;

    private void Start() => _player.Healing.HealthChanged += HealthUpdate;

    public void HealthUpdate(float health, float maxHealth) => _textHealth.text = $"{health}/{maxHealth}";
}
