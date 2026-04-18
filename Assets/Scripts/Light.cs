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

            if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Mirror" && transform.parent != hit.collider.transform && !isCreated)
            {
                isCreated = true;
                isGrowing = false;
                CreateNewLight(hit.collider, hit);
            }
        }
    }

    private void CreateNewLight(Collider2D collider, RaycastHit2D hit)
    {
        Mirror mirrorScript = collider.transform.GetComponent<Mirror>();
        mirrorScript.lastLight = this;

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

        DirectionLight(collider, lightScript);

        LineRenderer renderer = newLight.AddComponent<LineRenderer>();
        renderer.startWidth = 0.1f;
        renderer.material = lineRenderer.material;
        renderer.colorGradient = lineRenderer.colorGradient;
    }

    public void DirectionLight(Collider2D collider, Light lightScript)
    {
        lightScript.isRight = collider.transform.up.x > 0f ? true : false;
        lightScript.isUp = collider.transform.up.y > 0f ? true : false;
    }
}
