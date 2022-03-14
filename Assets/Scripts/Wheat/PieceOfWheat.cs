using UnityEngine;
using UnityEngine.Events;

public class PieceOfWheat : MonoBehaviour
{
    public bool isUpperPiece;
    public static UnityAction<Vector3> UpperPieceFalled;

    private float _timeToDestroy = 2f;

    private void Start()
    {
        if (isUpperPiece)
        {
            UpperPieceFalled?.Invoke(transform.position);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, _timeToDestroy);
        }
    }

}
