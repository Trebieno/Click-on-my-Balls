using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SilverView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textSilver; 
    private Player _player => PlayerCache.Instance.Player;

    private void Start() => _player.SilverChanged += SilverUpdate;

    public void SilverUpdate(int silver) => _textSilver.text = silver.ToString();
}
