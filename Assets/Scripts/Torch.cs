using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject fire, light;
    private List<GameObject> hiddenObjects = new List<GameObject>();

    public void TryHide()
    {
        if (fire.activeSelf)
            HideChildren();
        else
            ShowChildren();
    }

    public void HideChildren()
    {
        fire.SetActive(false);
        light.SetActive(false);

        HideObjects();
    }

    public void ShowChildren()
    {
        fire.SetActive(true);
        light.SetActive(true);

        ShowObjects();
    }

    private void HideObjects()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("ForLight"))
            {
                hiddenObjects.Add(collider.gameObject);
                collider.gameObject.SetActive(false);
            }
        }
    }

    private void ShowObjects()
    {
        foreach (GameObject obj in hiddenObjects)
        {
            if (obj != null)
                obj.gameObject.SetActive(true);
        }

        hiddenObjects.Clear();
    }
}
