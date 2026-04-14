using UnityEngine;

public class Robot : MonoBehaviour
{
    public Transform laser;
    public LayerMask hitLayers;
    public LineRenderer lineRenderer;

    private float maxAngle = 90f, swaySpeed = 0.3f, swayTimer = 0f, swayAngle = 0f;
    private Vector3 headRotation, startPoint, direction;
    
    private float maxDistance = 100f;
    private RaycastHit2D hit;

    private void Update()
    {
        RotateHead();
        UpdateLaser();
    }

    private void RotateHead()
    {
        swayTimer += Time.deltaTime * swaySpeed;
        swayAngle = Mathf.Sin(swayTimer) * maxAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, headRotation.z + swayAngle);
    }

    void UpdateLaser()
    {
        startPoint = laser.position;
        direction = Quaternion.Euler(0, 0, swayAngle) * Vector3.up;

        hit = Physics2D.Raycast(startPoint, direction, maxDistance, hitLayers);

        if (hit.collider != null)
        {
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, startPoint + direction * maxDistance);
        }
    }
}
