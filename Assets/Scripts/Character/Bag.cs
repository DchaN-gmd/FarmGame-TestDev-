using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System.Threading.Tasks;

public class Bag : MonoBehaviour
{
    [Header("Main Settings")]
    [SerializeField] private float _indentPacksOffsetY;
    [SerializeField] private float _packsOffsetZ;
    [SerializeField] private int _maxPackCount;
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private int _delayBeforeSendPack;

    private List<WheatPack> _packs = new List<WheatPack>();
    private Vector3 _currentPosition;
    private bool _isMovabled = false;

    public UnityEvent packsCountChanged;

    public int MaxPackCount => _maxPackCount;
    public int PacksCount => _packs.Count;

    [HideInInspector] public bool IsFull = false;

    private void Start()
    {
        _currentPosition = transform.localPosition;
    }

    private void Update()
    {
        MoveByInertia();
    }

    public void AddPack(WheatPack pack)
    {
        if (pack == null) return;

        if (_maxPackCount == _packs.Count)
        {
            IsFull = true;
            return;
        }

        pack.GetComponent<BoxCollider>().enabled = false;
        pack.transform.parent = gameObject.transform;
        pack.transform.localPosition = _currentPosition;
        pack.transform.localRotation = transform.localRotation;
        _currentPosition.y += pack.transform.localScale.y - _indentPacksOffsetY;

        _packs.Add(pack);
        packsCountChanged?.Invoke();
    }

    public List<WheatPack> GetPacks(Transform target)
    {    
        List<WheatPack> packs = new List<WheatPack>();
        packs.AddRange(_packs);
        SendPacks(target);

        _currentPosition = transform.localPosition;
        IsFull = false;
        return packs;
    }

    private async void SendPacks(Transform targetPoint)
    {
        for (int i = _packs.Count - 1; i >= 0; i--)
        {
            await Task.Delay(_delayBeforeSendPack);
            _packs[i].MoveToStorage(targetPoint);
            _packs[i].GetComponent<BoxCollider>().enabled = true;
            _packs.Remove(_packs[i]);
            packsCountChanged?.Invoke();
        }
    }

    private void MoveByInertia()
    {
        if (_joystick.Vertical != 0 && _joystick.Horizontal != 0)
        {
            for (int i = 0; i <= _packs.Count - 1; i++)
            {
                var offset = -_packsOffsetZ * i;
                _packs[i].transform.DOLocalMoveZ(offset, 1);
                _isMovabled = true;
            }
        }

        if (_isMovabled && _joystick.Vertical == 0 && _joystick.Horizontal == 0)
        {
            for (int i = 0; i <= _packs.Count - 1; i++)
            {
                _packs[i].transform.DOLocalMoveZ(_currentPosition.z, 1);
                _isMovabled = false;
            }
        }
    }
}
