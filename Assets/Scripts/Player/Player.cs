using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public Transform holdPosition, playerPosition;
    public GameObject heldItem = null;
    public Collider2D itemCollider;

    public Rigidbody2D rb;

    public float speed = 5f, minSpeed = 0.1f, jumpForce = 6f, maxJump = 0f;
    public Vector2 lastDirection = Vector2.down;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody2D>();
    }
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
