using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public bool ObjectGrabbed;

    void OnCollisionEnter(Collision collision)
    {
        //Drop object if it collides with other things
        if(ObjectGrabbed)
        {
            Debug.Log("Let go of grabbable object");
            transform.parent = null;
            ObjectGrabbed = false;
        }
    }
}