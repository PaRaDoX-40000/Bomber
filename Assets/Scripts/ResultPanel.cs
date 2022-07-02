using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private Button _buttonRestart;
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private Color _victoryColor;
    [SerializeField] private Color _defeatColor;
    [SerializeField] private string _victoryText;
    [SerializeField] private string _defeatText;

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void TurnOnResultPanelVictory()
    {
        _resultText.text = _victoryText;
        _resultText.color = _victoryColor;
        TurnOnResultPanel();
    }
    public void TurnOnResultPanelDefeat()
    {
        _resultText.text = _defeatText;
        _resultText.color = _defeatColor;
        TurnOnResultPanel();
    }

    private void TurnOnResultPanel()
    {
        _canvasGroup.alpha = 1;
        _buttonRestart.interactable = true;
    }
    public void TurnOffResultPanel()
    {
        _canvasGroup.alpha = 0;
        _buttonRestart.interactable = false;
    }
    







}
