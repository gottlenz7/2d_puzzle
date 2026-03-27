using UnityEngine;

public class PlayerVisual : MonoBehaviour 
{
    public static PlayerVisual Instance { get; private set; }

    private bool isRight, isLeft, Jump;

    public Animator animator;

    private SpriteRenderer spriteRenderer;

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

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        SetDirection(Player.Instance.IsRight, Player.Instance.IsLeft, Player.Instance.Jump);
        SetAnimator(animator);
    }

    public void SetDirection(bool right, bool left, bool jump)
    {
        isRight = right;
        isLeft = left;
        Jump = jump;
    }

    public void SetAnimator(Animator animator)
    {
        animator.SetBool("isRight", isRight);
        animator.SetBool("isLeft", isLeft);
        animator.SetBool("Jump", Jump);
    }
}
