using UnityEngine;

public class TakeDrop
{
    public void TryTake(Collider2D collider)
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
    }

    public void DropItem()
    {
        Player.Instance.heldItem.transform.SetParent(null);

        Rigidbody2D rbItem = Player.Instance.heldItem.GetComponent<Rigidbody2D>();
        rbItem.isKinematic = false;

        Player.Instance.itemCollider.enabled = true;

        Player.Instance.heldItem = null;
    }
}
