using System.Collections;
using UnityEngine;

public class Wheat : MonoBehaviour
{
    [SerializeField] private float _timeToGrowUp;
    [SerializeField] private Vector3 _sproutScale;

    private Vector3 _normalScale;

    private void Start()
    {
        _normalScale = transform.localScale;
    }

    public IEnumerator Grow()
    {   
        gameObject.transform.localScale = _sproutScale;
        yield return new WaitForSeconds(_timeToGrowUp);
        gameObject.transform.localScale = _normalScale;
        StopCoroutine(Grow());
    }
}
