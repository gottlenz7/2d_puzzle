using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering;

public class Light : MonoBehaviour
{
    public bool isCreated = true, isGrowing = true;
    public bool isHorizontal = true, isUp = true, isRight = true;
    public Vector3 startPoint, direction, newpoint;

    public LayerMask hitLayers;
    public LineRenderer lineRenderer;

    private float maxDistance = 100f;
    private RaycastHit2D hit;
    private int lightNumber;

    private void Update()
    {
        if (isGrowing)
            Grow();
    }

    private void Grow()
    {
        lineRenderer = GetComponent<LineRenderer>();

        startPoint = transform.position;

        if (isHorizontal)
            direction = isRight ? transform.right : -transform.right;
        else
            direction = isUp ? transform.up : -transform.up;

        hit = Physics2D.Raycast(startPoint, direction, maxDistance, hitLayers);
        lineRenderer.SetPosition(0, startPoint);

        if (hit.collider != null && transform.parent == hit.collider.transform)
        {
            newpoint = startPoint + 0.5f * direction;
            lineRenderer.SetPosition(1, newpoint);

            hit = Physics2D.Raycast(newpoint, direction, maxDistance, hitLayers);
        }

        if (hit.collider != null)
        {
            lineRenderer.SetPosition(1, hit.point + (Vector2)(0.1f * direction));
            

            if (transform.parent != hit.collider.transform && !isCreated)
            {
                if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Mirror"
                    || LayerMask.LayerToName(hit.collider.gameObject.layer) == "Prism")
                {
                    isCreated = true;
                    isGrowing = false;

                    if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Prism")
                        PrismController.Instance.DeleteChildren();

                    CreateNewLight(hit.collider, hit, LayerMask.LayerToName(hit.collider.gameObject.layer));
                }

                else if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Receiver")
                {
                    Receiver receiver = hit.collider.GetComponent<Receiver>();
                    receiver.Hit();
                }
            }
        }
    }

    private void CreateNewLight(Collider2D collider, RaycastHit2D hit, string layer)
    {
        lightNumber++;

        GameObject newLight = new GameObject($"Light_{lightNumber}");
        newLight.transform.parent = collider.transform;
        newLight.transform.localPosition = new Vector3(0f, 0f, 0f);

        newLight.tag = "Light";

        Light lightScript = newLight.AddComponent<Light>();
        lightScript.isCreated = false;
        lightScript.isGrowing = true;
        lightScript.lightNumber = lightNumber;
        lightScript.isHorizontal = !isHorizontal;
        lightScript.hitLayers = hitLayers;

        LineRenderer renderer = newLight.AddComponent<LineRenderer>();
        renderer.startWidth = 0.1f;
        
        DirectionLight(collider, lightScript);

        if (layer == "Prism")
        {
            if (lineRenderer.sharedMaterial.name == "Red Light")
                PrismController.Instance.SetRedLight(this);
            else
                PrismController.Instance.SetBlueLight(this);
        }
        else
        {
            Mirror mirrorScript = collider.transform.GetComponent<Mirror>();
            mirrorScript.lastLight = this;

            renderer.sharedMaterial = lineRenderer.sharedMaterial;
        }
    }

    public void DirectionLight(Collider2D collider, Light lightScript)
    {
        lightScript.isRight = collider.transform.up.x >= 0f ? true : false;
        lightScript.isUp = collider.transform.up.y >= 0f ? true : false;
    }
}
