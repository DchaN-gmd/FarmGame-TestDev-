using System.Collections;
using UnityEngine;

public class Wheat : MonoBehaviour
{
    [SerializeField] private float _timeUntilGrowing;

    private void OnEnable()
    {
        StopCoroutine(Grow());
    }

    private void OnDisable()
    {
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        yield return new WaitForSeconds(_timeUntilGrowing);

        Debug.Log("Жопа");

        gameObject.SetActive(true);
    }
}
