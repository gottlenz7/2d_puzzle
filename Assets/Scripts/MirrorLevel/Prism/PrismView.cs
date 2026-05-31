using UnityEngine;

public class PrismView : MonoBehaviour
{
    public Transform redBox, blueBox;
    public Material redMaterial, blueMaterial, purpleMaterial;

    public void CreateRedLight(Transform light)
    {
        light.GetComponent<LineRenderer>().sharedMaterial = redMaterial;
        light.GetComponent<LineRenderer>().SetPosition(1, redBox.position);
        light.GetComponent<Light>().isGrowing = false;

        redBox.GetComponent<Receiver>().Hit();
    }

    public void CreateBlueLight(Transform light)
    {
        light.GetComponent<LineRenderer>().sharedMaterial = blueMaterial;
        light.GetComponent<LineRenderer>().SetPosition(1, blueBox.position);
        light.GetComponent<Light>().isGrowing = false;

        blueBox.GetComponent<Receiver>().Hit();
    }

    public void CreatePurpleLight(Transform light)
    {
        light.GetComponent<LineRenderer>().sharedMaterial = purpleMaterial;
    }
}
