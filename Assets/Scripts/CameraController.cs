using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;


    // Update is called once per frame
    void Update()
    {
        // Move forward and backward
        float moveDirection = Input.GetAxis("Vertical"); // W = +1, S = -1
        transform.Translate(Vector3.forward * moveDirection * moveSpeed * Time.deltaTime);

        // Rotate left and right
        float rotationDirection = Input.GetAxis("Horizontal"); // A = -1, D = +1
        transform.Rotate(Vector3.up * rotationDirection * rotationSpeed * Time.deltaTime);
    }
}
