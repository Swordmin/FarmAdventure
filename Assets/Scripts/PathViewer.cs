using UnityEngine;

[RequireComponent(typeof(Character))]
public class PathViewer : MonoBehaviour
{
    [SerializeField] private Character _character;
    private void Awake()
    {
        _character = GetComponent<Character>();
    }


    public void Show()
    {
        foreach(Block block in _character.Blocks) 
        {
            block.Select();
        }
    }

    public void Hide() 
    {
        foreach (Block block in _character.Blocks)
        {
            block.UnSelect();
        }
    }

    public void ShowIndexBlock(Block block) => block.Select();
    public void HideIndexBlock(Block block) => block.UnSelect();

}
