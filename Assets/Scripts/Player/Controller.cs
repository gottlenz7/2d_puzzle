using UnityEngine;

public class Controller : MonoBehaviour 
{
    public static Controller Instance { get; private set; }

    private TakeDropController takeDropscript;

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

        takeDropscript = new TakeDropController();
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
            takeDropscript.TakeItem();
        }
        if (Input.GetMouseButtonDown(0))
        {
            GetFunction();

            if (PlayerModel.Instance.heldItem != null)
            {
                GameObject Item = PlayerModel.Instance.heldItem.gameObject;
                Item.GetComponent<ThrowController>().ThrowItem();
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

        }
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontal, vertical);
        inputVector = inputVector.normalized;

        PlayerModel.Instance.rb.linearVelocity = new Vector2(horizontal * PlayerModel.Instance.speed, PlayerModel.Instance.rb.linearVelocity.y);

        if (inputVector.magnitude > PlayerModel.Instance.minSpeed)
            PlayerModel.Instance.lastDirection = inputVector;

        if (PlayerVisual.Instance.Jump)
            PlayerModel.Instance.rb.linearVelocity = new Vector2(PlayerModel.Instance.rb.linearVelocity.x, PlayerModel.Instance.jumpForce);
    }
}
