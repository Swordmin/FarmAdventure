using UnityEngine;
using DG.Tweening;

public class MoveTween : MonoBehaviour
{

    [SerializeField] private bool _isLoop;

    [SerializeField] private Vector3 _target;

    [SerializeField] private float _speed;

    private void Awake()
    {
        transform.DOMove(transform.position + _target, _speed).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
        transform.DOLocalRotate(new Vector3(0, 360, 0), 5f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

}
