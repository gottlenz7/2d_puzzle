using System.Diagnostics.Tracing;
using UnityEngine;

public class Prism : MonoBehaviour
{
    public static Prism Instance { get; private set; }

    public Light redLight, blueLight;
    public Transform redBox, blueBox;
    public Material redMaterial, blueMaterial, purpleMaterial;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        if (redLight == null && blueLight == null)
            DeleteChildren();

        if (transform.childCount > 0)
            ExitLight(transform.GetChild(0));
    }

    public void ExitLight(Transform light)
    {
        light.GetComponent<LineRenderer>().SetPosition(0, transform.position);

        if (redLight != null && blueLight == null)
        {
            light.GetComponent<LineRenderer>().sharedMaterial = redMaterial;
            light.GetComponent<LineRenderer>().SetPosition(1, redBox.position);
            light.GetComponent<Light>().isGrowing = false;
        }
        else if (redLight == null && blueLight != null)
        {
            light.GetComponent<LineRenderer>().sharedMaterial = blueMaterial;
            light.GetComponent<LineRenderer>().SetPosition(1, blueBox.position);
            light.GetComponent<Light>().isGrowing = false;
        }
        else if (redLight != null && blueLight != null)
        {
            light.GetComponent<LineRenderer>().sharedMaterial = purpleMaterial;
        }
    }

    public void DeleteChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
