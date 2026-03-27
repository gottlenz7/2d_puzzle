using UnityEngine;
using UnityEngine.Rendering;

public class Light : MonoBehaviour
{
    public bool isCreated = false, isGrowing = true;
    private float growthSpeed = 10f, grow;
    public bool isHorizontal = true, isUp = true, isRight = true;
    private int lightNumber;

    private void Update()
    {
        if (isGrowing)
            Grow();
    }

    private void Grow()
    {
        Vector2 newScale = transform.localScale;
        Vector2 newPosition = transform.position;
        grow = growthSpeed * Time.deltaTime;

        newScale.x += grow;

        if (isHorizontal)
        {
            if (isRight)
                newPosition.x += grow / 2;
            else
                newPosition.x -= grow / 2;
        }
        else
        {
            if (isUp)
                newPosition.y += grow / 2;
            else
                newPosition.y -= grow / 2;
        }

        transform.localScale = newScale;
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Back"))
        {
            if (other.transform.parent != null)
            {
                Mirror mirrorScript = other.transform.parent.GetComponent<Mirror>();
                mirrorScript.lastLight = this;
            }
            
            isGrowing = false;
            isCreated = true;
            return;
        }

        if (other.CompareTag("Mirror") && transform.parent != other.transform)
        {
            isGrowing = false;

            Mirror mirrorScript = other.GetComponent<Mirror>();
            mirrorScript.lastLight = this;

            if (!isCreated)
            {
                isCreated = true;
                CreateNewLight(other);
            }
        }
    }

    public void CreateNewLight(Collider2D mirror)
    {
        lightNumber++;

        GameObject newLight = new GameObject($"Light_{lightNumber}");
        newLight.transform.parent = mirror.transform;
        newLight.transform.localPosition = new Vector3(0f, 0f, 0f);
        newLight.transform.localScale = new Vector3(0.1f, 0.1f, 0f);

        Light lightScript = newLight.AddComponent<Light>();
        lightScript.growthSpeed = growthSpeed;
        lightScript.lightNumber = lightNumber;

        DirectionLight(lightScript, mirror, newLight);

        SpriteRenderer renderer = newLight.AddComponent<SpriteRenderer>();
        renderer.sprite = GetComponent<SpriteRenderer>().sprite;
        renderer.color = GetComponent<SpriteRenderer>().color;
        renderer.sortingOrder = -1;

        BoxCollider2D collider = newLight.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1f, 1f);
        collider.isTrigger = true;

        Rigidbody2D rb = newLight.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void DirectionLight(Light lightScript, Collider2D mirror, GameObject newLight)
    {
        if (transform.rotation.eulerAngles.z == 0f)
        {
            newLight.transform.eulerAngles = new Vector3(0f, 0f, 90f);
            lightScript.isHorizontal = false;
        }
        else
        {
            newLight.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            lightScript.isHorizontal = true;
        }

        if (mirror.transform.up.x > 0f)
            lightScript.isRight = true;
        else
            lightScript.isRight = false;

        if (mirror.transform.up.y  > 0f)
            lightScript.isUp = true;
        else
            lightScript.isUp = false;
    }
}
