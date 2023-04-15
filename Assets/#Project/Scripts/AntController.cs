using UnityEngine;
using Unity.Netcode;
public class AntController : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float raycastDistance = 1f;
    public float raycastMoveCheckDistance = 1f;
    private GameObject VrPlayer;
    private float spawnDistance = 2.5f;

    /*
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * raycastDistance);
    }
    */
    void Spawn()
    {
        float spawnAngle = Random.Range(0f, 360f); // A random angle in degrees
        Vector3 spawnDirection = Quaternion.Euler(0f, spawnAngle, 0f) * Vector3.forward; // A vector pointing in a random direction
        Vector3 spawnPosition = spawnDirection * spawnDistance; // A random position outside of the cube

        Vector3 lookDirection = (Vector3.zero - spawnPosition).normalized;
        Quaternion spawnRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        spawnPosition.y = transform.localScale.y / 2;
        transform.rotation = spawnRotation;
        transform.position = spawnPosition;

    }
    
    public override void OnNetworkSpawn()
    {
      
        if(IsServer && IsOwner)
        {
            NetworkObject.Despawn();
        }
        if (!IsServer && IsOwner)
        {
            VrPlayer = GameObject.Find("VrPlayer");
            VrPlayer.SetActive(false);
            Spawn();
        }

    }

    void Update()
    {

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitGrab, raycastDistance))
        {
            Debug.Log(hitGrab.collider.gameObject.name);
            if (hitGrab.collider.gameObject.CompareTag("Grabbable"))
            {
                Debug.Log("Collided with grabbable object");
                hitGrab.collider.gameObject.transform.SetParent(transform, true);
                hitGrab.collider.gameObject.GetComponent<GrabbableObject>().ObjectGrabbed = true;
            }
        }

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
    }

    //Could do (but decided against):
    //When raycast hits a grabbable object, switch ObjectGrabbed to true, and set its transform to the transform of ant plus offset
    //(When ants grabs fruit, it checks the difference between its transform and fruit transform, and that is the offset)
    //Then remove this collider system
}