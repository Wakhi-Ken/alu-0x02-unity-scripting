using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;

    private Rigidbody rb;

    [Header("Camera Settings")]
    private Camera mainCamera;
    private Vector3 cameraOffset;

    [Header("Score Settings")]
    private int score = 0; // Player score starts at 0

    void Start()
    {
        // Get Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Cache main camera and initial offset
        mainCamera = Camera.main;
        cameraOffset = mainCamera.transform.position - transform.position;
    }

    void FixedUpdate()
    {
        // Get input from WASD / Arrow keys
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Move only on X and Z axes
        Vector3 movement = new Vector3(moveX, 0f, moveZ) * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Camera follows player at a constant offset
        mainCamera.transform.position = transform.position + cameraOffset;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if collided object is a coin
        if (other.CompareTag("Pickup"))
        {
            score++; // Increment score
            Debug.Log("Score: " + score); // Print to console

            Destroy(other.gameObject); // Remove coin
        }
    }
}
