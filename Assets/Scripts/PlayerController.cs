using UnityEngine;

public class PlayerController : MonoBehaviour


{
    //player reference(set in inspector) to move the player object using Rigidbody
    public GameObject player;

    // Editable speed in Inspector
    public float speed = 5f;

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get input from WASD or Arrow keys
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Movement only on X and Z axes
        Vector3 movement = new Vector3(moveX, 0f, moveZ) * speed * Time.fixedDeltaTime;

        // Move the player
        rb.MovePosition(rb.position + movement);

        //camera follow player
        transform.position = player.transform.position + new Vector3(0, 5, -10);
    }
}
