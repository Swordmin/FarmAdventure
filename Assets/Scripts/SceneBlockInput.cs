using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBlockInput : MonoBehaviour
{

    [SerializeField] private List<BlockResources> _resources;
    [SerializeField] private BlockResources _blockResource;
    [SerializeField] private BlockType _type;
    private void OnEnable()
    {
        foreach(BlockResources resource in _resources) 
        {
            resource.OnSelect += (type) => Select(type, resource);
        }
    }

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

    private void Select(BlockType type, BlockResources blockResource) 
    {
        _type = type;
        _blockResource = blockResource;
    }

    private void Click() 
    {
        if (_blockResource == null)
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.TryGetComponent(out Block block)) 
            {
                if (LevelStateMachine.Instance.Stage == LevelStage.Prepare && _blockResource.IsCan)
                {
                    block.SetType(_blockResource.Type);
                    _blockResource.RemoveCount();
                }
                    
            }
        }
    }

}
