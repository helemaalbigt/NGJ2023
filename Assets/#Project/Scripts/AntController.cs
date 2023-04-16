using UnityEngine;

public class AntController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float raycastDistance = 1f;
    public float raycastMoveCheckDistance = 1f;
    private float spawnDistance = 2.5f;
    public float offset;
    public float SphereRadius = 0.025f;

    public bool AntOne;
    public Transform Attach;
    public bool alreadyGrabbed = false;
    public Transform SceneWrapper;

    public LayerMask layerMask;


    public void Spawn()
    {
        if(alreadyGrabbed)
        {
            Attach.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            Attach.GetChild(0).transform.parent = SceneWrapper;
        }
        float spawnAngle = Random.Range(0f, 360f); // A random angle in degrees
        Vector3 spawnDirection = Quaternion.Euler(0f, spawnAngle, 0f) * Vector3.forward; // A vector pointing in a random direction
        Vector3 spawnPosition = spawnDirection * spawnDistance; // A random position outside of the cube

        Vector3 lookDirection = (Vector3.zero - spawnPosition).normalized;
        Quaternion spawnRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        spawnPosition.y = 0;
        transform.rotation = spawnRotation;
        transform.position = spawnPosition;

    }

    private void Awake() {
        Spawn();
    }

    void Update()
    {

        if (AntOne)
        {
            HandleMovement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            HandleGrab();

        }
        else
        {
            HandleMovement(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
            HandleGrab();
        }
    }
    void HandleMovement(float horizontal, float vertical)
    {
        /*
        if (Physics.SphereCast(transform.position + Vector3.forward * offset, SphereRadius, transform.forward, out RaycastHit hitForward, raycastMoveCheckDistance))
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
        if (Physics.SphereCast(transform.position + Vector3.forward * offset, SphereRadius, -transform.forward, out RaycastHit hitBack, raycastMoveCheckDistance))
        {
            if (!hitBack.collider.CompareTag("Grabbable"))
            {
                // Block movement
                if (vertical < 0)
                {
                    vertical = 0;
                }
            }
        }
        */
        // Move the player forward or backward based on input
        transform.Translate(Vector3.forward * vertical * moveSpeed * Time.deltaTime);

        // Rotate the player on the Y axis based on input
        transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);
    }

    void HandleGrab()
    {
        if (Attach.childCount == 0)
        {
            alreadyGrabbed = false;
        }

        var collisions = Physics.OverlapSphere(transform.position, SphereRadius, layerMask);
        if(collisions.Length > 0)
        {
            collisions[0].GetComponent<Rigidbody>().isKinematic = true;
            collisions[0].transform.SetParent(Attach, true);
            //collisions[0].transform.localPosition = Vector3.zero;
            alreadyGrabbed = true;
        }
        /*
        if (Physics.SphereCast(transform.position + Vector3.forward * offset, SphereRadius, transform.forward, out RaycastHit hitGrab, raycastDistance) && !alreadyGrabbed)
        {
            if (hitGrab.collider.gameObject.CompareTag("Grabbable"))
            {
                Debug.Log("Collided with grabbable object");
                    
                hitGrab.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                hitGrab.collider.gameObject.transform.SetParent(Attach, true);
                hitGrab.collider.gameObject.transform.localPosition = Vector3.zero;
                alreadyGrabbed = true;
            }
        }
        */
    }


    //Could do (but decided against):
    //When raycast hits a grabbable object, switch ObjectGrabbed to true, and set its transform to the transform of ant plus offset
    //(When ants grabs fruit, it checks the difference between its transform and fruit transform, and that is the offset)
    //Then remove this collider system
}