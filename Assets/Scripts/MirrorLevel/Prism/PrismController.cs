using UnityEngine;

public class PrismController : MonoBehaviour 
{
    public static PrismController Instance { get; private set; }

    public PrismView view;

    public static PrismModel Model { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (Model == null)
            Model = new PrismModel();
    }

    private void Update()
    {
        if (!Model.HasRed && !Model.HasBlue)
            DeleteChildren();

        if (transform.childCount > 0)
            ExitLight(transform.GetChild(0));
    }

    private void ExitLight(Transform light)
    {
        light.GetComponent<LineRenderer>().SetPosition(0, transform.position);

        if (Model.HasRed && !Model.HasBlue)
            view.CreateRedLight(light);

        else if (!Model.HasRed && Model.HasBlue)
            view.CreateBlueLight(light);

        else if (Model.HasRed && Model.HasBlue)
            view.CreatePurpleLight(light);
    }

    public void DeleteChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            Destroy(transform.GetChild(i).gameObject);
    }

    public void SetRedLight(Light light)
    {
        Model.redLight = light;
    }

    public void SetBlueLight(Light light)
    {
        Model.blueLight = light;
    }
}
