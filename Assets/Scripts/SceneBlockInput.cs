using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBlockInput : MonoBehaviour
{

    public Action<Character> OnCharacterSelect;
    public Action<Block> OnBlockClick;

    [SerializeField] private List<BlockResources> _resources;
    [SerializeField] private BlockResources _blockResource;
    [SerializeField] private BlockType _type;

    private void OnDisable()
    {
        foreach (BlockResources resource in _resources)
        {
            resource.OnSelect -= (type) => Select(type, resource);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Click();
    }

    public void AddResource(BlockResources resource) 
    {
        _resources.Add(resource);
        resource.OnSelect += (type) => Select(type, resource);
    }

    private void Select(BlockType type, BlockResources blockResource) 
    {
        _type = type;
        _blockResource = blockResource;
    }

    private void Click() 
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.TryGetComponent(out Block block)) 
            {
                OnBlockClick?.Invoke(block);
                if(_blockResource)
                    if (LevelStateMachine.Instance.Stage == LevelStage.Prepare && _blockResource.IsCan)
                    {
                        if (block.Type != _blockResource.Type)
                        {
                            block.SetType(_blockResource.Type);
                            _blockResource.RemoveCount();
                        }
                    }
                    
            }

            if(hit.transform.TryGetComponent(out Character character)) 
            {
                OnCharacterSelect?.Invoke(character);
            }
        }
    }

}
