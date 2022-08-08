using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PathCreator : MonoBehaviour
{

    [SerializeField] private Character _selectCharacter;
    private SceneBlockInput _sceneBlockInput;
    private LevelStateMachine _levelStateMachine;

    [Inject]
    private void Constructor(SceneBlockInput sceneBlockInput, LevelStateMachine levelStateMachine) 
    {
        UpdateSceneBlockInput(sceneBlockInput);
        _levelStateMachine = levelStateMachine;
    }

    private void OnDisable()
    {
        _sceneBlockInput.OnCharacterSelect -= UpdateCharacter;
        _sceneBlockInput.OnBlockClick -= UpdatePath;
    }

    private void UpdateSceneBlockInput(SceneBlockInput sceneBlockInput) 
    {
        _sceneBlockInput = sceneBlockInput;
        _sceneBlockInput.OnCharacterSelect += UpdateCharacter;
        _sceneBlockInput.OnBlockClick += UpdatePath;
    }

    private void UpdateCharacter(Character character)
    {
        if (_selectCharacter)
            _selectCharacter.GetComponent<PathViewer>().Hide();
        _selectCharacter = character;
        _selectCharacter.GetComponent<PathViewer>().Show();
    }

    private void UpdatePath(Block block) 
    {
        if (_levelStateMachine.Stage != LevelStage.Prepare)
            return;
        if (_selectCharacter == null)
            return;
        if ((_selectCharacter.LastBlock.transform.position - block.transform.position).magnitude > 1f)
            return;

        if (_selectCharacter.IsFindBlock(block))
        {
            if (_selectCharacter.LastBlock == block)
            {
                _selectCharacter.RemoveBlock(block);
                _selectCharacter.GetComponent<PathViewer>().HideIndexBlock(block);
            }
        }
        else
        {
            _selectCharacter.AddBlock(block);
            _selectCharacter.GetComponent<PathViewer>().ShowIndexBlock(block);
        }
    }

}
