using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CoinMover : MonoBehaviour
{
    [Header("Animation settings")]
    [SerializeField] private float _coinMoveDuration;
    [SerializeField] private float _endPointShakeDuration;
    [SerializeField] private float _strenghtOfShake;
    [SerializeField] private RectTransform _endTargetPoint;

    private Storage _storage;
    private Camera _camera;
    private CoinPool _pool;
    
    private void Awake()
    {
        _camera = FindObjectOfType<Camera>();
        _storage = FindObjectOfType<Storage>();
        _pool = FindObjectOfType<CoinPool>();
    }

    private void OnEnable()
    {
        _storage.onPackSold.AddListener(MoveCoin);
    }

    private void OnDisable()
    {
        _storage.onPackSold.RemoveListener(MoveCoin);
    }

    private void MoveCoin()
    {
        var coin = _pool.GetCoin();
        var targetPoint = _storage.GetComponent<Transform>();

        SpawnCoin(coin.rectTransform, targetPoint);
        var sequence = DOTween.Sequence();
        sequence.Append(coin.transform.DOMove(_endTargetPoint.position, _coinMoveDuration));
        sequence.AppendCallback(() =>
            {
                _endTargetPoint.transform.DOShakePosition(_endPointShakeDuration,_strenghtOfShake);
                _pool.ReturnCoinToPool(coin);
            }
        );
    }

    private void SpawnCoin(RectTransform coin,Transform point)
    {
        coin.position = RectTransformUtility.WorldToScreenPoint(_camera, point.position);
    }

}
