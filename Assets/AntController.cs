using UnityEngine;

public class AntController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Move the player forward or backward based on input
        transform.Translate(Vector3.forward * vertical * moveSpeed * Time.deltaTime);

        // Rotate the player on the Y axis based on input
        transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);
    }
}