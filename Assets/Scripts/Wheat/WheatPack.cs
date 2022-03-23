using UnityEngine;
using DG.Tweening;

public class WheatPack : MonoBehaviour
{
    [SerializeField] private float _durationOfJump;
    [SerializeField] private float _jumpPower;
    [SerializeField] private ResourceData _recourceData;

    private WheatPackPool _pool;
    private BoxCollider _collider;

    public int Cost => _recourceData.Cost;
    public string Name => _recourceData.Name;

    private void Start()
    {
        _pool = FindObjectOfType<WheatPackPool>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bag bag))
        {
            if(!bag.IsFull)
            {
                var sequence = DOTween.Sequence();
                sequence.Append(transform.DOJump(bag.transform.position, _jumpPower, 1, _durationOfJump));
                sequence.AppendCallback(() => bag.AddPack(gameObject.GetComponent<WheatPack>()));
            }
        }
    }

    public void MoveToStorage(Transform _targetPoint)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOJump(_targetPoint.position, _jumpPower, 1, _durationOfJump));
        sequence.AppendCallback(() => _pool.RestoreElement(gameObject.GetComponent<WheatPack>()));
    }
}
    