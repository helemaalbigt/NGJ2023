using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    public Transform _reference;
    public Transform _target;
    
    void Update() {
        _target.position = _reference.position;
        _target.rotation = _reference.rotation;
    }
}
