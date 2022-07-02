using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultLevel : MonoBehaviour
{
    [SerializeField] ResultPanel _resultPanel;
    [SerializeField] Player _player;
    [SerializeField] VictoryLevel _victoryLevel;

    private void Start()
    {
        _resultPanel.TurnOffResultPanel();
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        _player.DiedEvent += DefeatLevel;
        _victoryLevel.VictoryEvent += VictoryLevel;
    }
    private void OnDisable()
    {
        _player.DiedEvent -= DefeatLevel;
        _victoryLevel.VictoryEvent -= VictoryLevel;
    }

    private void VictoryLevel()
    {
        _resultPanel.TurnOnResultPanelVictory();
        Time.timeScale = 0;
    }
    private void DefeatLevel()
    {
        _resultPanel.TurnOnResultPanelDefeat();
        Time.timeScale = 0;
    }



}
