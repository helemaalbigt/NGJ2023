using UnityEngine;
using Unity.Netcode;
public class AntController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float raycastDistance = 1f;
    public float raycastMoveCheckDistance = 1f;

    /*
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * raycastDistance);
    }
    */
    void Update()
    {
        if (!IsOwner) return;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Physics.SphereCast(transform.position, transform.localScale.x / 2, transform.forward, out RaycastHit hitForward, raycastMoveCheckDistance))
        {
            if (!hitForward.collider.CompareTag("Grabbable"))
            {
                // Block movement
                if (vertical > 0)
                {
                    vertical = 0;
                }
            }
        }
        if (Physics.SphereCast(transform.position, transform.localScale.x / 2, -transform.forward, out RaycastHit hitBack, raycastMoveCheckDistance))
        {
            if(!hitBack.collider.CompareTag("Grabbable"))
            {
                // Block movement
                if(vertical < 0)
                {
                    vertical = 0;
                }
            }
        }

        // Move the player forward or backward based on input
        transform.Translate(Vector3.forward * vertical * moveSpeed * Time.deltaTime);

        // Rotate the player on the Y axis based on input
        transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitGrab, raycastDistance))
        {
            Debug.Log(hitGrab.collider.gameObject.name);
        }
    }

    //TODO:
    //When raycast hits a grabbable object, switch a boolean GrabbedFruit to true
    //When this boolean is true, the fruit will set its transform to the transform of the ant plus some offset
    //(When ants grabs fruit, it checks the difference between its transform and fruit transform, and that is the offset)
    //Then remove this collider system

    //Grab food or other grabbable objects
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with something");

        if (other.CompareTag("Grabbable"))
        {
            Debug.Log("Collided with grabbable object");
            other.transform.SetParent(transform, true);
        }
    }
}