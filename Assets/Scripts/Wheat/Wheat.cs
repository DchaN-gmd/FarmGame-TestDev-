using System.Collections;
using UnityEngine;

public class Wheat : MonoBehaviour
{
    [SerializeField] private float _timeToGrowUp;
    [SerializeField] private Vector3 _sproutScale;

    private MeshCollider _meshCollider;
    private Vector3 _normalScale;

    private void Start()
    {
        _normalScale = transform.localScale;
        _meshCollider = GetComponent<MeshCollider>();
    }

    public IEnumerator Grow()
    {
        _meshCollider.enabled = false;
        gameObject.transform.localScale = _sproutScale;
        yield return new WaitForSeconds(_timeToGrowUp);
        _meshCollider.enabled = true;
        gameObject.transform.localScale = _normalScale;
        StopCoroutine(Grow());
    }
}
