using UnityEngine;

public enum BlockType
{
    Normal,
    Wheat,
    Water
}

public class Block : MonoBehaviour
{

    [SerializeField] private BlockType _type = BlockType.Normal;
    public BlockType Type => _type;


    public void SetType(BlockType type)
    {
        _type = type;
        switch (_type)
        {
            case BlockType.Normal:
                break;
            case BlockType.Wheat:
                GetComponentInChildren<MeshFilter>().mesh = Resources.Load<Mesh>("BlocksMesh/GroundWheat");
                break;
            case BlockType.Water:
                break;
        }

    }
}