using UnityEngine;

public class Ring : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Throne"))
        {
            MonsterPlant.Instance.animator.SetBool("Eating", true);
        }
    }
}
