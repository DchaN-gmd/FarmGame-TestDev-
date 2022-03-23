using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Storage : MonoBehaviour
{
    [SerializeField] private Transform _targetPointToJump;
    [SerializeField] private int _processPacketsDelay;

    private List<WheatPack> _packs = new List<WheatPack>();

    public UnityEvent<int> onPackReceived;
    public UnityEvent onPackSold;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bag bag))
        {
            _packs = bag.GetPacks(_targetPointToJump);

            ProcessPackets();
        }
    }

    private async void ProcessPackets()
    {
        foreach (var pack in _packs)
        {
            await Task.Delay(_processPacketsDelay);
            onPackReceived?.Invoke(pack.Cost);
            onPackSold?.Invoke();
        }
    }
}

