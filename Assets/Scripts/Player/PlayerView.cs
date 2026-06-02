using UnityEngine;

public class PlayerView : MonoBehaviour 
{
    public Rigidbody2D rb;
    public Animator animator;

    public Transform holdPosition;
    public GameObject heldItem = null;
    public Collider2D itemCollider;

    public bool isRight, isLeft, Jump;

    public Vector3 position => transform.position;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
