using UnityEngine;

public class TakeDrop
{
    public void TakeItem()
    {
        if (Player.Instance.heldItem == null)
            TryTake();
        else
            DropItem();
    }

    private void TryTake()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(Player.Instance.transform.position, 2f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Item"))
            {
                Player.Instance.heldItem = collider.gameObject;
                Player.Instance.itemCollider = collider;

                Rigidbody2D rbItem = Player.Instance.heldItem.GetComponent<Rigidbody2D>();
                rbItem.isKinematic = true;
                rbItem.linearVelocity = Vector2.zero;

                Player.Instance.itemCollider.enabled = false;

                Player.Instance.heldItem.transform.SetParent(Player.Instance.holdPosition);
                Player.Instance.heldItem.transform.localPosition = Vector2.zero;
                Player.Instance.heldItem.transform.localRotation = Quaternion.identity;

                break;
            }
        }
    }

    private void DropItem()
    {
        Player.Instance.heldItem.transform.SetParent(null);

        Rigidbody2D rbItem = Player.Instance.heldItem.GetComponent<Rigidbody2D>();
        rbItem.isKinematic = false;

        Player.Instance.itemCollider.enabled = true;

        Player.Instance.heldItem = null;
    }
}
