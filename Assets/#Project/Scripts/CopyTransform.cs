using UnityEngine;
using UnityEngine.Serialization;

public class CopyTransform : MonoBehaviour
{
    public Transform _referenceL;
    public Transform _targetL;
    public Transform _referenceR;
    public Transform _targetR;

    void Update() {
        _targetL.position = _referenceL.position;
        _targetL.rotation = _referenceL.rotation;
        _targetR.position = _referenceR.position;
        _targetR.rotation = _referenceR.rotation;
    }
}
