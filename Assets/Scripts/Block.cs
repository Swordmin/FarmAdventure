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

    private GameObject _index;


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

    public void Select() 
    {
        GameObject IndexCreate = Instantiate(Resources.Load<GameObject>("Index"),transform.position,Quaternion.identity,transform);
        _index = IndexCreate;
    }

    public void UnSelect() 
    {
        Destroy(_index);
    }
}