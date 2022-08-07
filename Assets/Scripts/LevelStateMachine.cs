using System;
using UnityEngine;
using UnityEngine.UI;


public enum LevelStage 
{
    Prepare,
    Play,
    Lose,
    Win
}
public class LevelStateMachine : Singleton<LevelStateMachine>
{

    [SerializeField] private Button _playButton;
    [SerializeField] private LevelStage _stage;
    [SerializeField] private Character _mainCharacter;
    public LevelStage Stage => _stage;

    public Action<LevelStage> OnStageUpdate;

    private void OnEnable()
    {
        _playButton?.onClick.AddListener(() => UpdateState(LevelStage.Play));
        _mainCharacter.OnDie += () => UpdateState(LevelStage.Lose);
        _mainCharacter.OnGoalTake += () => UpdateState(LevelStage.Win);
    }

    private void OnDisable()
    {
        _playButton?.onClick.RemoveListener(() => UpdateState(LevelStage.Play));
        _mainCharacter.OnDie -= () => UpdateState(LevelStage.Lose);
        _mainCharacter.OnGoalTake -= () => UpdateState(LevelStage.Win);
    }


    private void UpdateState(LevelStage stage) 
    {
        if (stage == _stage)
            return;
        _stage = stage;
        OnStageUpdate?.Invoke(_stage);
    }



}
