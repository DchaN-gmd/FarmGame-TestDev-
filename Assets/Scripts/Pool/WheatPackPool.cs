using UnityEngine;

public class WheatPackPool : MonoBehaviour
{
    [SerializeField] private WheatPack _prefab;
    [SerializeField] private int _count;
    [SerializeField] private bool _autoExpand;

    private PoolMono<WheatPack> _pool;
    
    private void Start()
    {
        _pool = new PoolMono<WheatPack>(_prefab, _count, transform);
        _pool.autoExpand = _autoExpand;
    }

    private void OnEnable()
    {
        PieceOfWheat.UpperPieceFalled += DropWheatPack;  
    }

    private void OnDisable()
    {
        PieceOfWheat.UpperPieceFalled -= DropWheatPack;
    }

    private void DropWheatPack(Vector3 piecePosition)
    {
        var element = _pool.GetFreeElement();
        element.transform.position = piecePosition;
    }
}
