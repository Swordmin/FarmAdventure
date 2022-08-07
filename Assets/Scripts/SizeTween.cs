using UnityEngine;
using DG.Tweening;

public class SizeTween : MonoBehaviour
{

    [SerializeField] private Vector3 _targetSize;
    [SerializeField] private float _time;

    [ExecuteAlways]
    private void Awake()
    {
        transform.DOScale(_targetSize, _time).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

}
