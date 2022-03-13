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
            _sliceObjectMaterial = other.GetComponentInChildren<MeshRenderer>().material;
            _objectToSlice = other.gameObject;
            SlicedHull slisedObject = GetSlicedObject(_objectToSlice, _sliceObjectMaterial);
            Slice(slisedObject);
        }
    }

    private void Slice(SlicedHull slisedObject)
    {
        GameObject upperHull = slisedObject.CreateUpperHull(_objectToSlice, _sliceObjectMaterial);
        upperHull.AddComponent<Rigidbody>();
        upperHull.AddComponent<MeshCollider>().convex = true;
        GameObject lowerHull = slisedObject.CreateLowerHull(_objectToSlice, _sliceObjectMaterial);
        lowerHull.AddComponent<Rigidbody>();
        lowerHull.AddComponent<MeshCollider>().convex = true;
        Destroy(_objectToSlice);
    }

    private SlicedHull GetSlicedObject(GameObject objectToSlice, Material objectMaterial = null)
    {
        return _objectToSlice.Slice(transform.position, transform.up, objectMaterial);
    }
}
