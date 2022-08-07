using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(Button))]
public class BlockResources : MonoBehaviour
{

    public Action<BlockType> OnSelect;
    public bool IsCan => _count > 0;

    [SerializeField] private BlockType _type;
    public BlockType Type => _type;
    [SerializeField] private TextMeshProUGUI _textCount;
    [SerializeField] private int _count;
    public int Count => _count;

    private Button _button;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => OnSelect?.Invoke(_type));
        UpdateText();
    }


    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => OnSelect?.Invoke(_type));
    }

    public void RemoveCount() 
    {
        if (_count <= 0)
            return;
        _count--;
        UpdateText();
    }

    private void UpdateText() => _textCount.text = $"{_count}";

}
