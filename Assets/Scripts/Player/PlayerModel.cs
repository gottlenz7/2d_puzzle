using UnityEngine;

public class PlayerModel
{
    public float speed = 5f, minSpeed = 0.1f, jumpForce = 6f, maxJump = 0f;
    public Vector2 lastDirection = Vector2.down;
    public int jumpsLeft = 2;
    public int maxJumps = 2;
    public bool isGrounded = false;
}
