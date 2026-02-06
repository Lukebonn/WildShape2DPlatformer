using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos, length;
    [Header("Optional:")]
    public GameObject cam;
    public float parallaxEffect;

    private void Start()
    {
        //set to y because background is only moving horizontally
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    private void FixedUpdate()
    {
        //calculate distance background will move based on cam movement
        float distance = cam.transform.position.x * parallaxEffect; // 0 = with camera || 1 = not at all
        float movement = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        //if background has reached the end of its length adjust its position for infinite scrolling
        if (movement > startPos + length)
        {
            startPos += length;
        }
        else if (movement < startPos - length)
        {
            startPos -= length;
        }

    }
}