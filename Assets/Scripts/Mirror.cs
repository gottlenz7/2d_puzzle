using UnityEngine;

public class Mirror : MonoBehaviour
{
    public Light lastLight;
    public Transform mirror;

    private float rotateMirror = 90f;
    private string childName;

    public void Update()
    {
        if ((lastLight == null || Prism.Instance.redLight == null || Prism.Instance.blueLight == null) 
            && transform.childCount > 1 && transform.name == "Mirror (4)")
            Destroy(transform.GetChild(1).gameObject);
    }

    public void Rotate()
    {
        transform.Rotate(0f, 0f, rotateMirror);


        if (transform.childCount > 1)
        {
            GameObject[] lights = GameObject.FindGameObjectsWithTag("Light");

            foreach (GameObject light in lights)
            {
                string objectName = light.name;

                childName = transform.GetChild(1).name;

                string[] parts = objectName.Split('_');
                string[] partsChild = childName.Split('_');
                int.TryParse(partsChild[1], out int numberChild);

                int.TryParse(parts[1], out int number);

                if (number >= numberChild &&
                    transform.GetChild(1).GetComponent<LineRenderer>().sharedMaterial == light.GetComponent<LineRenderer>().sharedMaterial)
                    Destroy(light);
            }
        }

        if (lastLight != null)
        {
            Physics2D.SyncTransforms();

            lastLight.lineRenderer.SetPosition(1, lastLight.startPoint + 0.5f * lastLight.direction);
            lastLight.isCreated = false;
            lastLight.isGrowing = true;
        }
    }
}
