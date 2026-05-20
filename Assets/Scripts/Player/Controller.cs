using UnityEngine;

public class Controller : MonoBehaviour 
{
    public static Controller Instance { get; private set; }

    private PlayerModel model;
    [SerializeField] private PlayerView view;

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

        model = new PlayerModel();
        takeDropscript = new TakeDropController(view);
    }

    private void Update()
    {
        view.SetDirection(view.isRight, view.isLeft, view.Jump);
        view.SetAnimator(view.animator);

        CheckButton();
        HandleMovement();
        CheckGrounded();
    }

    private void CheckButton()
    {
        view.Jump = false;
        view.isLeft = false;
        view.isRight = false;

        if (Input.GetKey(KeyCode.D))
        {
            view.isRight = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            view.isLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (model.jumpsLeft > 0)
            {
                view.Jump = true;
                model.jumpsLeft--;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            takeDropscript.TakeItem();
        }
        if (Input.GetMouseButtonDown(0))
        {
            GetFunction();

            if (view.heldItem != null && view.heldItem.GetComponent<ThrowController>() != null) 
            {
                ThrowController.Instance.ThrowItem();
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
            {
                lever.Init(view);

                lever.SwithchLever();
            }

        }
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontal, vertical);
        inputVector = inputVector.normalized;

        view.rb.linearVelocity = new Vector2(horizontal * model.speed, view.rb.linearVelocity.y);

        if (inputVector.magnitude > model.minSpeed)
            model.lastDirection = inputVector;

        if (view.Jump)
            view.rb.linearVelocity = new Vector2(view.rb.linearVelocity.x, model.jumpForce);
    }

    private void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(view.transform.position, Vector2.down, view.GetComponent<Collider2D>().bounds.extents.y + 0.1f);

        bool wasGrounded = model.isGrounded;
        model.isGrounded = hit.collider != null ? true : false;

        if (model.isGrounded && !wasGrounded)
            model.jumpsLeft = model.maxJumps;
    }
}
