using UnityEngine;

public class Controller : MonoBehaviour 
{
    public static Controller Instance { get; private set; }

    public Note noteScript;

    private TakeDrop takeDropscript;

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

        takeDropscript = new TakeDrop();
    }

    private void Update()
    {
        CheckButton();
        HandleMovement();
    }

    private void CheckButton()
    {
        PlayerVisual.Instance.Jump = false;
        PlayerVisual.Instance.isLeft = false;
        PlayerVisual.Instance.isRight = false;

        if (Input.GetKey(KeyCode.D))
        {
            PlayerVisual.Instance.isRight = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            PlayerVisual.Instance.isLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerVisual.Instance.Jump = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Player.Instance.heldItem != null)
            {
                takeDropscript.DropItem();
                return;
            }

            Collider2D[] colliders = Physics2D.OverlapCircleAll(Player.Instance.transform.position, 2f);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Note"))
                {
                    if (noteScript.isOpen)
                    {
                        noteScript.HideNote();
                        break;
                    }
                    else
                    {
                        noteScript.ShowNote();
                        break;
                    }
                }

                else if (collider.CompareTag("Item"))
                {
                    takeDropscript.TryTake(collider);
                    break;
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            GetFunction();

            if (Player.Instance.heldItem != null)
            {
                GameObject Item = Player.Instance.heldItem.gameObject;
                Item.GetComponent<Throw>().ThrowItem();
            }
        }
    }
    
    private void GetFunction()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            Mirror mirror = hit.collider.GetComponent<Mirror>();
            if (mirror != null)
                mirror.Rotate();

            Lever lever = hit.collider.GetComponent<Lever>();
            if (lever != null)
                lever.SwithchLever();

            Throne throne = hit.collider.GetComponent<Throne>();

            float distance = Vector3.Distance(hit.collider.transform.position, Player.Instance.transform.position);

            if (throne != null && distance < 2f)
            {
                PlayerVisual.Instance.Jump = false;
                PlayerVisual.Instance.isLeft = false;
                PlayerVisual.Instance.isRight = false;
                Player.Instance.speed = 0f;
                throne.SitOnThrone();
            }
        }
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontal, vertical);
        inputVector = inputVector.normalized;

        Player.Instance.rb.linearVelocity = new Vector2(horizontal * Player.Instance.speed, Player.Instance.rb.linearVelocity.y);

        if (inputVector.magnitude > Player.Instance.minSpeed)
            Player.Instance.lastDirection = inputVector;

        if (PlayerVisual.Instance.Jump)
            Player.Instance.rb.linearVelocity = new Vector2(Player.Instance.rb.linearVelocity.x, Player.Instance.jumpForce);
    }
}
