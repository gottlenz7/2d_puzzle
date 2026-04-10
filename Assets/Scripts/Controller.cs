using UnityEngine;

public class Controller : MonoBehaviour 
{
    public static Controller Instance { get; private set; }

    public bool isRight = false, isLeft = false, isUp = false;

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
    }

    private void Update()
    {
        CheckButton();
    }

    private void CheckButton()
    {
        isUp = false;
        isLeft = false;
        isRight = false;

        if (Input.GetKey(KeyCode.D))
        {
            isRight = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            isLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isUp = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Player.Instance.TakeItem();
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

        }
    }
}
