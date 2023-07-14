using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textScore; 
    private Player _player => PlayerCache.Instance.Player;

    private void Start() => _player.ScoreChanged += ScoreUpdate;

    public void ScoreUpdate(int score) => _textScore.text = score.ToString();
}
