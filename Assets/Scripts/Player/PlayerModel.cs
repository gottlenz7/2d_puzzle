using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public static PlayerModel Instance { get; private set; }

    public Transform holdPosition;
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
            DontDestroyOnLoad(gameObject);
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
