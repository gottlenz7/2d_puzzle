using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private Rigidbody2D rb;

    private float speed = 5f, minSpeed = 0.1f, jumpForce = 5f, maxJump = 0f;

    private Vector2 lastDirection = Vector2.down;
    public bool IsRight => lastDirection.x > 0 && Input.GetKey(KeyCode.D);
    public bool IsLeft => lastDirection.x < 0 && Input.GetKey(KeyCode.A);
    public bool Jump => (Input.GetKeyDown(KeyCode.Space));
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontal, vertical);
        inputVector = inputVector.normalized;

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        if (inputVector.magnitude > minSpeed)
            lastDirection = inputVector;

        if (Jump && rb.position.y <= maxJump)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
