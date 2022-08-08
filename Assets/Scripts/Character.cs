using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Character : MonoBehaviour
{

    public enum CharacterType 
    {

        Predator,
        FarmAnimal

    }


    private Sequence _sequence;

    [SerializeField] private CharacterType _type = CharacterType.FarmAnimal;
    public CharacterType Type => _type;

    [SerializeField] private float _jumpHeight;
    [SerializeField] private int _numbersJump;
    [SerializeField] private float _direction;

    [SerializeField] private List<Block> _blocks;
    public Block LastBlock => _blocks[_blocks.Count - 1];
    public IReadOnlyCollection<Block> Blocks => _blocks;

    [SerializeField] private Button _buttonStart;

    public Action OnDie;
    public Action OnGoalTake;


    private void Awake()
    {
        _buttonStart.onClick.AddListener(Move);
    }


    private void OnDrawGizmosSelected()
    {
        Color colorSphere = new Color(0, 0, 0);
        float i = 0;
        foreach (Block point in _blocks)
        {
            i += 0.1f;
            colorSphere = new Color(i, i, i);
            Gizmos.color = colorSphere;
            Vector3 pointPosition = new Vector3(point.transform.position.x, point.transform.position.y + 0.64f, point.transform.position.z);
            Gizmos.DrawSphere(pointPosition, 0.1f);
            if (point != _blocks[_blocks.Count - 1])
            {
                Vector3 targetBlock = _blocks[_blocks.IndexOf(point) + 1].transform.position;
                Gizmos.color = Color.green;
                Gizmos.DrawRay(pointPosition, new Vector3(targetBlock.x, targetBlock.y + 0.64f, targetBlock.z));
            }
        }
    }

    public void AddBlock(Block block)
    {
        _blocks.Add(block);
    }

    public void RemoveBlock(Block block) 
    {
        if(IsFindBlock(block))
            _blocks.Remove(block);
    }

    public Block FindBlock(Block blockFind) 
    {
        foreach(Block block in _blocks) 
        {
            if (block == blockFind)
                return blockFind;
        }

        return null;
    }
    public bool IsFindBlock(Block blockFind)
    {
        foreach (Block block in _blocks)
        {
            if (block == blockFind)
                return true;
        }

        return false;
    }


    private void Move()
    {
        _sequence = DOTween.Sequence();
        foreach (Block point in _blocks)
        {
            Vector3 pointPosition = new Vector3(point.transform.position.x, point.transform.position.y + 0.64f, point.transform.position.z);
            if (point.Type == BlockType.Normal)
                _sequence.Append(transform.DOJump(pointPosition, _jumpHeight, _numbersJump, _direction));
            if (point.Type == BlockType.Wheat)
            {
                _sequence.Append(transform.DOJump(pointPosition, _jumpHeight, _numbersJump, _direction));
                _sequence.Append(transform.DOScale(new Vector3(3, 3, 3), 1));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            if (Type == CharacterType.Predator)
                return;
            if (character.Type == CharacterType.Predator)
            {
                _sequence.Kill();
                OnDie?.Invoke();
            }
        }
        if (other.TryGetComponent(out LevelGoal goal))
        {
            OnGoalTake?.Invoke();
            Destroy(goal.gameObject);
        }
    }

}
