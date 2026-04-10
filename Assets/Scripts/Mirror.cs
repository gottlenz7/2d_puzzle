using UnityEngine;

public class Mirror : MonoBehaviour
{
    public Light lastLight;
    public int preNumber;
    public Transform mirror;

    private float rotateMirror = 90f;
    private string childName;
    private Collider2D colliderMirror;

    private void Awake()
    {
        colliderMirror = GetComponent<Collider2D>();
    }

    public void Rotate()
    {

        transform.Rotate(0f, 0f, rotateMirror);
        colliderMirror.isTrigger = false;
        colliderMirror.isTrigger = true;

        if (transform.childCount > 1)
        {

            GameObject[] lights = GameObject.FindGameObjectsWithTag("Light");

            foreach (GameObject light in lights)
            {
                string objectName = light.name;

                if (transform.childCount > 1)
                {
                    childName = transform.GetChild(1).name;

                    string[] parts = objectName.Split('_');
                    string[] partsChild = childName.Split('_');
                    int.TryParse(partsChild[1], out int numberChild);

                    if (parts[0] == "Light")
                    {
                        int.TryParse(parts[1], out int number);

                        if (number >= numberChild)
                            Destroy(light);
                    }
                }
            }
        }

        if (lastLight != null)
        {
            Collider2D lightCollider = lastLight.GetComponent<Collider2D>();

            lastLight.isCreated = false;
            lightCollider.enabled = false;
            lightCollider.enabled = true;
        }
            
    }
}
