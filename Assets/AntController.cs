using UnityEngine;

public class AntController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float raycastDistance = 1f;

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * raycastDistance);
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Move the player forward or backward based on input
        transform.Translate(Vector3.forward * vertical * moveSpeed * Time.deltaTime);

        // Rotate the player on the Y axis based on input
        transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, raycastDistance))
        {
            Debug.Log(hit.collider.gameObject.name);
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