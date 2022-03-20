using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WheatPack : MonoBehaviour
{
    [SerializeField] private float _durationOfJump;
    [SerializeField] private float _jumpPower;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Bag bag))
        {
            if(!bag.IsFull)
            {
                transform.DOJump(bag.transform.position, _jumpPower, 1, _durationOfJump).SetEase(Ease.Flash).OnComplete(() =>
                    bag.AddPack(gameObject.GetComponent<WheatPack>()));
            }
        }
    }
}
