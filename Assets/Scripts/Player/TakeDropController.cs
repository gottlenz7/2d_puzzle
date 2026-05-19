using UnityEngine;

public class TakeDropController
{
    private PlayerView view;

    public TakeDropController(PlayerView view)
    {
        this.view = view;
    }

    public void TakeItem()
    {
        if (view.heldItem == null)
            TryTake();
        else
            DropItem();
    }

    private void TryTake()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(view.position, 2f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Item"))
            {
                view.heldItem = collider.gameObject;
                view.itemCollider = collider;

                ThrowController throwController = view.heldItem.GetComponent<ThrowController>();
                if (throwController != null) 
                    throwController.Init(view);

                Rigidbody2D rbItem = view.heldItem.GetComponent<Rigidbody2D>();
                rbItem.isKinematic = true;
                rbItem.linearVelocity = Vector2.zero;

                view.itemCollider.enabled = false;

                view.heldItem.transform.SetParent(view.holdPosition);
                view.heldItem.transform.localPosition = Vector2.zero;
                view.heldItem.transform.localRotation = Quaternion.identity;

                break;
            }
        }
    }

    private void DropItem()
    {
        view.heldItem.transform.SetParent(null);

        Rigidbody2D rbItem = view.heldItem.GetComponent<Rigidbody2D>();
        rbItem.isKinematic = false;

        view.itemCollider.enabled = true;

        view.heldItem = null;
    }
}
