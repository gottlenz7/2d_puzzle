using UnityEngine;

public class TakeDropController
{
    public void TakeItem()
    {
        if (PlayerModel.Instance.heldItem == null)
            TryTake();
        else
            DropItem();
    }

    private void TryTake()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(PlayerModel.Instance.transform.position, 2f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Item"))
            {
                PlayerModel.Instance.heldItem = collider.gameObject;
                PlayerModel.Instance.itemCollider = collider;

                Rigidbody2D rbItem = PlayerModel.Instance.heldItem.GetComponent<Rigidbody2D>();
                rbItem.isKinematic = true;
                rbItem.linearVelocity = Vector2.zero;

                PlayerModel.Instance.itemCollider.enabled = false;

                PlayerModel.Instance.heldItem.transform.SetParent(PlayerModel.Instance.holdPosition);
                PlayerModel.Instance.heldItem.transform.localPosition = Vector2.zero;
                PlayerModel.Instance.heldItem.transform.localRotation = Quaternion.identity;

                break;
            }
        }
    }

    private void DropItem()
    {
        PlayerModel.Instance.heldItem.transform.SetParent(null);

        Rigidbody2D rbItem = PlayerModel.Instance.heldItem.GetComponent<Rigidbody2D>();
        rbItem.isKinematic = false;

        PlayerModel.Instance.itemCollider.enabled = true;

        PlayerModel.Instance.heldItem = null;
    }
}
