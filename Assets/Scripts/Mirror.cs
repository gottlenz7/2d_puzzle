using UnityEngine;

public class Mirror : MonoBehaviour
{
    public static Mirror Instance { get; private set; }
    public Light lastLight;
    public int preNumber;

    private float rotateMirror = 90f;
    private string childName;
    private Collider2D colliderMirror;

    private void Awake()
    {
        colliderMirror = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float distance = Vector3.Distance(transform.position, Player.Instance.transform.position);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (distance < 2f)
            {
                transform.Rotate(0f, 0f, rotateMirror);
                colliderMirror.isTrigger = false;
                colliderMirror.isTrigger = true;


                if (transform.childCount > 1)
                {

                    GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

                    foreach (GameObject obj in allObjects)
                    {
                        string objectName = obj.name;

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
                                    Destroy(obj);
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
    }
}
