using UnityEngine;
using UnityEngine.UI;

public class CoinPool : MonoBehaviour
{
    [SerializeField] private Image _prefab;
    [SerializeField] private int _count;
    [SerializeField] private bool _autoExpand;

    private PoolMono<Image> _pool;

    private void Awake()
    {
        _pool = new PoolMono<Image>(_prefab, _count, transform);
        _pool.autoExpand = _autoExpand;
    }

    public Image GetCoin()
    {
        var element = _pool.GetFreeElement();
        return element;
    }

    public void ReturnCoinToPool(Image coin)
    {
        if (coin == null)
        {
            return;
        }
        coin.gameObject.SetActive(false);
        _pool.AddElement(coin);
    }
}
