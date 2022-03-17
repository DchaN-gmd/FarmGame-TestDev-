using UnityEngine;
using EzySlice;

public class Scythe : MonoBehaviour
{
    private Material _sliceObjectMaterial;
    private GameObject _objectToSlice;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Wheat wheat))
        {
            _sliceObjectMaterial = other.GetComponent<MeshRenderer>().material;
            _objectToSlice = other.gameObject;
            SlicedHull slisedObject = GetSlicedObject(_objectToSlice, _sliceObjectMaterial);
            Slice(slisedObject);
        }
    }

    private void Slice(SlicedHull slisedObject)
    {
        if(slisedObject == null)
        {
            return;
        }
        GameObject upperHull = slisedObject.CreateUpperHull(_objectToSlice, _sliceObjectMaterial);
        upperHull.transform.position = _objectToSlice.transform.position;
        upperHull.AddComponent<Rigidbody>();
        upperHull.AddComponent<MeshCollider>().convex = true;
        upperHull.AddComponent<PieceOfWheat>().isUpperPiece = true;

        GameObject lowerHull = slisedObject.CreateLowerHull(_objectToSlice, _sliceObjectMaterial);
        lowerHull.transform.position = _objectToSlice.transform.position;
        lowerHull.AddComponent<Rigidbody>();
        lowerHull.AddComponent<MeshCollider>().convex = true;
        lowerHull.AddComponent<PieceOfWheat>().isUpperPiece = false;

        _objectToSlice.GetComponentInParent<WheatPlace>().GrowWheat();
    }

    private SlicedHull GetSlicedObject(GameObject objectToSlice, Material objectMaterial = null)
    {
        return _objectToSlice.Slice(transform.position, transform.forward, objectMaterial);
    }
}
