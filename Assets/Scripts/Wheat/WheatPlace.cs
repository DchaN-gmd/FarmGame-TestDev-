using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatPlace : MonoBehaviour
{
    [SerializeField] private float _timeToReset;

    private Wheat _wheat;

    private void Start()
    {
        _wheat = GetComponentInChildren<Wheat>();
    }

    public void GrowWheat()
    {
        StartCoroutine(ResetWheatPlace());
    }

    private IEnumerator ResetWheatPlace()
    {
        _wheat.gameObject.SetActive(false);
        yield return new WaitForSeconds(_timeToReset);

        _wheat.gameObject.SetActive(true);
        StartCoroutine(_wheat.Grow());
        
        StopCoroutine(ResetWheatPlace());
    }
}
