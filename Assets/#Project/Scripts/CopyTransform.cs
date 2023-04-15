using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    public Transform _target;
    
    void Update() {
        transform.position = _target.position;
        transform.rotation = _target.rotation;
    }
}
