using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bag : MonoBehaviour
{
    [SerializeField] private float _packsOffsetY;
    [SerializeField] private float _packsOffsetX;
    [SerializeField] private int _maxPackCount;
    [SerializeField] private FloatingJoystick _joystick;

    private Stack<WheatPack> _stackOfPacks = new Stack<WheatPack>();
    private List<WheatPack> _packs = new List<WheatPack>();
    private Vector3 _currentPosition;

    public bool IsFull = false;

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
        if (_maxPackCount == _stackOfPacks.Count)
        {
            IsFull = true;
            return;
        }
        pack.GetComponent<BoxCollider>().enabled = false;
        pack.transform.parent = gameObject.transform;
        pack.transform.localPosition = _currentPosition;
        pack.transform.localRotation = transform.localRotation;
        _currentPosition.y += pack.transform.localScale.y - _packsOffsetY;
        _stackOfPacks.Push(pack);
        _packs.Add(pack);
    }

    private void MoveByInertia()
    {
        if(_joystick.Vertical > 0)
        {
            for (int i = 0; i <= _packs.Count - 1; i++)
            {
                var offset = _packsOffsetX * i * -_joystick.Vertical;
                _packs[i].transform.DOLocalMoveZ(offset, 1);
            }
        }

        if (_joystick.Vertical < 0)
        {
            for (int i = 0; i <= _packs.Count - 1; i++)
            {
                var offset = _packsOffsetX * i * _joystick.Vertical;
                _packs[i].transform.DOLocalMoveZ(offset, 1);
            }
        }
        if (_joystick.Horizontal > 0)
        {
            for (int i = 0; i <= _packs.Count - 1; i++)
            {
                var offset = _packsOffsetX * i * -_joystick.Horizontal;
                _packs[i].transform.DOLocalMoveZ(offset, 1);
            }
        }

        if (_joystick.Horizontal < 0)
        {
            for (int i = 0; i <= _packs.Count - 1; i++)
            {
                var offset = _packsOffsetX * i * _joystick.Horizontal;
                _packs[i].transform.DOLocalMoveZ(offset, 1);
            }
        }
    }
}
