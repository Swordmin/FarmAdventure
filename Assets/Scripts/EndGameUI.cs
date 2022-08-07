using System;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{

    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _winPanel;

    private void OnEnable()
    {
        LevelStateMachine.Instance.OnStageUpdate += UpdateLevelStage;
    }

    private void OnDisable()
    {
        LevelStateMachine.Instance.OnStageUpdate -= UpdateLevelStage;
    }

    private void UpdateLevelStage(LevelStage stage)
    {
        switch (stage)
        {
            case LevelStage.Prepare:
                break;
            case LevelStage.Play:
                break;
            case LevelStage.Lose:
                ShowLoseScreen();
                break;
            case LevelStage.Win:
                ShowWinScreen();
                break;
        }
    }

    private void ShowWinScreen()
    {
        _winPanel?.SetActive(true);
    }

    private void ShowLoseScreen()
    {
        _losePanel?.SetActive(true);
    }
}