using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;

    private Rigidbody rb;

    [Header("Camera Settings")]
    private Camera mainCamera;
    private Vector3 cameraOffset;

    [Header("Score Settings")]
    private int score = 0;

    [Header("Health Settings")]
    public int health = 5;

    public Text scoreText;

    public Text healthText;

    public Text winLoseText;

    public Image winLoseBG;

    // Store starting values to reset on Game Over
    private int startingScore;
    private int startingHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        mainCamera = Camera.main;
        cameraOffset = mainCamera.transform.position - transform.position;

        startingScore = score;
        startingHealth = health;

        SetScoreText();
        SetHealthText();
        winLoseText.text = "";
    }

    void FixedUpdate()
    {
        // Player movement input (natural controls)
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down

        // Move only on X/Z axes
        Vector3 movement = new Vector3(moveX, 0f, moveZ) * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Camera follows player
        mainCamera.transform.position = transform.position + cameraOffset;
    }

    void Update()
    {
        // Game Over check
        if (health <= 0)
        {
            Debug.Log("Game Over!");

            // Reset health and score
            health = startingHealth;
            score = startingScore;

            // Reload current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Coin collection
        if (other.CompareTag("Pickup"))
        {
            score++;
            SetScoreText();
            Destroy(other.gameObject);
        }

        // Trap collision
        if (other.CompareTag("Trap"))
        {
            health--;
            SetHealthText();
        }

        // Goal reached
        if (other.CompareTag("Goal"))
        {
            winLoseText.text = "You Win!";
            winLoseText.color = Color.black;
            winLoseBG.color = Color.green;
        }
    }


    void SetScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void SetHealthText()
    {
        healthText.text = "Health: " + health;
    }
}
